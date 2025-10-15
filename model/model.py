import argparse
import csv
import copy
from dataclasses import dataclass
from pathlib import Path
from typing import Dict, List, Optional, Tuple

import torch
from torch import nn
from torch.utils.data import DataLoader, TensorDataset, WeightedRandomSampler


FEATURE_COLUMNS = [
    "Size",
    "StraightTimePenalty",
    "RotationPenalty",
    "Fork",
    "DeadEnd",
]
TARGET_COLUMNS = ["BFS", "DFS", "Astar"]


@dataclass
class NormalizationStats:
    mean: torch.Tensor
    std: torch.Tensor

    def to_dict(self) -> Dict[str, List[float]]:
        return {
            "mean": self.mean.tolist(),
            "std": self.std.tolist(),
        }

    @classmethod
    def from_dict(cls, payload: Dict[str, List[float]]) -> "NormalizationStats":
        mean = torch.tensor(payload["mean"], dtype=torch.float32)
        std = torch.tensor(payload["std"], dtype=torch.float32)
        return cls(mean=mean, std=std)


class MazeNet(nn.Module):
    def __init__(
        self,
        input_dim: int,
        hidden_dims: Tuple[int, ...] = (64, 32),
        dropout: float = 0.15,
        num_classes: int = len(TARGET_COLUMNS),
    ) -> None:
        super().__init__()
        layers: List[nn.Module] = []
        prev_dim = input_dim
        for hidden_dim in hidden_dims:
            layers.append(nn.Linear(prev_dim, hidden_dim))
            layers.append(nn.ReLU())
            if dropout > 0:
                layers.append(nn.Dropout(p=dropout))
            prev_dim = hidden_dim
        layers.append(nn.Linear(prev_dim, num_classes))
        self.network = nn.Sequential(*layers)
        self.input_dim = input_dim
        self.num_classes = num_classes

    def forward(self, features: torch.Tensor) -> torch.Tensor:
        return self.network(features)


def load_csv(csv_path: Path) -> Tuple[torch.Tensor, torch.Tensor]:
    if not csv_path.exists():
        raise FileNotFoundError(f"Could not find dataset at {csv_path}")

    with csv_path.open("r", newline="") as file_handle:
        reader = csv.DictReader(file_handle)
        missing_columns = [col for col in FEATURE_COLUMNS + TARGET_COLUMNS if col not in reader.fieldnames]
        if missing_columns:
            raise ValueError(f"CSV file must contain columns: {missing_columns}")

        feature_rows: List[List[float]] = []
        labels: List[int] = []

        for row in reader:
            if not row:
                continue

            try:
                feature_values = [float(row[column]) for column in FEATURE_COLUMNS]
                target_values = [float(row[column]) for column in TARGET_COLUMNS]
            except ValueError as exc:
                raise ValueError(f"Encountered non-numeric value in row: {row}") from exc

            label = min(range(len(target_values)), key=lambda idx: target_values[idx])
            feature_rows.append(feature_values)
            labels.append(label)

    if not feature_rows:
        raise ValueError(f"No data rows found in {csv_path}")

    feature_tensor = torch.tensor(feature_rows, dtype=torch.float32)
    label_tensor = torch.tensor(labels, dtype=torch.long)
    return feature_tensor, label_tensor


def train_val_split(
    features: torch.Tensor,
    labels: torch.Tensor,
    val_ratio: float,
    seed: int,
) -> Tuple[Tuple[torch.Tensor, torch.Tensor], Tuple[torch.Tensor, torch.Tensor]]:
    sample_count = features.size(0)
    if sample_count < 2:
        raise ValueError("Need at least two samples to create a train/validation split.")

    generator = torch.Generator().manual_seed(seed)
    num_classes = len(TARGET_COLUMNS)

    train_indices: List[torch.Tensor] = []
    val_indices: List[torch.Tensor] = []

    for class_idx in range(num_classes):
        class_mask = (labels == class_idx).nonzero(as_tuple=False).squeeze(1)
        if class_mask.numel() == 0:
            continue

        shuffled = class_mask[torch.randperm(class_mask.numel(), generator=generator)]
        desired_val = int(round(class_mask.numel() * val_ratio))
        max_allowed_val = max(class_mask.numel() - 1, 0)
        val_count = min(desired_val, max_allowed_val)

        if val_count > 0:
            val_indices.append(shuffled[:val_count])
            train_indices.append(shuffled[val_count:])
        else:
            train_indices.append(shuffled)

    if not train_indices:
        raise ValueError("Unable to create a training split: no samples found.")

    if not val_indices:
        # Fallback to a simple random split if stratification produced no validation set.
        indices = torch.randperm(sample_count, generator=generator)
        raw_val_size = int(sample_count * val_ratio)
        val_size = min(max(raw_val_size, 1), sample_count - 1)
        val_indices_tensor = indices[:val_size]
        train_indices_tensor = indices[val_size:]
    else:
        train_indices_tensor = torch.cat(train_indices)
        val_indices_tensor = torch.cat(val_indices)
        train_indices_tensor = train_indices_tensor[torch.randperm(train_indices_tensor.numel(), generator=generator)]
        val_indices_tensor = val_indices_tensor[torch.randperm(val_indices_tensor.numel(), generator=generator)]

    train_features = features[train_indices_tensor]
    train_labels = labels[train_indices_tensor]
    val_features = features[val_indices_tensor]
    val_labels = labels[val_indices_tensor]

    return (train_features, train_labels), (val_features, val_labels)


def compute_normalization(train_features: torch.Tensor) -> NormalizationStats:
    mean = train_features.mean(dim=0)
    std = train_features.std(dim=0)
    std = torch.where(std < 1e-6, torch.ones_like(std), std)
    return NormalizationStats(mean=mean, std=std)


def normalize(features: torch.Tensor, stats: NormalizationStats) -> torch.Tensor:
    mean = stats.mean.to(features.device)
    std = stats.std.to(features.device)
    return (features - mean) / std


def create_loaders(
    features: torch.Tensor,
    labels: torch.Tensor,
    batch_size: int,
    val_ratio: float,
    seed: int,
    balance_classes: bool,
) -> Tuple[DataLoader, DataLoader, NormalizationStats, Optional[torch.Tensor]]:
    (train_features, train_labels), (val_features, val_labels) = train_val_split(
        features, labels, val_ratio=val_ratio, seed=seed
    )

    normalization_stats = compute_normalization(train_features)
    train_dataset = TensorDataset(normalize(train_features, normalization_stats), train_labels)
    val_dataset = TensorDataset(normalize(val_features, normalization_stats), val_labels)

    class_weights: Optional[torch.Tensor] = None
    if balance_classes:
        class_counts = torch.bincount(train_labels, minlength=len(TARGET_COLUMNS)).float()
        class_counts = torch.clamp(class_counts, min=1)
        class_weights = (class_counts.sum() / (class_counts * len(class_counts))).to(torch.float32)

        sample_weights = class_weights[train_labels]
        sampler = WeightedRandomSampler(weights=sample_weights, num_samples=len(sample_weights), replacement=True)
        train_loader = DataLoader(train_dataset, batch_size=batch_size, sampler=sampler)
    else:
        train_loader = DataLoader(train_dataset, batch_size=batch_size, shuffle=True)
    val_loader = DataLoader(val_dataset, batch_size=batch_size, shuffle=False)

    return train_loader, val_loader, normalization_stats, class_weights


@torch.no_grad()
def evaluate(
    model: nn.Module,
    data_loader: DataLoader,
    criterion: nn.Module,
    device: torch.device,
) -> Tuple[float, float]:
    model.eval()
    total_loss = 0.0
    total_samples = 0
    correct_predictions = 0

    for features, labels in data_loader:
        features = features.to(device)
        labels = labels.to(device)

        logits = model(features)
        loss = criterion(logits, labels)
        predictions = torch.argmax(logits, dim=1)

        batch_size = labels.size(0)
        total_loss += loss.item() * batch_size
        total_samples += batch_size
        correct_predictions += (predictions == labels).sum().item()

    average_loss = total_loss / total_samples
    accuracy = correct_predictions / total_samples
    return average_loss, accuracy


def train_model(
    model: nn.Module,
    train_loader: DataLoader,
    val_loader: DataLoader,
    epochs: int,
    learning_rate: float,
    weight_decay: float,
    device: torch.device,
    class_weights: Optional[torch.Tensor] = None,
) -> Dict[str, List[float]]:
    weight_tensor = class_weights.to(device) if class_weights is not None else None
    criterion = nn.CrossEntropyLoss(weight=weight_tensor)
    optimizer = torch.optim.Adam(model.parameters(), lr=learning_rate, weight_decay=weight_decay)
    history = {
        "train_loss": [],
        "train_accuracy": [],
        "val_loss": [],
        "val_accuracy": [],
    }

    best_state = copy.deepcopy(model.state_dict())
    best_val_loss = float("inf")

    for epoch in range(1, epochs + 1):
        model.train()
        running_loss = 0.0
        running_correct = 0
        running_samples = 0

        for features, labels in train_loader:
            features = features.to(device)
            labels = labels.to(device)

            optimizer.zero_grad()
            logits = model(features)
            loss = criterion(logits, labels)
            loss.backward()
            optimizer.step()

            batch_size = labels.size(0)
            running_loss += loss.item() * batch_size
            running_samples += batch_size
            running_correct += (logits.argmax(dim=1) == labels).sum().item()

        train_loss = running_loss / running_samples
        train_accuracy = running_correct / running_samples

        val_loss, val_accuracy = evaluate(model, val_loader, criterion, device)

        history["train_loss"].append(train_loss)
        history["train_accuracy"].append(train_accuracy)
        history["val_loss"].append(val_loss)
        history["val_accuracy"].append(val_accuracy)

        improved = val_loss < best_val_loss - 1e-4
        if improved:
            best_val_loss = val_loss
            best_state = copy.deepcopy(model.state_dict())

        print(
            f"Epoch {epoch:03d} | Train Loss: {train_loss:.4f} | Train Acc: {train_accuracy:.3f} "
            f"| Val Loss: {val_loss:.4f} | Val Acc: {val_accuracy:.3f}"
        )

    model.load_state_dict(best_state)
    return history


@torch.no_grad()
def predict_faster_algorithm(
    model: nn.Module,
    feature_vector: List[float],
    normalization: NormalizationStats,
    device: torch.device,
) -> Tuple[int, torch.Tensor]:
    model.eval()
    features = torch.tensor(feature_vector, dtype=torch.float32, device=device)
    normalized = normalize(features, normalization)
    logits = model(normalized.unsqueeze(0))
    probabilities = torch.softmax(logits, dim=1).squeeze(0)
    prediction = int(torch.argmax(probabilities).item())
    return prediction, probabilities.cpu()


def parse_arguments() -> argparse.Namespace:
    default_csv = Path(__file__).parent / "data" / "maze_data.csv"
    parser = argparse.ArgumentParser(
        description="Train a model to predict which algorithm (BFS, DFS, or A*) solves a maze fastest."
    )
    parser.add_argument("--csv-path", type=Path, default=default_csv, help="Path to the maze dataset CSV file.")
    parser.add_argument("--batch-size", type=int, default=32, help="Mini-batch size for training.")
    parser.add_argument("--epochs", type=int, default=200, help="Maximum number of training epochs.")
    parser.add_argument("--learning-rate", type=float, default=1e-3, help="Learning rate for the optimizer.")
    parser.add_argument("--weight-decay", type=float, default=1e-4, help="Weight decay for regularization.")
    parser.add_argument("--val-ratio", type=float, default=0.2, help="Fraction of data reserved for validation.")
    parser.add_argument(
        "--balance-classes",
        action="store_true",
        help="Use class-balanced sampling and weighted loss (may reduce overall accuracy).",
    )
    parser.add_argument("--seed", type=int, default=42, help="Random seed for reproducibility.")
    parser.add_argument(
        "--output-path",
        type=Path,
        default=Path(__file__).parent / "artifacts" / "maze_predictor.pt",
        help="Where to store the trained model checkpoint.",
    )
    return parser.parse_args()


def ensure_output_directory(path: Path) -> None:
    if not path.parent.exists():
        path.parent.mkdir(parents=True, exist_ok=True)


def save_checkpoint(
    model: nn.Module,
    normalization: NormalizationStats,
    history: Dict[str, List[float]],
    checkpoint_path: Path,
    class_weights: Optional[torch.Tensor] = None,
) -> None:
    ensure_output_directory(checkpoint_path)
    label_mapping = {index: f"{name} fastest" for index, name in enumerate(TARGET_COLUMNS)}
    payload = {
        "model_state_dict": model.state_dict(),
        "input_dim": getattr(model, "input_dim", next(model.parameters()).size(1)),
        "num_classes": getattr(model, "num_classes", len(TARGET_COLUMNS)),
        "normalization": normalization.to_dict(),
        "history": history,
        "label_mapping": label_mapping,
        "feature_columns": FEATURE_COLUMNS,
        "class_names": TARGET_COLUMNS,
    }
    if class_weights is not None:
        payload["class_weights"] = class_weights.tolist()

    torch.save(payload, checkpoint_path)
    print(f"Model checkpoint saved to {checkpoint_path}")


def main() -> None:
    args = parse_arguments()
    torch.manual_seed(args.seed)

    features, labels = load_csv(args.csv_path)
    train_loader, val_loader, normalization, class_weights = create_loaders(
        features,
        labels,
        batch_size=args.batch_size,
        val_ratio=args.val_ratio,
        seed=args.seed,
        balance_classes=args.balance_classes,
    )

    device = torch.device("cuda" if torch.cuda.is_available() else "cpu")
    model = MazeNet(input_dim=features.size(1), num_classes=len(TARGET_COLUMNS))
    model = model.to(device)

    history = train_model(
        model=model,
        train_loader=train_loader,
        val_loader=val_loader,
        epochs=args.epochs,
        learning_rate=args.learning_rate,
        weight_decay=args.weight_decay,
        device=device,
        class_weights=class_weights,
    )

    if class_weights is not None:
        eval_criterion = nn.CrossEntropyLoss(weight=class_weights.to(device))
    else:
        eval_criterion = nn.CrossEntropyLoss()
    val_loss, val_accuracy = evaluate(model, val_loader, eval_criterion, device)
    print(f"Validation Loss: {val_loss:.4f} | Validation Accuracy: {val_accuracy:.3f}")

    save_checkpoint(model, normalization, history, args.output_path, class_weights=class_weights)


if __name__ == "__main__":
    main()
