#!/usr/bin/env python3
from __future__ import annotations

import argparse
import csv
import json
import math
from pathlib import Path
from statistics import fmean
from typing import Dict, Iterable, List, Tuple


def parse_args() -> argparse.Namespace:
    default_csv = Path(__file__).resolve().parent / "maze_data.csv"
    parser = argparse.ArgumentParser(
        description=(
            "Summarize how maze metrics relate to algorithm execution times. "
            "Prints correlations and quantile-based averages for each metric."
        )
    )
    parser.add_argument("--csv", type=Path, default=default_csv, help="Path to the maze data CSV file.")
    parser.add_argument(
        "--bins",
        type=int,
        default=5,
        help="Number of quantile groups to compute per metric (defaults to 5, set 0 to skip bin summaries).",
    )
    parser.add_argument(
        "--json-out",
        type=Path,
        help="Optional path to store the computed summaries as JSON for further analysis or plotting.",
    )
    return parser.parse_args()


def try_parse_float(raw_value: str | None) -> float | None:
    if raw_value is None:
        return None
    value = raw_value.strip()
    if not value:
        return None
    try:
        return float(value)
    except ValueError:
        return None


def load_dataset(csv_path: Path) -> Tuple[List[Dict[str, float]], List[str]]:
    if not csv_path.exists():
        raise FileNotFoundError(f"Could not locate CSV file at {csv_path}")

    rows: List[Dict[str, float]] = []
    with csv_path.open("r", newline="") as handle:
        reader = csv.DictReader(handle)
        if not reader.fieldnames:
            raise ValueError("CSV file must include a header row.")

        for line_number, row in enumerate(reader, start=2):
            parsed: Dict[str, float] = {}
            for column, value in row.items():
                parsed_value = try_parse_float(value)
                if parsed_value is not None:
                    parsed[column] = parsed_value
            if parsed:
                rows.append(parsed)

    return rows, list(reader.fieldnames)


def determine_columns(fieldnames: Iterable[str]) -> Tuple[List[str], List[str]]:
    time_columns: List[str] = []
    metric_columns: List[str] = []
    for name in fieldnames:
        if name in {"BFS", "DFS", "Astar"} or name.endswith("_2nd"):
            time_columns.append(name)
        else:
            metric_columns.append(name)
    return metric_columns, time_columns


def pearson_correlation(xs: List[float], ys: List[float]) -> float:
    count = len(xs)
    if count < 2:
        return math.nan

    mean_x = fmean(xs)
    mean_y = fmean(ys)
    numerator = 0.0
    sum_sq_x = 0.0
    sum_sq_y = 0.0

    for x, y in zip(xs, ys):
        dx = x - mean_x
        dy = y - mean_y
        numerator += dx * dy
        sum_sq_x += dx * dx
        sum_sq_y += dy * dy

    denominator = math.sqrt(sum_sq_x * sum_sq_y)
    if denominator == 0.0:
        return math.nan
    return numerator / denominator


def sanitize_number(value: float | None) -> float | None:
    if value is None:
        return None
    if math.isnan(value) or math.isinf(value):
        return None
    return value


def build_bins(
    metric_values: List[float],
    time_series: Dict[str, List[float]],
    bin_count: int,
    time_columns: List[str],
) -> List[Dict[str, object]]:
    if bin_count <= 0 or not metric_values:
        return []

    total = len(metric_values)
    actual_bins = min(bin_count, total)
    sorted_indices = sorted(range(total), key=lambda idx: metric_values[idx])
    results: List[Dict[str, object]] = []

    for bin_index in range(actual_bins):
        start = (bin_index * total) // actual_bins
        end = ((bin_index + 1) * total) // actual_bins
        indices = sorted_indices[start:end]
        if not indices:
            continue

        values = [metric_values[idx] for idx in indices]
        averages: Dict[str, float] = {}
        for time_column in time_columns:
            averages[time_column] = sum(time_series[time_column][idx] for idx in indices) / len(indices)

        results.append(
            {
                "index": bin_index + 1,
                "count": len(indices),
                "min": min(values),
                "max": max(values),
                "averages": averages,
            }
        )

    return results


def analyze_metric(
    metric: str,
    rows: List[Dict[str, float]],
    time_columns: List[str],
    bin_count: int,
) -> Dict[str, object] | None:
    metric_values: List[float] = []
    time_series: Dict[str, List[float]] = {name: [] for name in time_columns}

    for row in rows:
        metric_value = row.get(metric)
        if metric_value is None:
            continue

        missing_time = any(row.get(time_column) is None for time_column in time_columns)
        if missing_time:
            continue

        metric_values.append(metric_value)
        for time_column in time_columns:
            time_series[time_column].append(row[time_column])

    if not metric_values:
        return None

    correlations = {
        time_column: sanitize_number(pearson_correlation(metric_values, time_series[time_column]))
        for time_column in time_columns
    }

    summary: Dict[str, object] = {
        "metric": metric,
        "count": len(metric_values),
        "min": min(metric_values),
        "max": max(metric_values),
        "mean": fmean(metric_values),
        "correlation": correlations,
        "bins": build_bins(metric_values, time_series, bin_count, time_columns),
    }
    return summary


def format_optional(value: float | None, digits: int = 3) -> str:
    if value is None:
        return "n/a"
    return f"{value:.{digits}f}"


def print_summary(summaries: List[Dict[str, object]], time_columns: List[str]) -> None:
    if not summaries:
        print("No metrics could be analyzed.")
        return

    print(f"Analyzed {len(summaries)} metrics across {len(time_columns)} algorithm time columns.")
    for summary in summaries:
        print()
        print(f"Metric: {summary['metric']}")
        print(
            f"  Samples: {summary['count']} | "
            f"Min: {summary['min']:.3f} | "
            f"Max: {summary['max']:.3f} | "
            f"Mean: {summary['mean']:.3f}"
        )
        print("  Pearson correlation with execution times:")
        correlations: Dict[str, float | None] = summary["correlation"]  # type: ignore[assignment]
        for time_column in time_columns:
            value = correlations.get(time_column)
            print(f"    {time_column:8s}: {format_optional(value)}")

        bins: List[Dict[str, object]] = summary["bins"]  # type: ignore[assignment]
        if bins:
            print("  Quantile averages:")
            for bin_entry in bins:
                averages: Dict[str, float] = bin_entry["averages"]  # type: ignore[assignment]
                averages_text = ", ".join(
                    f"{time_column}={averages[time_column]:.3f}" for time_column in time_columns
                )
                print(
                    f"    Bin {bin_entry['index']:>2}: "
                    f"[{bin_entry['min']:.3f}, {bin_entry['max']:.3f}] "
                    f"(n={bin_entry['count']}) -> {averages_text}"
                )
        else:
            print("  Quantile averages: skipped (insufficient data or bins disabled).")


def maybe_write_json(summaries: List[Dict[str, object]], time_columns: List[str], output_path: Path, csv_path: Path) -> None:
    payload = {
        "source": str(csv_path),
        "time_columns": time_columns,
        "metrics": summaries,
    }
    output_path.parent.mkdir(parents=True, exist_ok=True)
    with output_path.open("w", encoding="utf-8") as handle:
        json.dump(payload, handle, indent=2, ensure_ascii=False)
    print(f"\nSummary written to {output_path}")


def main() -> None:
    args = parse_args()
    rows, fieldnames = load_dataset(args.csv)
    metric_columns, time_columns = determine_columns(fieldnames)

    if not time_columns:
        raise ValueError("No algorithm execution time columns were found in the CSV file.")
    if not metric_columns:
        raise ValueError("No metric columns were found in the CSV file.")

    summaries: List[Dict[str, object]] = []
    for metric in metric_columns:
        summary = analyze_metric(metric, rows, time_columns, args.bins)
        if summary is not None:
            summaries.append(summary)

    print_summary(summaries, time_columns)

    if args.json_out:
        maybe_write_json(summaries, time_columns, args.json_out, args.csv)


if __name__ == "__main__":
    main()
