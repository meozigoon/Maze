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
MazeNet = maze_model.MazeNet
NormalizationStats = maze_model.NormalizationStats
predict_faster_algorithm = maze_model.predict_faster_algorithm

DEFAULT_CHECKPOINT_PATH = THIS_DIR / "artifacts" / "maze_predictor.pt"
EXAMPLE_FEATURES: Dict[str, float] = {
    "Size": 20.0,
    "StraightTimePenalty": 1.0,
    "RotationPenalty": 1.0,
    "Fork": 4.0,
    "DeadEnd": 2.0,
}


def load_checkpoint(
    checkpoint_path: Path,
    device: torch.device,
) -> Tuple[torch.nn.Module, NormalizationStats, Dict[int, str], Tuple[str, ...]]:
    if not checkpoint_path.exists():
        raise FileNotFoundError(f"Checkpoint file not found at {checkpoint_path}")

    payload = torch.load(checkpoint_path, map_location=device)

    if "input_dim" not in payload or "model_state_dict" not in payload:
        raise KeyError("Checkpoint does not contain required model information.")

    input_dim = payload["input_dim"]
    model = MazeNet(input_dim=input_dim).to(device)
    model.load_state_dict(payload["model_state_dict"])
    model.eval()

    normalization_payload = payload.get("normalization")
    if normalization_payload is None:
        raise KeyError("Checkpoint missing normalization statistics.")
    normalization = NormalizationStats.from_dict(normalization_payload)

    label_mapping = payload.get("label_mapping", {0: "BFS faster or equal", 1: "DFS faster"})
    feature_columns = tuple(payload.get("feature_columns", FEATURE_COLUMNS))

    return model, normalization, label_mapping, feature_columns


def run_inference(
    feature_values: Dict[str, float],
    *,
    checkpoint_path: Path = DEFAULT_CHECKPOINT_PATH,
    use_cuda: bool = False,
) -> Tuple[str, float, float]:
    device = torch.device("cuda" if use_cuda and torch.cuda.is_available() else "cpu")

    model, normalization, label_mapping, feature_columns = load_checkpoint(
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

    chosen_label = label_mapping.get(prediction, f"Class {prediction}")
    bfs_prob = float(probabilities[0].item())
    dfs_prob = float(probabilities[1].item())
    return chosen_label, bfs_prob, dfs_prob


def main() -> None:
    result_label, bfs_prob, dfs_prob = run_inference(EXAMPLE_FEATURES)

    print("Prediction Result")
    print("-----------------")
    print(f"Checkpoint: {DEFAULT_CHECKPOINT_PATH}")
    print(f"Input features: {EXAMPLE_FEATURES}")
    print(f"Predicted faster algorithm: {result_label}")
    print(f"Probabilities -> BFS: {bfs_prob:.3f}, DFS: {dfs_prob:.3f}")


if __name__ == "__main__":
    main()
