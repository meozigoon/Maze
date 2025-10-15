from __future__ import annotations

import importlib.util
import sys
from pathlib import Path
from typing import Dict, Tuple

import torch


THIS_DIR = Path(__file__).resolve().parent
MODEL_FILE = THIS_DIR / "model.py"

if not MODEL_FILE.exists():
    raise FileNotFoundError(f"Cannot locate model.py at {MODEL_FILE}")

spec = importlib.util.spec_from_file_location("maze_model_module", MODEL_FILE)
if spec is None or spec.loader is None:
    raise ImportError(f"Unable to create import spec for {MODEL_FILE}")

maze_model = importlib.util.module_from_spec(spec)
sys.modules["maze_model_module"] = maze_model
spec.loader.exec_module(maze_model)

FEATURE_COLUMNS = maze_model.FEATURE_COLUMNS
TARGET_COLUMNS = tuple(getattr(maze_model, "TARGET_COLUMNS", ("BFS", "DFS", "Astar")))
MazeNet = maze_model.MazeNet
NormalizationStats = maze_model.NormalizationStats
predict_faster_algorithm = maze_model.predict_faster_algorithm

DEFAULT_CHECKPOINT_PATH = THIS_DIR / "artifacts" / "maze_predictor.pt"
EXAMPLE_FEATURES: Dict[str, float] = {
    "Size": 10,
    "StraightTimePenalty": 100,
    "RotationPenalty": 150,
    "Fork": 13,
    "DeadEnd": 11,
}


def load_checkpoint(
    checkpoint_path: Path,
    device: torch.device,
) -> Tuple[torch.nn.Module, NormalizationStats, Dict[int, str], Tuple[str, ...], Tuple[str, ...]]:
    if not checkpoint_path.exists():
        raise FileNotFoundError(f"Checkpoint file not found at {checkpoint_path}")

    payload = torch.load(checkpoint_path, map_location=device)

    if "input_dim" not in payload or "model_state_dict" not in payload:
        raise KeyError("Checkpoint does not contain required model information.")

    raw_num_classes = payload.get("num_classes")
    if raw_num_classes is None:
        mapping = payload.get("label_mapping")
        if isinstance(mapping, dict) and mapping:
            raw_num_classes = len(mapping)
        else:
            class_names_payload = payload.get("class_names")
            if isinstance(class_names_payload, (list, tuple)) and class_names_payload:
                raw_num_classes = len(class_names_payload)
    if raw_num_classes is None:
        raw_num_classes = len(TARGET_COLUMNS)
    num_classes = int(raw_num_classes)

    input_dim = payload["input_dim"]
    model = MazeNet(input_dim=input_dim, num_classes=num_classes).to(device)
    model.load_state_dict(payload["model_state_dict"])
    model.eval()

    normalization_payload = payload.get("normalization")
    if normalization_payload is None:
        raise KeyError("Checkpoint missing normalization statistics.")
    normalization = NormalizationStats.from_dict(normalization_payload)

    label_mapping_payload = payload.get("label_mapping")
    if isinstance(label_mapping_payload, dict) and label_mapping_payload:
        label_mapping = {int(key): str(value) for key, value in label_mapping_payload.items()}
    else:
        label_mapping = {index: f"{name} fastest" for index, name in enumerate(TARGET_COLUMNS[:num_classes])}

    feature_columns = tuple(payload.get("feature_columns", FEATURE_COLUMNS))
    class_names_payload = payload.get("class_names")
    if isinstance(class_names_payload, (list, tuple)) and class_names_payload:
        class_names = tuple(str(name) for name in class_names_payload)[:num_classes]
    else:
        class_names = tuple(TARGET_COLUMNS[:num_classes])

    return model, normalization, label_mapping, feature_columns, class_names


def run_inference(
    feature_values: Dict[str, float],
    *,
    checkpoint_path: Path = DEFAULT_CHECKPOINT_PATH,
    use_cuda: bool = False,
) -> Tuple[str, Dict[str, float]]:
    device = torch.device("cuda" if use_cuda and torch.cuda.is_available() else "cpu")

    model, normalization, label_mapping, feature_columns, class_names = load_checkpoint(
        checkpoint_path=checkpoint_path,
        device=device,
    )

    missing = [name for name in feature_columns if name not in feature_values]
    if missing:
        raise ValueError(f"Missing required feature values: {missing}")

    feature_vector = [float(feature_values[column]) for column in feature_columns]

    prediction, probabilities = predict_faster_algorithm(
        model=model,
        feature_vector=feature_vector,
        normalization=normalization,
        device=device,
    )

    default_label = class_names[prediction] if prediction < len(class_names) else f"Class {prediction}"
    chosen_label = label_mapping.get(prediction, default_label)
    probability_map = {
        (class_names[index] if index < len(class_names) else f"Class {index}"): float(probabilities[index].item())
        for index in range(probabilities.size(0))
    }
    return chosen_label, probability_map


def main() -> None:
    result_label, probability_map = run_inference(EXAMPLE_FEATURES)

    print("Prediction Result")
    print("-----------------")
    print(f"Checkpoint: {DEFAULT_CHECKPOINT_PATH}")
    print(f"Input features: {EXAMPLE_FEATURES}")
    print(f"Predicted faster algorithm: {result_label}")
    probability_summary = ", ".join(f"{name}: {prob:.3f}" for name, prob in probability_map.items())
    print(f"Probabilities -> {probability_summary}")


if __name__ == "__main__":
    main()
