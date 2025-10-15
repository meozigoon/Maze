#!/usr/bin/env python3
"""
Generate algorithm comparison plots for maze metrics.

This script reads the maze dataset CSV and produces one plot per structural
metric that visualises BFS, DFS, and A* execution times. All charts are rendered
as line graphs: discrete metrics use grouped averages, continuous metrics use
sorted trends, and algorithm-centric plots compare cumulative distributions for
visited nodes, path length, and path turns.
"""

from __future__ import annotations

import argparse
import importlib.util
from pathlib import Path
from typing import Iterable, List, Sequence, Tuple

import matplotlib

matplotlib.use("Agg")
import numpy as np  # noqa: E402
import matplotlib.pyplot as plt  # noqa: E402
import pandas as pd  # noqa: E402


SCRIPT_DIR = Path(__file__).resolve().parent
MODEL_FILE = SCRIPT_DIR.parent.parent / "model.py"
DEFAULT_DATASET = SCRIPT_DIR.parent / "maze_data.csv"
DEFAULT_OUTPUT_DIR = SCRIPT_DIR / "plots"
ALGORITHMS: Tuple[str, ...] = ("BFS", "DFS", "Astar")
ALGORITHM_METRIC_SUMMARIES: Tuple[Tuple[str, str, str], ...] = (
    ("VisitedNodes", "Visited nodes by algorithm", "Visited nodes (count)"),
    ("PathLength", "Path length by algorithm", "Path length (cells)"),
    ("PathTurns", "Path turns by algorithm", "Path turns (count)"),
)
ALGORITHM_COLORS: Tuple[str, ...] = ("#1f77b4", "#ff7f0e", "#2ca02c")
ALGORITHM_DISTRIBUTION_SUFFIXES: Tuple[Tuple[str, str], ...] = (
    ("TimeSeconds", "Time (seconds)"),
    ("VisitedNodes", "Visited nodes"),
    ("ExpandedNodes", "Expanded nodes"),
)
METRIC_COLORS: Tuple[str, ...] = ("#1f77b4", "#ff7f0e", "#2ca02c")


def load_model_columns() -> tuple[List[str], List[str]]:
    """Load feature and target column names from model.py."""
    if not MODEL_FILE.exists():
        raise FileNotFoundError(f"Unable to locate model.py at {MODEL_FILE}")

    spec = importlib.util.spec_from_file_location("maze_model_for_viz", MODEL_FILE)
    if spec is None or spec.loader is None:
        raise ImportError(f"Unable to create import spec for {MODEL_FILE}")

    module = importlib.util.module_from_spec(spec)
    spec.loader.exec_module(module)  # type: ignore[call-arg]

    features = list(getattr(module, "FEATURE_COLUMNS", []))
    targets = list(getattr(module, "TARGET_COLUMNS", ["BFS_TimeSeconds", "DFS_TimeSeconds", "Astar_TimeSeconds"]))
    if not features:
        raise AttributeError("FEATURE_COLUMNS could not be loaded from model.py")
    return features, targets


def parse_args(feature_columns: Sequence[str]) -> argparse.Namespace:
    parser = argparse.ArgumentParser(description="Generate time-comparison plots for maze metrics.")
    parser.add_argument(
        "--csv",
        type=Path,
        default=DEFAULT_DATASET,
        help=f"Path to the maze dataset CSV (default: {DEFAULT_DATASET})",
    )
    parser.add_argument(
        "--output-dir",
        type=Path,
        default=DEFAULT_OUTPUT_DIR,
        help=f"Directory to store generated plots (default: {DEFAULT_OUTPUT_DIR})",
    )
    parser.add_argument(
        "--metrics",
        nargs="*",
        help="Optional subset of metric column names to visualise; defaults to all feature columns.",
    )
    parser.add_argument(
        "--rolling-window",
        type=int,
        default=0,
        help="Window size for rolling averages on continuous plots (0 = auto)",
    )
    return parser.parse_args()


def ensure_columns(frame: pd.DataFrame, columns: Iterable[str], kind: str) -> List[str]:
    missing = [col for col in columns if col not in frame.columns]
    if missing:
        raise ValueError(f"The dataset is missing required {kind} columns: {missing}")
    return list(columns)


def algorithm_metric_columns(suffix: str) -> List[str]:
    return [f"{algorithm}_{suffix}" for algorithm in ALGORITHMS]


def is_discrete(series: pd.Series) -> bool:
    unique_count = series.nunique(dropna=True)
    if pd.api.types.is_integer_dtype(series) or pd.api.types.is_bool_dtype(series):
        return unique_count <= 25
    return unique_count <= 10


def friendly_label(column: str) -> str:
    return column.replace("_TimeSeconds", "").replace("_", " ").strip()


def plot_discrete(ax: plt.Axes, frame: pd.DataFrame, metric: str, time_columns: Sequence[str]) -> None:
    grouped = frame.groupby(metric, dropna=False)[list(time_columns)].mean().reset_index()
    grouped = grouped.sort_values(metric)
    for column in time_columns:
        ax.plot(grouped[metric], grouped[column], marker="o", label=friendly_label(column))


def plot_continuous(ax: plt.Axes, frame: pd.DataFrame, metric: str, time_columns: Sequence[str], rolling_window: int) -> None:
    sorted_frame = frame.sort_values(metric)
    window = rolling_window
    if window <= 0:
        window = max(3, len(sorted_frame) // 25)

    for column in time_columns:
        trend = sorted_frame[column]
        if window > 1:
            trend = (
                sorted_frame[column]
                .rolling(window=window, min_periods=max(2, window // 2), center=True)
                .mean()
            )
        ax.plot(sorted_frame[metric], trend, linewidth=1.6, label=friendly_label(column))


def generate_algorithm_metric_plots(frame: pd.DataFrame, output_dir: Path) -> None:
    for suffix, title, ylabel in ALGORITHM_METRIC_SUMMARIES:
        metric_columns = algorithm_metric_columns(suffix)
        try:
            ensure_columns(frame, metric_columns, "algorithm metric")
        except ValueError as exc:
            print(f"[!] Skipping {suffix}: {exc}")
            continue

        distributions = [frame[column].dropna().to_numpy() for column in metric_columns]
        if not any(len(values) for values in distributions):
            print(f"[!] Skipping {suffix}: no data available after dropping NaN values.")
            continue

        fig, ax = plt.subplots(figsize=(8, 5))
        for algorithm, values, color in zip(ALGORITHMS, distributions, ALGORITHM_COLORS):
            if len(values) == 0:
                continue
            sorted_values = np.sort(values)
            if len(sorted_values) == 1:
                x_positions = np.array([0.0, 1.0])
                y_values = np.repeat(sorted_values[0], 2)
            else:
                x_positions = np.linspace(0.0, 1.0, num=len(sorted_values))
                y_values = sorted_values
            ax.plot(
                x_positions,
                y_values,
                label=algorithm,
                linewidth=1.8,
                color=color,
            )

        ax.set_title(title)
        ax.set_xlabel("Cumulative proportion of samples")
        ax.set_ylabel(ylabel)
        ax.legend()
        ax.grid(True, alpha=0.3)
        fig.tight_layout()

        output_file = output_dir / f"Algorithm_{suffix}_comparison.png"
        fig.savefig(output_file, dpi=200)
        plt.close(fig)
        print(f"[+] Saved algorithm metric plot: {output_file}")


def generate_individual_algorithm_plots(frame: pd.DataFrame, output_dir: Path) -> None:
    for algorithm in ALGORITHMS:
        fig, ax = plt.subplots(figsize=(8, 5))
        plotted = False
        for (suffix_name, pretty_label), metric_color in zip(ALGORITHM_DISTRIBUTION_SUFFIXES, METRIC_COLORS):
            column = f"{algorithm}_{suffix_name}"
            if column not in frame.columns:
                print(f"[!] Skipping missing column for {algorithm}: {column}")
                continue
            values = frame[column].dropna().to_numpy()
            if len(values) == 0:
                print(f"[!] Skipping {column}: no non-NaN values.")
                continue
            sorted_values = np.sort(values)
            x_positions = np.linspace(0.0, 1.0, num=len(sorted_values), endpoint=True)
            ax.plot(
                x_positions,
                sorted_values,
                linewidth=1.8,
                color=metric_color,
                label=pretty_label,
            )
            plotted = True

        if not plotted:
            plt.close(fig)
            print(f"[!] No data available to plot distribution for {algorithm}.")
            continue

        ax.set_title(f"{algorithm} metric distributions")
        ax.set_xlabel("Cumulative proportion of samples")
        ax.set_ylabel("Metric value")
        ax.legend()
        ax.grid(True, alpha=0.3)
        fig.tight_layout()

        output_file = output_dir / f"{algorithm}_distribution.png"
        fig.savefig(output_file, dpi=200)
        plt.close(fig)
        print(f"[+] Saved algorithm distribution plot: {output_file}")


def main() -> None:
    feature_columns, target_columns = load_model_columns()
    args = parse_args(feature_columns)

    dataset_path: Path = args.csv
    if not dataset_path.exists():
        raise FileNotFoundError(f"Dataset not found at {dataset_path}")

    df = pd.read_csv(dataset_path)
    metrics_to_plot = feature_columns if not args.metrics else args.metrics
    time_columns = ensure_columns(df, target_columns, "target")

    args.output_dir.mkdir(parents=True, exist_ok=True)

    for metric in metrics_to_plot:
        if metric not in df.columns:
            print(f"[!] Skipping missing metric column: {metric}")
            continue

        metric_frame = df[[metric, *time_columns]].dropna()
        if metric_frame.empty or metric_frame[metric].nunique(dropna=True) <= 1:
            print(f"[!] Skipping {metric}: insufficient variability for plotting.")
            continue

        fig, ax = plt.subplots(figsize=(8, 5))
        if is_discrete(metric_frame[metric]):
            plot_discrete(ax, metric_frame, metric, time_columns)
            ax.set_ylabel("Average time (seconds)")
        else:
            plot_continuous(ax, metric_frame, metric, time_columns, rolling_window=args.rolling_window)
            ax.set_ylabel("Time (seconds)")

        ax.set_title(f"{metric} vs algorithm time")
        ax.set_xlabel(metric)
        ax.legend()
        ax.grid(True, alpha=0.3)
        fig.tight_layout()

        output_file = args.output_dir / f"{metric}_time_comparison.png"
        fig.savefig(output_file, dpi=200)
        plt.close(fig)
        print(f"[+] Saved plot: {output_file}")

    generate_algorithm_metric_plots(df, args.output_dir)
    generate_individual_algorithm_plots(df, args.output_dir)


if __name__ == "__main__":
    main()
