namespace Maze
{
    internal static class MazeHelpers
    {

        private static int CountTurns(IReadOnlyList<Point> path)
        {
            ArgumentNullException.ThrowIfNull(path);
            if (path.Count < 3)
            {
                return 0;
            }

            int turns = 0;
            Point previousDirection = new(path[1].X - path[0].X, path[1].Y - path[0].Y);

            for (int i = 2; i < path.Count; i++)
            {
                Point direction = new(path[i].X - path[i - 1].X, path[i].Y - path[i - 1].Y);
                if (direction != previousDirection)
                {
                    turns++;
                }
                previousDirection = direction;
            }

            return turns;
        }
    }
}