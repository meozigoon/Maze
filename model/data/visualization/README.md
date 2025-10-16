# Maze Time Comparison Plots

This mini-project generates line-chart comparisons of BFS, DFS, and A\* execution metrics for every structural feature captured in `maze_data.csv`, along with cumulative line plots that contrast core algorithm metrics (visited nodes, path length, and turns) and per-algorithm distribution charts for their core runtime statistics.

## Quick start

```bash
python -m venv .venv
.venv\Scripts\activate  # On Windows
pip install -r model/data/visualization/requirements.txt
python model/data/visualization/generate_time_comparison.py
```

Plots are written to `model/data/visualization/plots`. Use `--csv` to point at a different dataset and `--metrics` to limit the metrics that get rendered.
The script additionally emits cumulative distribution line charts for visited nodes, path length, and path turns so you can quickly gauge how each algorithm behaves across the dataset, plus individual BFS/DFS/A\* distribution charts covering runtime, visited, and expanded node counts.
