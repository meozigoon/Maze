namespace Maze
{
    public partial class Maze : Form
    {
        MazeCell[,] mazeCell = null!;
        HashSet<Point> prevBfsVisited = [];
        HashSet<Point> prevDfsVisited = [];
        HashSet<Point> prevAstarVisited = [];

        readonly string dataFolderPath = Directory.GetCurrentDirectory() + @"\data";
        readonly string csvDataFilePath = Directory.GetCurrentDirectory() + @"\data\maze_data.csv";
        readonly string csv2ndDataFilePath = Directory.GetCurrentDirectory() + @"\data\2_maze_data.csv";

        enum Direction
        {
            Top = 0,
            Right = 1,
            Bottom = 2,
            Left = 3
        }

        private static readonly Point[] directions =
        [
            new(0, -1), // Top
			new(1, 0),  // Right
			new(0, 1),  // Bottom
			new(-1, 0), // Left
		];

        public Maze()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 미로에서 BFS 탐색 시작
        /// </summary>
        /// <param name="player">이동 개체</param>
        /// <param name="mazeCells">미로 칸 배열</param>
        /// <param name="width">너비</param>
        /// <param name="height">높이</param>
        /// <returns>이동 경로</returns>
        private List<Point> StartBFS(Player player, MazeCell[,] mazeCells, int width, int height)
        {
            Queue<Point> bfsQueue = [];
            HashSet<Point> visited = [];

            bfsQueue.Enqueue(player.Location);
            visited.Add(player.Location);

            // 큐에 넣은 모든 경로를 저장
            List<Point> bfsMoves = [];

            while (bfsQueue.Count > 0)
            {
                Point current = bfsQueue.Dequeue();
                bfsMoves.Add(current); // 탐색 시도 위치 기록

                if (current == new Point(width - 1, height - 1))
                {
                    break;
                }

                for (int i = 0; i < 4; i++)
                {
                    if (!mazeCells[current.X, current.Y].isNotConnected[i] &&
                        !mazeCells[current.X, current.Y].closedSides.Contains((MazeCell.Closed)i))
                    {
                        Point next = new(current.X + directions[i].X, current.Y + directions[i].Y);
                        if (visited.Add(next))
                        {
                            bfsQueue.Enqueue(next);
                        }
                    }
                }
            }

            if (Run2ndCheckBox.Checked)
            {
                prevBfsVisited = visited; // 1차 BFS에서 방문한 위치 저장
            }

            return bfsMoves;
        }

        /// <summary>
        /// 2차 BFS 탐색 시작
        /// </summary>
        /// <param name="player">이동 개체</param>
        /// <param name="mazeCells">미로 배열</param>
        /// <param name="width">너비</param>
        /// <param name="height">높이</param>
        /// <param name="visited1st">1차 탐색 visited</param>
        /// <returns>이동 경로</returns>
        private static List<Point> Start2ndBFS(Player player, MazeCell[,] mazeCells, int width, int height, HashSet<Point> visited1st)
        {
            Queue<Point> bfsQueue = [];
            HashSet<Point> visited = [];

            bfsQueue.Enqueue(player.Location);
            visited.Add(player.Location);

            // 큐에 넣은 모든 경로를 저장
            List<Point> bfsMoves = [];

            while (bfsQueue.Count > 0)
            {
                Point current = bfsQueue.Dequeue();
                bfsMoves.Add(current); // 탐색 시도 위치 기록

                if (current == new Point(width - 1, height - 1))
                {
                    break;
                }

                for (int i = 0; i < 4; i++)
                {
                    if (!mazeCells[current.X, current.Y].isNotConnected[i] &&
                        !mazeCells[current.X, current.Y].closedSides.Contains((MazeCell.Closed)i))
                    {
                        Point next = new(current.X + directions[i].X, current.Y + directions[i].Y);
                        if (visited.Add(next) && visited1st.Contains(next))
                        {
                            bfsQueue.Enqueue(next);
                        }
                    }
                }
            }

            return bfsMoves;
        }

        /// <summary>
        /// DFS 탐색 시작
        /// </summary>
        /// <param name="player">이동 개체</param>
        /// <param name="mazeCells">미로 배열</param>
        /// <param name="width">너비</param>
        /// <param name="height">높이</param>
        /// <returns>이동 경로</returns>
        private List<Point> StartDFS(Player player, MazeCell[,] mazeCells, int width, int height)
        {
            Stack<Point> dfsStack = [];
            HashSet<Point> visited = [];

            List<Point> dfsMoves = [];

            dfsStack.Push(player.Location);
            visited.Add(player.Location);

            while (dfsStack.Count > 0)
            {
                Point current = dfsStack.Pop();
                dfsMoves.Add(current); // 이동 시도 기록

                if (current == new Point(width - 1, height - 1))
                {
                    break;
                }

                for (int i = 0; i < 4; i++)
                {
                    if (!mazeCells[current.X, current.Y].isNotConnected[i] &&
                        !mazeCells[current.X, current.Y].closedSides.Contains((MazeCell.Closed)i))
                    {
                        Point next = new(current.X + directions[i].X, current.Y + directions[i].Y);
                        if (visited.Add(next))
                        {
                            dfsStack.Push(next);
                        }
                    }
                }
            }

            if (Run2ndCheckBox.Checked)
            {
                prevDfsVisited = visited;
            }

            return dfsMoves;
        }

        /// <summary>
        /// 2차 DFS 탐색 시작
        /// </summary>
        /// <param name="player">이동 개체</param>
        /// <param name="mazeCells">미로 배열</param>
        /// <param name="width">너비</param>
        /// <param name="height">높이</param>
        /// <param name="visited1st">1차 visited</param>
        /// <returns>이동 경로</returns>
        private static List<Point> Start2ndDFS(Player player, MazeCell[,] mazeCells, int width, int height, HashSet<Point> visited1st)
        {
            Stack<Point> dfsStack = [];
            HashSet<Point> visited = [];

            List<Point> dfsMoves = [];

            dfsStack.Push(player.Location);
            visited.Add(player.Location);

            while (dfsStack.Count > 0)
            {
                Point current = dfsStack.Pop();
                dfsMoves.Add(current); // 이동 시도 기록

                if (current == new Point(width - 1, height - 1))
                {
                    break;
                }

                for (int i = 0; i < 4; i++)
                {
                    if (!mazeCells[current.X, current.Y].isNotConnected[i] &&
                        !mazeCells[current.X, current.Y].closedSides.Contains((MazeCell.Closed)i))
                    {
                        Point next = new(current.X + directions[i].X, current.Y + directions[i].Y);
                        if (!visited.Contains(next) && visited1st.Contains(next))
                        {
                            dfsStack.Push(next);
                            visited.Add(next);
                        }
                        if (visited.Add(next) && visited1st.Contains(next))
                        {
                            dfsStack.Push(next);
                        }
                    }
                }
            }

            return dfsMoves;
        }

        /// <summary>
        /// A* 탐색
        /// </summary>
        /// <param name="player">이동 개체</param>
        /// <param name="mazeCells">미로 배열</param>
        /// <param name="width">너비</param>
        /// <param name="height">높이</param>
        /// <returns>이동 경로</returns>
        private List<Point> StartAstar(Player player, MazeCell[,] mazeCells, int width, int height)
        {
            PriorityQueue<Point, int> openSet = new();
            Dictionary<Point, int> gScore = [];
            HashSet<Point> closedSet = [];
            List<Point> astarMoves = [];

            Point start = player.Location;
            Point goal = new(width - 1, height - 1);
            gScore[start] = 0;
            openSet.Enqueue(start, CalculateManhattanDistance(start, goal));

            while (openSet.Count > 0)
            {
                Point current = openSet.Dequeue();

                if (!closedSet.Add(current))
                {
                    continue;
                }

                astarMoves.Add(current);

                if (current == goal)
                {
                    if (Run2ndCheckBox.Checked)
                    {
                        prevAstarVisited = closedSet;
                    }

                    return astarMoves;
                }

                for (int i = 0; i < 4; i++)
                {
                    if (!mazeCells[current.X, current.Y].isNotConnected[i] &&
                        !mazeCells[current.X, current.Y].closedSides.Contains((MazeCell.Closed)i))
                    {
                        Point next = new(current.X + directions[i].X, current.Y + directions[i].Y);
                        int tentativeGScore = gScore[current] + 1;

                        if (!gScore.TryGetValue(next, out int existingGScore) || tentativeGScore < existingGScore)
                        {
                            gScore[next] = tentativeGScore;
                            int priority = tentativeGScore + CalculateManhattanDistance(next, goal);
                            openSet.Enqueue(next, priority);
                        }
                    }
                }
            }

            if (Run2ndCheckBox.Checked)
            {
                prevAstarVisited = closedSet;
            }

            return astarMoves;
        }

        /// <summary>
        /// 2차 A* 탐색
        /// </summary>
        /// <param name="player">이동 개체</param>
        /// <param name="mazeCells">미로 배열</param>
        /// <param name="width">너비</param>
        /// <param name="height">높이</param>
        /// <param name="visited1st">1차 visited</param>
        /// <returns>이동 경로</returns>
        private static List<Point> Start2ndAstar(Player player, MazeCell[,] mazeCells, int width, int height, HashSet<Point> visited1st)
        {
            PriorityQueue<Point, int> openSet = new();
            Dictionary<Point, int> gScore = [];
            HashSet<Point> closedSet = [];
            List<Point> astarMoves = [];

            Point start = player.Location;
            Point goal = new(width - 1, height - 1);
            if (!visited1st.Contains(start))
            {
                return astarMoves;
            }

            gScore[start] = 0;
            openSet.Enqueue(start, CalculateManhattanDistance(start, goal));

            while (openSet.Count > 0)
            {
                Point current = openSet.Dequeue();

                if (!visited1st.Contains(current) || !closedSet.Add(current))
                {
                    continue;
                }

                astarMoves.Add(current);

                if (current == goal)
                {
                    return astarMoves;
                }

                for (int i = 0; i < 4; i++)
                {
                    if (!mazeCells[current.X, current.Y].isNotConnected[i] &&
                        !mazeCells[current.X, current.Y].closedSides.Contains((MazeCell.Closed)i))
                    {
                        Point next = new(current.X + directions[i].X, current.Y + directions[i].Y);

                        if (!visited1st.Contains(next))
                        {
                            continue;
                        }

                        int tentativeGScore = gScore[current] + 1;

                        if (!gScore.TryGetValue(next, out int existingGScore) || tentativeGScore < existingGScore)
                        {
                            gScore[next] = tentativeGScore;
                            int priority = tentativeGScore + CalculateManhattanDistance(next, goal);
                            openSet.Enqueue(next, priority);
                        }
                    }
                }
            }

            return astarMoves;
        }

        /// <summary>
        /// 맨해튼 거리 계산
        /// </summary>
        /// <param name="point">현재 위치</param>
        /// <param name="goal">목표 위치</param>
        /// <returns>거리</returns>
        private static int CalculateManhattanDistance(Point point, Point goal)
        {
            return Math.Abs(point.X - goal.X) + Math.Abs(point.Y - goal.Y);
        }

        /// <summary>
        /// 움직임 시각화
        /// </summary>
        /// <param name="player">이동 개체</param>
        /// <param name="moveSequence">이동 경로</param>
        /// <param name="mazeCells">미로 배열</param>
        /// <returns>개체</returns>
        private static Player SimulateMovement(Player player, List<Point> moveSequence, MazeCell[,] mazeCells)
        {
            for (int moveIndex = 0; moveIndex < moveSequence.Count; moveIndex++)
            {
                player.Move(moveSequence[moveIndex], mazeCells); // 내부적으로 Path 갱신됨
                player.Location = moveSequence[moveIndex];       // 실제 현재 위치 갱신
            }
            return player;
        }

        /// <summary>
        /// 움직임 경로 시각화 및 시간 표시
        /// </summary>
        /// <param name="players">이동 개체 List</param>
        /// <param name="straightPenalty">직선 이동 delay</param>
        /// <param name="rotationPenalty">회전 이동 delay</param>
        private void SimulateMove(List<Player> players, int straightPenalty, int rotationPenalty)
        {
            Point?[] previousDirections = new Point?[players.Count];
            Dictionary<string, int> time = [];
            foreach (var player in players)
            {
                time[player.Name] = 0;
            }

            while (true)
            {
                bool isPlayerMoved = false;
                int maxDelay = 0;
                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i].Path.Count > 0)
                    {
                        isPlayerMoved = true;
                        Point currentLocation = players[i].Location;
                        Point nextLocation = players[i].Path[0];
                        players[i].Path.RemoveAt(0);
                        Point movement = new(nextLocation.X - currentLocation.X, nextLocation.Y - currentLocation.Y);
                        bool hasMovement = movement != Point.Empty;
                        bool isRotation = hasMovement && previousDirections[i].HasValue && previousDirections[i] != movement;
                        int delay = hasMovement ? (isRotation ? rotationPenalty : straightPenalty) : 0;
                        time[players[i].Name] += delay;
                        if (hasMovement)
                        {
                            previousDirections[i] = movement;
                        }
                        players[i].Location = nextLocation;
                        mazeCell[nextLocation.X, nextLocation.Y].PlayerOn(players[i].Color.R, players[i].Color.G, players[i].Color.B);
                        maxDelay = Math.Max(maxDelay, delay);
                    }
                }
                if (maxDelay > 0)
                {
                    if (VisualDisplayCheckBox.Checked)
                    {
                        var timer = new System.Windows.Forms.Timer
                        {
                            Interval = maxDelay
                        };
                        timer.Tick += (s, e) =>
                        {
                            timer.Stop();
                        };
                        timer.Start();
                        while (timer.Enabled)
                        {
                            Application.DoEvents();
                        }
                    }
                }

                if (!isPlayerMoved)
                {
                    BfsTimeLabel.Text = "BFS : " + (BfsCheckBox.Checked ? (time["BFS"] / 1000.0 + " s") : " ");
                    DfsTimeLabel.Text = "DFS : " + (DfsCheckBox.Checked ? (time["DFS"] / 1000.0 + " s") : " ");
                    AstarTimeLabel.Text = "A* : " + (AstarCheckBox.Checked ? (time["Astar"] / 1000.0 + " s") : " ");

                    if (Run2ndCheckBox.Checked)
                    {
                        Bfs2ndTimeLabel.Text = "BFS : " + time["BFS2"] / 1000.0 + " s";
                        Dfs2ndTimeLabel.Text = "DFS : " + time["DFS2"] / 1000.0 + " s";
                        Astar2ndTimeLabel.Text = "A* : " + (AstarCheckBox.Checked ? (time["Astar2"] / 1000.0 + " s") : " ");
                    }
                    else
                    {
                        Bfs2ndTimeLabel.Text = "BFS : ";
                        Dfs2ndTimeLabel.Text = "DFS : ";
                        Astar2ndTimeLabel.Text = "A* : ";
                    }
                    break; // 이동할 수 없을 때 종료
                }
            }
        }

        /// <summary>
        /// 미로 생성 (DFS 백트래킹)
        /// </summary>
        /// <param name="mazeCells">미로 배열</param>
        /// <param name="width">너비</param>
        /// <param name="height">높이</param>
        private static void GenerateMaze(ref MazeCell[,] mazeCells, int width, int height)
        {
            bool[,] visited = new bool[width, height];
            Stack<Point> dfsStack = [];
            Random rand = new();

            Point start = new(0, 0);
            dfsStack.Push(start);
            visited[start.X, start.Y] = true;

            while (dfsStack.Count > 0)
            {
                Point current = dfsStack.Peek();
                List<int> availableDirections = [];

                for (int i = 0; i < 4; i++)
                {
                    int nx = current.X + directions[i].X;
                    int ny = current.Y + directions[i].Y;
                    if (nx >= 0 && nx < width && ny >= 0 && ny < height && !visited[nx, ny])
                    {
                        availableDirections.Add(i);
                    }
                }

                if (availableDirections.Count > 0)
                {
                    int dir = availableDirections[rand.Next(availableDirections.Count)];
                    int nx = current.X + directions[dir].X;
                    int ny = current.Y + directions[dir].Y;

                    mazeCells[current.X, current.Y].RemovedClosed((MazeCell.Closed)dir);
                    mazeCells[nx, ny].RemovedClosed((MazeCell.Closed)((dir + 2) % 4));

                    visited[nx, ny] = true;
                    dfsStack.Push(new Point(nx, ny));
                }
                else
                {
                    dfsStack.Pop();
                }
            }
        }

        /// <summary>
        /// 미로 생성 버튼 클릭 시
        /// </summary>
        /// <param name="sender">이벤트를 발생시킨 컨트롤</param>
        /// <param name="e">이벤트 데이터</param>
        private void GenerateMazeButton_Click(object sender, EventArgs e)
        {
            if (mazeCell != null)
            {
                foreach (var cell in mazeCell)
                {
                    this.Controls.Remove(cell.pictureBox);
                    cell.Dispose();
                }
            }

            mazeCell = new MazeCell[(int)SizeNumericUpDown.Value, (int)SizeNumericUpDown.Value];
            int size;
            int widthStart;
            int heightStart;
            if (Size.Width < Size.Height)
            {
                size = 4 * Size.Width / ((int)SizeNumericUpDown.Value * 5);
                heightStart = SizeNumericUpDown.Location.Y + SizeNumericUpDown.Size.Height + Size.Height / 10;
                widthStart = Size.Width / 2 - Convert.ToInt32(size * (int)SizeNumericUpDown.Value / 2.0);
            }
            else
            {
                size = 4 * (Size.Height - SizeNumericUpDown.Location.Y - SizeNumericUpDown.Size.Height) / ((int)SizeNumericUpDown.Value * 5);
                heightStart = SizeNumericUpDown.Location.Y + SizeNumericUpDown.Size.Height + Size.Height / 10;
                widthStart = Size.Width / 2 - Convert.ToInt32(size * (int)SizeNumericUpDown.Value / 2.0);
            }

            for (int i = 0; i < mazeCell.GetLength(0); i++)
            {
                for (int j = 0; j < mazeCell.GetLength(1); j++)
                {
                    mazeCell[i, j] = new MazeCell();
                    if (i == 0 && j == 0)
                    {
                        mazeCell[i, j].isNotConnected[0] = true; // Top
                        mazeCell[i, j].isNotConnected[3] = true; // Left
                    }
                    else if (i == 0 && j == mazeCell.GetLength(1) - 1)
                    {
                        mazeCell[i, j].isNotConnected[2] = true; // Bottom
                        mazeCell[i, j].isNotConnected[3] = true; // Left
                    }
                    else if (i == mazeCell.GetLength(0) - 1 && j == 0)
                    {
                        mazeCell[i, j].isNotConnected[0] = true; // Top
                        mazeCell[i, j].isNotConnected[1] = true; // Right
                    }
                    else if (i == mazeCell.GetLength(0) - 1 && j == mazeCell.GetLength(1) - 1)
                    {
                        mazeCell[i, j].isNotConnected[2] = true; // Bottom
                        mazeCell[i, j].isNotConnected[1] = true; // Right
                    }
                    else if (i == 0)
                    {
                        mazeCell[i, j].isNotConnected[3] = true; // Left
                    }
                    else if (j == 0)
                    {
                        mazeCell[i, j].isNotConnected[0] = true; // Top
                    }
                    else if (i == mazeCell.GetLength(0) - 1)
                    {
                        mazeCell[i, j].isNotConnected[1] = true; // Right
                    }
                    else if (j == mazeCell.GetLength(1) - 1)
                    {
                        mazeCell[i, j].isNotConnected[2] = true; // Bottom
                    }
                    mazeCell[i, j].Size = new(size, size);
                    mazeCell[i, j].Location = new(i * size + widthStart, j * size + heightStart);
                    mazeCell[i, j].AddAllWalls();
                    this.Controls.Add(mazeCell[i, j].pictureBox);
                }
            }
            GenerateMaze(ref mazeCell, (int)SizeNumericUpDown.Value, (int)SizeNumericUpDown.Value);
            RunButton.Enabled = true;
        }

        /// <summary>
        /// 사이즈 변경 시 최소 크기 제한
        /// </summary>
        /// <param name="sender">이벤트를 발생시킨 컨트롤</param>
        /// <param name="e">이벤트 인자</param>
        private void Maze_SizeChanged(object sender, EventArgs e)
        {
            if (this.Size.Width < 1200)
            {
                this.Size = new(1200, Size.Height);
            }
            if (this.Size.Height < 800)
            {
                this.Size = new(Size.Width, 800);
            }
        }

        /// <summary>
        /// 크기 변경 시 최소 크기 제한 및 라벨 갱신
        /// </summary>
        /// <param name="sender">이벤트를 발생시킨 컨트롤</param>
        /// <param name="e">이벤트 인자</param>
        private void SizeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (SizeNumericUpDown.Value < 2)
            {
                SizeNumericUpDown.Value = 2;
            }
            LoopLimitLabel.Text = "n = " + SizeNumericUpDown.Value.ToString();
        }

        /// <summary>
        /// 실행 버튼 클릭 시
        /// </summary>
        /// <param name="sender">이벤트를 발생시킨 컨트롤</param>
        /// <param name="e">이벤트 인자</param>
        private void RunButton_Click(object sender, EventArgs e)
        {
            List<Player> players = [];
            if (!DfsCheckBox.Checked && !BfsCheckBox.Checked && !AstarCheckBox.Checked)
            {
                MessageBox.Show("알고리즘을 선택해야 합니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (DfsCheckBox.Checked)
            {
                Player player = new(Color.Blue, "DFS");
                List<Point> dfs = StartDFS(player, mazeCell, (int)SizeNumericUpDown.Value, (int)SizeNumericUpDown.Value);
                player = SimulateMovement(player, dfs, mazeCell);
                player.Path.RemoveAt(0); // 시작 위치 제외
                players.Add(player);
            }
            if (BfsCheckBox.Checked)
            {
                Player player = new(Color.Red, "BFS");
                List<Point> bfs = StartBFS(player, mazeCell, (int)SizeNumericUpDown.Value, (int)SizeNumericUpDown.Value);
                player = SimulateMovement(player, bfs, mazeCell);
                player.Path.RemoveAt(0); // 시작 위치 제외
                players.Add(player);
            }
            if (AstarCheckBox.Checked)
            {
                Player player = new(Color.Green, "Astar");
                List<Point> astar = StartAstar(player, mazeCell, (int)SizeNumericUpDown.Value, (int)SizeNumericUpDown.Value);
                player = SimulateMovement(player, astar, mazeCell);
                player.Path.RemoveAt(0); // 시작 위치 제외
                players.Add(player);
            }
            GenerateMazeButton.Enabled = false;
            RunButton.Enabled = false;

            if (Run2ndCheckBox.Checked)
            {
                if (DfsCheckBox.Checked)
                {
                    Player player = new(Color.Blue, "DFS2");
                    List<Point> dfs = Start2ndDFS(player, mazeCell, (int)SizeNumericUpDown.Value, (int)SizeNumericUpDown.Value, prevDfsVisited);
                    player = SimulateMovement(player, dfs, mazeCell);
                    player.Path.RemoveAt(0); // 시작 위치 제외
                    players.Add(player);
                }
                if (BfsCheckBox.Checked)
                {
                    Player player = new(Color.Red, "BFS2");
                    List<Point> bfs = Start2ndBFS(player, mazeCell, (int)SizeNumericUpDown.Value, (int)SizeNumericUpDown.Value, prevBfsVisited);
                    player = SimulateMovement(player, bfs, mazeCell);
                    player.Path.RemoveAt(0); // 시작 위치 제외
                    players.Add(player);
                }
                if (AstarCheckBox.Checked)
                {
                    Player player = new(Color.Green, "Astar2");
                    List<Point> astar = Start2ndAstar(player, mazeCell, (int)SizeNumericUpDown.Value, (int)SizeNumericUpDown.Value, prevAstarVisited);
                    player = SimulateMovement(player, astar, mazeCell);
                    player.Path.RemoveAt(0); // 시작 위치 제외
                    players.Add(player);
                }
            }
            SimulateMove(players, (int)StraightTimePenaltyNumericUpDown.Value, (int)RotationPenaltyNumericUpDown.Value);

            if (WriteCheckBox.Checked)
            {
                if (!Run2ndCheckBox.Checked)
                {
                    WriteCsv(csvDataFilePath);
                }
                else
                {
                    WriteCsv(csv2ndDataFilePath);
                }
            }
            GenerateMazeButton.Enabled = true;
        }

        /// <summary>
        /// csv 파일에 결과 기록
        /// </summary>
        /// <param name="csvFilePath">csv 파일 경로</param>
        private void WriteCsv(string csvFilePath)
        {
            if (string.IsNullOrEmpty(csvFilePath))
            {
                return;
            }

            int width = mazeCell.GetLength(0);
            int height = mazeCell.GetLength(1);
            int fork = 0;
            int deadEnd = 0;

            int[,] openMask = new int[width, height];
            int[] openDegreeCounts = new int[5];
            int goalAlignedEdges = 0;
            int totalOpenEdges = 0;
            Point goal = new(width - 1, height - 1);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    MazeCell cell = mazeCell[x, y];
                    int mask = 0;

                    for (int dir = 0; dir < directions.Length; dir++)
                    {
                        if (cell.isNotConnected[dir] || cell.closedSides.Contains((MazeCell.Closed)dir))
                        {
                            continue;
                        }

                        mask |= 1 << dir;
                        totalOpenEdges++;

                        int nx = x + directions[dir].X;
                        int ny = y + directions[dir].Y;
                        int currentDistance = Math.Abs(goal.X - x) + Math.Abs(goal.Y - y);
                        int nextDistance = Math.Abs(goal.X - nx) + Math.Abs(goal.Y - ny);
                        if (nextDistance < currentDistance)
                        {
                            goalAlignedEdges++;
                        }
                    }

                    openMask[x, y] = mask;

                    int openCount = CountBits(mask);
                    if (openCount >= 0 && openCount < openDegreeCounts.Length)
                    {
                        openDegreeCounts[openCount]++;
                    }

                    if (cell.closedSides.Count == 1)
                    {
                        deadEnd++;
                    }
                    else if (cell.closedSides.Count == 3)
                    {
                        fork++;
                    }
                }
            }

            int longestCorridor = 0;
            int deadEndChainSum = 0;
            int deadEndChainMax = 0;
            int deadEndChainCount = 0;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int mask = openMask[x, y];
                    int openCount = CountBits(mask);

                    if (openCount == 1)
                    {
                        int depth = ComputeDeadEndChainDepth(x, y, openMask, width, height);
                        deadEndChainSum += depth;
                        if (depth > deadEndChainMax)
                        {
                            deadEndChainMax = depth;
                        }
                        deadEndChainCount++;
                    }

                    if (IsCorridorMask(mask))
                    {
                        GetCorridorDirections(mask, out int dirA, out int dirB);
                        int corridorLength =
                            1
                            + CountCorridorCellsInDirection(x, y, dirA, openMask, width, height)
                            + CountCorridorCellsInDirection(x, y, dirB, openMask, width, height);

                        if (corridorLength > longestCorridor)
                        {
                            longestCorridor = corridorLength;
                        }
                    }
                }
            }

            decimal deadEndChainAverage = deadEndChainCount > 0 ? (decimal)deadEndChainSum / deadEndChainCount : 0m;
            decimal deadEndChainAverageRounded = decimal.Round(deadEndChainAverage, 2, MidpointRounding.AwayFromZero);
            decimal goalDirectionOpenness = totalOpenEdges > 0 ? (decimal)goalAlignedEdges / totalOpenEdges : 0m;

            List<decimal> rowData =
            [
                (decimal)SizeNumericUpDown.Value,
                (decimal)StraightTimePenaltyNumericUpDown.Value,
                (decimal)RotationPenaltyNumericUpDown.Value,
                fork,
                deadEnd,
                openDegreeCounts.Length > 1 ? openDegreeCounts[1] : 0,
                openDegreeCounts.Length > 2 ? openDegreeCounts[2] : 0,
                openDegreeCounts.Length > 3 ? openDegreeCounts[3] : 0,
                openDegreeCounts.Length > 4 ? openDegreeCounts[4] : 0,
                longestCorridor,
                deadEndChainAverageRounded,
                deadEndChainMax,
                goalDirectionOpenness,
                decimal.Parse(BfsTimeLabel.Text.Split(" ")[2]),
                decimal.Parse(DfsTimeLabel.Text.Split(" ")[2]),
                decimal.Parse(AstarTimeLabel.Text.Split(" ")[2])
            ];

            if (Run2ndCheckBox.Checked)
            {
                rowData.Add(decimal.Parse(Bfs2ndTimeLabel.Text.Split(" ")[2]));
                rowData.Add(decimal.Parse(Dfs2ndTimeLabel.Text.Split(" ")[2]));
                rowData.Add(decimal.Parse(Astar2ndTimeLabel.Text.Split(" ")[2]));
            }

            try
            {
                using StreamWriter sw = new(csvFilePath, append: true);
                string[] formattedRow = new string[rowData.Count];
                for (int i = 0; i < rowData.Count; i++)
                {
                    formattedRow[i] = rowData[i].ToString(System.Globalization.CultureInfo.InvariantCulture);
                }
                sw.WriteLine(string.Join(",", formattedRow));
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSV 파일 기록 중 오류가 발생했습니다. 경로를 확인하세요.\n" + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Process.Start("explorer.exe", csvFilePath);
            }
        }

        /// <summary>
        /// 1인 비트 수 계산
        /// </summary>
        /// <param name="mask">비트 수 확인할 수</param>
        /// <returns>1인 비트 수</returns>
        private static int CountBits(int mask)
        {
            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                if ((mask & (1 << i)) != 0)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// 주어진 비트 마스크가 직선 복도 형태인지 판별합니다.
        /// </summary>
        /// <param name="mask">셀의 네 방향 개방 여부를 나타내는 비트 마스크</param>
        /// <returns>직선 복도라면 true, 아니면 false</returns>
        private static bool IsCorridorMask(int mask)
        {
            bool top = (mask & (1 << 0)) != 0;
            bool right = (mask & (1 << 1)) != 0;
            bool bottom = (mask & (1 << 2)) != 0;
            bool left = (mask & (1 << 3)) != 0;

            return (top && bottom && !right && !left) || (right && left && !top && !bottom);
        }

        /// <summary>
        /// 직선 복도 마스크가 가리키는 두 방향 인덱스를 반환합니다.
        /// </summary>
        /// <param name="mask">현재 셀의 개방 방향을 나타내는 비트 마스크</param>
        /// <param name="dirA">첫 번째 개방 방향 인덱스</param>
        /// <param name="dirB">두 번째 개방 방향 인덱스</param>
        private static void GetCorridorDirections(int mask, out int dirA, out int dirB)
        {
            if ((mask & (1 << 0)) != 0)
            {
                dirA = 0;
                dirB = 2;
            }
            else
            {
                dirA = 1;
                dirB = 3;
            }
        }

        /// <summary>
        /// 특정 방향으로 이어지는 직선 복도의 길이를 계산합니다.
        /// </summary>
        /// <param name="startX">시작 X 좌표</param>
        /// <param name="startY">시작 Y 좌표</param>
        /// <param name="direction">이동할 방향 인덱스</param>
        /// <param name="openMask">셀별 개방 상태 비트 마스크 배열</param>
        /// <param name="width">미로 너비</param>
        /// <param name="height">미로 높이</param>
        /// <returns>연속된 복도 셀 수</returns>
        private static int CountCorridorCellsInDirection(int startX, int startY, int direction, int[,] openMask, int width, int height)
        {
            int count = 0;
            int currentX = startX;
            int currentY = startY;

            while (true)
            {
                int currentMask = openMask[currentX, currentY];
                if ((currentMask & (1 << direction)) == 0)
                {
                    break;
                }

                int nextX = currentX + directions[direction].X;
                int nextY = currentY + directions[direction].Y;
                if (nextX < 0 || nextX >= width || nextY < 0 || nextY >= height)
                {
                    break;
                }

                int nextMask = openMask[nextX, nextY];
                if ((nextMask & (1 << ((direction + 2) % 4))) == 0)
                {
                    break;
                }

                if (!IsCorridorMask(nextMask))
                {
                    break;
                }

                count++;
                currentX = nextX;
                currentY = nextY;
            }

            return count;
        }

        /// <summary>
        /// 막다른 셀에서 갈림길을 만날 때까지의 체인 깊이를 계산합니다.
        /// </summary>
        /// <param name="startX">시작 X 좌표</param>
        /// <param name="startY">시작 Y 좌표</param>
        /// <param name="openMask">셀별 개방 상태 비트 마스크 배열</param>
        /// <param name="width">미로 너비</param>
        /// <param name="height">미로 높이</param>
        /// <returns>막다른길 체인 길이</returns>
        private static int ComputeDeadEndChainDepth(int startX, int startY, int[,] openMask, int width, int height)
        {
            int depth = 0;
            int currentX = startX;
            int currentY = startY;
            int previousDirection = -1;

            while (true)
            {
                int mask = openMask[currentX, currentY];
                int nextDirection = -1;

                for (int dir = 0; dir < 4; dir++)
                {
                    if ((mask & (1 << dir)) == 0)
                    {
                        continue;
                    }

                    if (previousDirection != -1 && dir == (previousDirection + 2) % 4)
                    {
                        continue;
                    }

                    nextDirection = dir;
                    break;
                }

                if (nextDirection == -1)
                {
                    break;
                }

                int nextX = currentX + directions[nextDirection].X;
                int nextY = currentY + directions[nextDirection].Y;
                if (nextX < 0 || nextX >= width || nextY < 0 || nextY >= height)
                {
                    break;
                }

                int nextMask = openMask[nextX, nextY];
                if ((nextMask & (1 << ((nextDirection + 2) % 4))) == 0)
                {
                    break;
                }

                depth++;

                if (CountBits(nextMask) != 2)
                {
                    break;
                }

                previousDirection = nextDirection;
                currentX = nextX;
                currentY = nextY;
            }

            return depth;
        }

        /// <summary>
        /// keydown 이벤트 (Enter: 미로 생성, Space: 실행)
        /// </summary>
        /// <param name="sender">이벤트를 발생시킨 컨트롤</param>
        /// <param name="e">이벤트 인자</param>
        private void SizeNumericUpDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GenerateMazeButton.PerformClick();
            }
            else if (e.KeyCode == Keys.Space)
            {
                RunButton.PerformClick();
            }
        }

        /// <summary>
        /// 반복 실행 버튼 클릭 시
        /// </summary>
        /// <param name="sender">이벤트를 발생시킨 컨트롤</param>
        /// <param name="e">이벤트 인자</param>
        private void RunLoopButton_Click(object sender, EventArgs e)
        {
            RunLoopButton.Enabled = false;
            SizeNumericUpDown.Enabled = false;
            StraightTimePenaltyNumericUpDown.Enabled = false;
            RotationPenaltyNumericUpDown.Enabled = false;
            LoopLimitNumericUpDown.Enabled = false;
            for (int i = 0; i < LoopLimitNumericUpDown.Value; i++)
            {
                LoopCountLabel.Text = "횟수 = " + (i + 1).ToString();
                GenerateMazeButton.PerformClick();
                RunButton.PerformClick();
            }
            RunLoopButton.Enabled = true;
            SizeNumericUpDown.Enabled = true;
            StraightTimePenaltyNumericUpDown.Enabled = true;
            RotationPenaltyNumericUpDown.Enabled = true;
            LoopLimitNumericUpDown.Enabled = true;
            LoopCountLabel.Text = "횟수 = 0";
        }

        /// <summary>
        /// 반복 실행 횟수 keydown 이벤트 (Enter: 반복 실행)
        /// </summary>
        /// <param name="sender">이벤트를 발생시킨 컨트롤</param>
        /// <param name="e">이벤트 인자</param>
        private void LoopLimitNumericUpDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RunLoopButton.PerformClick();
            }
        }

        /// <summary>
        /// 기록 체크 박스 상태 변경 시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WriteCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (WriteCheckBox.Checked)
            {
                try
                {
                    if (!Directory.Exists(dataFolderPath))
                    {
                        Directory.CreateDirectory(dataFolderPath);
                    }
                    if (!File.Exists(csvDataFilePath))
                    {
                        using StreamWriter sw = new(csvDataFilePath, append: false);
                        sw.WriteLine("Size,StraightTimePenalty,RotationPenalty,Fork,DeadEnd,BranchDeg1,BranchDeg2,BranchDeg3,BranchDeg4,LongestStraightCorridor,DeadEndChainDepthAvg,DeadEndChainDepthMax,GoalDirectionOpenness,BFS,DFS,Astar");
                    }
                    if (!File.Exists(csv2ndDataFilePath))
                    {
                        using StreamWriter sw = new(csv2ndDataFilePath, append: false);
                        sw.WriteLine("Size,StraightTimePenalty,RotationPenalty,Fork,DeadEnd,BranchDeg1,BranchDeg2,BranchDeg3,BranchDeg4,LongestStraightCorridor,DeadEndChainDepthAvg,DeadEndChainDepthMax,GoalDirectionOpenness,BFS,DFS,Astar,BFS_2nd,DFS_2nd,Astar_2nd");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("CSV 파일 오류가 발생했습니다. 경로를 확인하세요.\n" + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Diagnostics.Process.Start("explorer.exe", dataFolderPath);
                    this.Close();
                }
            }
        }
    }

    /// <summary>
    /// player 클래스
    /// </summary>
    /// <param name="color">player 색상</param>
    /// <param name="name">player 이름</param>
    class Player(Color color, string name)
	{
		public string Name { get; set; } = name;
		public Point Location { get; set; } = new Point(0, 0);
		public Color Color { get; set; } = color;
		public List<Point> Path { get; set; } = [];

		/// <summary>
		/// player 이동
		/// </summary>
		/// <param name="newLocation">이동할 위치</param>
		/// <param name="mazeCells">미로 배열 정보</param>
		public void Move(Point newLocation, MazeCell[,] mazeCells)
		{
			Dictionary<Point, Point?> parent = [];
			Queue<Point> bfsQueue = new();
			HashSet<Point> visited = [];

			bfsQueue.Enqueue(Location);
			visited.Add(Location);
			parent[Location] = null;

			while (bfsQueue.Count > 0)
			{
				Point current = bfsQueue.Dequeue();

				if (current == newLocation)
				{
					break;
				}

				Point[] directions =
				[
					new(0, -1),
					new(1, 0),
					new(0, 1),
					new(-1, 0)
				];

				for (int i = 0; i < 4; i++)
				{
					if (!mazeCells[current.X, current.Y].isNotConnected[i] && 
						!mazeCells[current.X, current.Y].closedSides.Contains((MazeCell.Closed)i))
					{
						Point next = new(current.X + directions[i].X, current.Y + directions[i].Y);
						if (!visited.Contains(next))
						{
							bfsQueue.Enqueue(next);
							visited.Add(next);
							parent[next] = current;
						}
					}
				}
			}

			// 경로 역추적
			Point? trace = newLocation;
			Stack<Point> reversedPath = [];
			while (trace != null)
			{
				reversedPath.Push(trace.Value);
				trace = parent.TryGetValue(trace.Value, out Point? value) ? value : null;
			}

			while (reversedPath.Count > 0)
			{
				if (reversedPath.Peek() == Location && reversedPath.Peek() != new Point(0, 0))
				{
					reversedPath.Pop(); // 현재 위치는 제외
				}
				Path.Add(reversedPath.Pop());
			}
		}
    }

	/// <summary>
	/// 미로 배열 클래스
	/// </summary>
	class MazeCell
	{
		public HashSet<Closed> closedSides;
		public Bitmap bitmap;
		public PictureBox pictureBox;
		public bool[] isNotConnected = [false, false, false, false]; // Top, Right, Bottom, Left

		public enum Closed
		{
			Top,
			Right,
			Bottom,
			Left
		}

		public MazeCell()
		{
			closedSides = [];
			bitmap = new Bitmap(800, 800);
			using (Graphics g = Graphics.FromImage(bitmap))
			{
				g.Clear(Color.White);
			}
			pictureBox = new PictureBox
			{
				Image = bitmap,
				SizeMode = PictureBoxSizeMode.StretchImage
			};
		}

		public Point Location
		{
			get { return pictureBox.Location; }
			set { pictureBox.Location = value; }
		}

		public Size Size
		{
			get { return pictureBox.Size; }
			set { pictureBox.Size = value; }
		}

		/// <summary>
		/// 모든 방향 벽 추가
		/// </summary>
		public void AddAllWalls()
		{
			AddClosed(Closed.Top);
			AddClosed(Closed.Right);
			AddClosed(Closed.Bottom);
			AddClosed(Closed.Left);
		}

		/// <summary>
		/// 닫힌 벽 추가
		/// </summary>
		/// <param name="closed">닫힌 벽 방향</param>
		/// <returns>닫힘 추가 성공 여부</returns>
		public bool AddClosed(Closed closed)
		{
			if (!closedSides.Add(closed))
			{
				return false;
			}

			using (Graphics g = Graphics.FromImage(bitmap))
			{
				using (Pen thickPen = new(Color.Gray, 40))
				{
					switch (closed)
					{
						case Closed.Top:
							g.DrawLine(thickPen, new(0, 20), new(800, 20));
							break;
						case Closed.Right:
							g.DrawLine(thickPen, new(780, 0), new(780, 800));
							break;
						case Closed.Bottom:
							g.DrawLine(thickPen, new(0, 780), new(800, 780));
							break;
						case Closed.Left:
							g.DrawLine(thickPen, new(20, 0), new(20, 800));
							break;
					}
				}

				using (Pen thickPen = new(Color.Gray, 40))
				{
					switch (closed)
					{
						case Closed.Top when isNotConnected[0]:
							g.DrawLine(thickPen, new(0, 60), new(800, 60));
							break;
						case Closed.Right when isNotConnected[1]:
							g.DrawLine(thickPen, new(740, 0), new(740, 800));
							break;
						case Closed.Bottom when isNotConnected[2]:
							g.DrawLine(thickPen, new(0, 740), new(800, 740));
							break;
						case Closed.Left when isNotConnected[3]:
							g.DrawLine(thickPen, new(60, 0), new(60, 800));
							break;
					}
				}
			}

			pictureBox.Image = bitmap;
			return true;
		}

        /// <summary>
        /// 지정된 방향이 닫혀 있는지 여부를 반환
        /// </summary>
        public bool IsWallClosed(Closed closed) => closedSides.Contains(closed);

        /// <summary>
        /// 플레이어가 있는 영역을 지정된 색상으로 음영 처리합니다.
        /// </summary>
        /// <param name="R">강조 색상의 빨강 채널 값</param>
        /// <param name="G">강조 색상의 초록 채널 값</param>
        /// <param name="B">강조 색상의 파랑 채널 값</param>
        public void PlayerOn(int R, int G, int B)
		{
			using (Graphics g = Graphics.FromImage(bitmap))
			{
				using Brush transparentRed = new SolidBrush(Color.FromArgb(25, R, G, B));
				g.FillRectangle(transparentRed, 80, 80, 640, 640);
			}
			pictureBox.Image = bitmap;
		}

		/// <summary>
		/// 벽 제거
		/// </summary>
		/// <param name="closed">제거할 벽</param>
		/// <returns>제거 성공 여부</returns>
		public bool RemovedClosed(Closed closed)
		{
			if (!closedSides.Remove(closed))
			{
				return false;
			}

			using (Graphics g = Graphics.FromImage(bitmap))
			{
				g.Clear(Color.White);

				using (Pen thickPen = new(Color.Gray, 40))
				{
					foreach (var side in closedSides)
					{
						switch (side)
						{
							case Closed.Top:
								g.DrawLine(thickPen, new(0, 20), new(800, 20));
								break;
							case Closed.Right:
								g.DrawLine(thickPen, new(780, 0), new(780, 800));
								break;
							case Closed.Bottom:
								g.DrawLine(thickPen, new(0, 780), new(800, 780));
								break;
							case Closed.Left:
								g.DrawLine(thickPen, new(20, 0), new(20, 800));
								break;
						}
					}
				}

				using (Pen thickPen = new(Color.Gray, 40))
				{
					foreach (var side in closedSides)
					{
						switch (side)
						{
							case Closed.Top when isNotConnected[0]:
								g.DrawLine(thickPen, new(0, 60), new(800, 60));
								break;
							case Closed.Right when isNotConnected[1]:
								g.DrawLine(thickPen, new(740, 0), new(740, 800));
								break;
							case Closed.Bottom when isNotConnected[2]:
								g.DrawLine(thickPen, new(0, 740), new(800, 740));
								break;
							case Closed.Left when isNotConnected[3]:
								g.DrawLine(thickPen, new(60, 0), new(60, 800));
								break;
						}
					}
				}
			}

			pictureBox.Image = bitmap;
			return true;
		}

		/// <summary>
		/// 해제
		/// </summary>
        public void Dispose()
        {
            pictureBox.Dispose();
            bitmap.Dispose();
        }
    }
}
