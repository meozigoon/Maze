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
import itertools
import re
from pathlib import Path
from typing import Iterable, List, Sequence, Tuple

import matplotlib

matplotlib.use("Agg")
import numpy as np  # noqa: E402
import matplotlib.pyplot as plt  # noqa: E402
import pandas as pd  # noqa: E402


SCRIPT_DIR = Path(__file__).resolve().parent
DEFAULT_DATASET = SCRIPT_DIR.parent / "maze_data.csv"
DEFAULT_OUTPUT_DIR = SCRIPT_DIR / "plots"
DEFAULT_COLOR_CYCLE: Tuple[str, ...] = (
    "#2ca02c",
    "#ff7f0e",
    "#1f77b4",
    "#d62728",
    "#9467bd",
    "#8c564b",
    "#e377c2",
    "#7f7f7f",
    "#bcbd22",
    "#17becf",
)
SUFFIX_METADATA = {
    "TimeSeconds": ("Algorithm time distribution", "Time (seconds)"),
    "VisitedNodes": ("Visited nodes by algorithm", "Visited nodes (count)"),
    "ExpandedNodes": ("Expanded nodes by algorithm", "Expanded nodes (count)"),
    "PathLength": ("Path length by algorithm", "Path length (cells)"),
    "PathTurns": ("Path turns by algorithm", "Path turns (count)"),
    "PathCost": ("Path cost by algorithm", "Path cost"),
}


def parse_args() -> argparse.Namespace:
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
        help="Optional subset of metric column names to visualise; defaults to all non-algorithm columns.",
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


def select_colors(count: int) -> List[str]:
    if count <= len(DEFAULT_COLOR_CYCLE):
        return list(DEFAULT_COLOR_CYCLE[:count])
    return list(itertools.islice(itertools.cycle(DEFAULT_COLOR_CYCLE), count))


def detect_algorithms(frame: pd.DataFrame) -> List[str]:
    candidates = {
        column.split("_", 1)[0]
        for column in frame.columns
        if "_" in column and column.endswith("TimeSeconds")
    }
    algorithms = sorted(candidates)
    if not algorithms:
        raise ValueError("Dataset must include at least one *_TimeSeconds column to identify algorithms.")
    return algorithms


def infer_metric_columns(frame: pd.DataFrame, algorithms: Sequence[str], time_columns: Sequence[str]) -> List[str]:
    algorithm_prefixes = tuple(f"{algorithm}_" for algorithm in algorithms)
    metric_columns = [
        column
        for column in frame.columns
        if column not in time_columns and not column.startswith(algorithm_prefixes)
    ]
    return metric_columns


def shared_algorithm_suffixes(frame: pd.DataFrame, algorithms: Sequence[str]) -> List[str]:
    suffix_sets = []
    for algorithm in algorithms:
        suffixes = {
            column.split("_", 1)[1] for column in frame.columns if column.startswith(f"{algorithm}_")
        }
        if suffixes:
            suffix_sets.append(suffixes)
    if not suffix_sets or len(suffix_sets) != len(algorithms):
        return []
    shared = set.intersection(*suffix_sets)
    return order_suffixes(shared)


def order_suffixes(suffixes: Iterable[str]) -> List[str]:
    priority = ("TimeSeconds", "VisitedNodes", "ExpandedNodes", "PathLength", "PathTurns", "PathCost")
    suffix_list = list(suffixes)
    ordered = [suffix for suffix in priority if suffix in suffix_list]
    remaining = sorted(suffix for suffix in suffix_list if suffix not in priority)
    return ordered + remaining


def humanize_token(name: str) -> str:
    cleaned = name.replace("_", " ").strip()
    spaced = re.sub(r"(?<!^)(?=[A-Z])", " ", cleaned)
    return spaced.strip().title()


def suffix_plot_metadata(suffix: str) -> Tuple[str, str]:
    if suffix in SUFFIX_METADATA:
        return SUFFIX_METADATA[suffix]
    friendly = humanize_token(suffix)
    return (f"{friendly} by algorithm", friendly)


def suffix_legend_label(suffix: str) -> str:
    if suffix in SUFFIX_METADATA:
        return SUFFIX_METADATA[suffix][1]
    return humanize_token(suffix)


def algorithm_metric_columns(algorithms: Sequence[str], suffix: str) -> List[str]:
    return [f"{algorithm}_{suffix}" for algorithm in algorithms]


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


def generate_algorithm_metric_plots(
    frame: pd.DataFrame,
    algorithms: Sequence[str],
    colors: Sequence[str],
    suffixes: Sequence[str],
    output_dir: Path,
) -> None:
    for suffix in suffixes:
        metric_columns = algorithm_metric_columns(algorithms, suffix)
        missing = [column for column in metric_columns if column not in frame.columns]
        if missing:
            print(f"[!] Skipping {suffix}: missing columns {missing}")
            continue

        distributions = [frame[column].dropna().to_numpy() for column in metric_columns]
        if not any(len(values) for values in distributions):
            print(f"[!] Skipping {suffix}: no data available after dropping NaN values.")
            continue

        title, ylabel = suffix_plot_metadata(suffix)
        fig, ax = plt.subplots(figsize=(8, 5))
        for algorithm, values, color in zip(algorithms, distributions, colors):
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


def generate_individual_algorithm_plots(
    frame: pd.DataFrame,
    algorithms: Sequence[str],
    suffixes: Sequence[str],
    output_dir: Path,
) -> None:
    if not suffixes:
        return
    metric_colors = select_colors(len(suffixes))
    for algorithm in algorithms:
        fig, ax = plt.subplots(figsize=(8, 5))
        plotted = False
        for suffix_name, metric_color in zip(suffixes, metric_colors):
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
                label=suffix_legend_label(suffix_name),
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
    args = parse_args()

    dataset_path: Path = args.csv
    if not dataset_path.exists():
        raise FileNotFoundError(f"Dataset not found at {dataset_path}")

    df = pd.read_csv(dataset_path)
    algorithms = detect_algorithms(df)
    time_columns = ensure_columns(df, [f"{algorithm}_TimeSeconds" for algorithm in algorithms], "time")
    metrics_to_plot = args.metrics if args.metrics else infer_metric_columns(df, algorithms, time_columns)
    suffixes = shared_algorithm_suffixes(df, algorithms)
    algorithm_colors = select_colors(len(algorithms))

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

    generate_algorithm_metric_plots(df, algorithms, algorithm_colors, suffixes, args.output_dir)
    generate_individual_algorithm_plots(df, algorithms, suffixes, args.output_dir)


if __name__ == "__main__":
    main()
