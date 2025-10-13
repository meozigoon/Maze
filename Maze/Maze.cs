namespace Maze
{
    public partial class Maze : Form
    {
        MazeWall[,] mazeWall = null!;
        bool isSecond = false;
        bool isWrite = false;
        HashSet<Point> prevBfsVisited = [];
        HashSet<Point> prevDfsVisited = [];

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

        public static void Delay(int ms)
        {
            DateTime dateTimeNow = DateTime.Now;
            TimeSpan duration = new(0, 0, 0, 0, ms);
            DateTime dateTimeAdd = dateTimeNow.Add(duration);
            while (dateTimeAdd >= dateTimeNow)
            {
                System.Windows.Forms.Application.DoEvents();
                dateTimeNow = DateTime.Now;
            }
            return;
        }

        private List<Point> StartBFS(Player player, MazeWall[,] mazeWalls, int width, int height)
        {
            Queue<Point> queue = [];
            HashSet<Point> visited = [];

            queue.Enqueue(player.Location);
            visited.Add(player.Location);

            // 큐에 넣은 모든 경로를 저장
            List<Point> bfsMoves = [];

            while (queue.Count > 0)
            {
                Point current = queue.Dequeue();
                bfsMoves.Add(current); // 탐색 시도 위치 기록

                if (current == new Point(width - 1, height - 1))
                {
                    break;
                }

                for (int i = 0; i < 4; i++)
                {
                    if (!mazeWalls[current.X, current.Y].isNotConnected[i] && !mazeWalls[current.X, current.Y].closedSides.Contains((MazeWall.Closed)i))
                    {
                        Point next = new(current.X + directions[i].X, current.Y + directions[i].Y);
                        if (!visited.Contains(next))
                        {
                            queue.Enqueue(next);
                            visited.Add(next);
                        }
                    }
                }
            }

            if (isSecond)
            {
                prevBfsVisited = visited; // 1차 BFS에서 방문한 위치 저장
            }

            return bfsMoves;
        }

        private static List<Point> Start2ndBFS(Player player, MazeWall[,] mazeWalls, int width, int height, HashSet<Point> visited1st)
        {
            Queue<Point> queue = [];
            HashSet<Point> visited = [];

            queue.Enqueue(player.Location);
            visited.Add(player.Location);

            // 큐에 넣은 모든 경로를 저장
            List<Point> bfsMoves = [];

            while (queue.Count > 0)
            {
                Point current = queue.Dequeue();
                bfsMoves.Add(current); // 탐색 시도 위치 기록

                if (current == new Point(width - 1, height - 1))
                {
                    break;
                }

                for (int i = 0; i < 4; i++)
                {
                    if (!mazeWalls[current.X, current.Y].isNotConnected[i] && !mazeWalls[current.X, current.Y].closedSides.Contains((MazeWall.Closed)i))
                    {
                        Point next = new(current.X + directions[i].X, current.Y + directions[i].Y);
                        if (!visited.Contains(next) && visited1st.Contains(next))
                        {
                            queue.Enqueue(next);
                            visited.Add(next);
                        }
                    }
                }
            }

            return bfsMoves;
        }

        private List<Point> StartDFS(Player player, MazeWall[,] mazeWalls, int width, int height)
        {
            Stack<Point> stack = [];
            HashSet<Point> visited = [];

            List<Point> dfsMoves = [];

            stack.Push(player.Location);
            visited.Add(player.Location);

            while (stack.Count > 0)
            {
                Point current = stack.Pop();
                dfsMoves.Add(current); // 이동 시도 기록

                if (current == new Point(width - 1, height - 1))
                {
                    break;
                }

                for (int i = 0; i < 4; i++)
                {
                    if (!mazeWalls[current.X, current.Y].isNotConnected[i] && !mazeWalls[current.X, current.Y].closedSides.Contains((MazeWall.Closed)i))
                    {
                        Point next = new(current.X + directions[i].X, current.Y + directions[i].Y);
                        if (!visited.Contains(next))
                        {
                            stack.Push(next);
                            visited.Add(next);
                        }
                    }
                }
            }

            if (isSecond)
            {
                prevDfsVisited = visited;
            }

            return dfsMoves;
        }

        private static List<Point> Start2ndDFS(Player player, MazeWall[,] mazeWalls, int width, int height, HashSet<Point> visited1st)
        {
            Stack<Point> stack = [];
            HashSet<Point> visited = [];

            List<Point> dfsMoves = [];

            stack.Push(player.Location);
            visited.Add(player.Location);

            while (stack.Count > 0)
            {
                Point current = stack.Pop();
                dfsMoves.Add(current); // 이동 시도 기록

                if (current == new Point(width - 1, height - 1))
                {
                    break;
                }

                for (int i = 0; i < 4; i++)
                {
                    if (!mazeWalls[current.X, current.Y].isNotConnected[i] && !mazeWalls[current.X, current.Y].closedSides.Contains((MazeWall.Closed)i))
                    {
                        Point next = new(current.X + directions[i].X, current.Y + directions[i].Y);
                        if (!visited.Contains(next) && visited1st.Contains(next))
                        {
                            stack.Push(next);
                            visited.Add(next);
                        }
                    }
                }
            }

            return dfsMoves;
        }

        private static Player SimulateMovement(Player player, List<Point> moveSequence, MazeWall[,] mazeWalls)
        {
            for (int moveIndex = 0; moveIndex < moveSequence.Count; moveIndex++)
            {
                player.Move(moveSequence[moveIndex], mazeWalls); // 내부적으로 Path 갱신됨
                player.Location = moveSequence[moveIndex];       // 실제 현재 위치 갱신
            }
            return player;
        }

        private void SimulateMove(List<Player> player, int clock)
        {
            int[] time = new int[player.Count];
            while (true)
            {
                int a = 0;
                for (int i = 0; i < player.Count; i++)
                {
                    if (player[i].Path.Count > 0)
                    {
                        time[i]++;
                        a++;
                        Point nextLocation = player[i].Path[0];
                        player[i].Path.RemoveAt(0);
                        player[i].Location = nextLocation;
                        mazeWall[nextLocation.X, nextLocation.Y].PlayerOn(player[i].Color.R, player[i].Color.G, player[i].Color.B);
                    }
                }
                Delay(clock);
                if (a == 0)
                {
                    BfsTimeLabel.Text = "BFS : " + time[0] * clock / 1000.0 + " s";
                    DfsTimeLabel.Text = "DFS : " + time[1] * clock / 1000.0 + " s";
                    if (isSecond)
                    {
                        Bfs2ndTimeLabel.Text = "BFS : " + time[2] * clock / 1000.0 + " s";
                        Dfs2ndTimeLabel.Text = "DFS : " + time[3] * clock / 1000.0 + " s";
                    }
                    else
                    {
                        Bfs2ndTimeLabel.Text = "BFS : ";
                        Dfs2ndTimeLabel.Text = "DFS : ";
                    }
                    break; // 이동할 수 없을 때 종료
                }
            }
        }

        private static void GenerateMaze(ref MazeWall[,] mazeWalls, int width, int height)
        {
            bool[,] visited = new bool[width, height];
            Stack<Point> stack = [];
            Random rand = new();

            Point start = new(0, 0);
            stack.Push(start);
            visited[start.X, start.Y] = true;

            while (stack.Count > 0)
            {
                Point current = stack.Peek();
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

                    mazeWalls[current.X, current.Y].RemovedClosed((MazeWall.Closed)dir);
                    mazeWalls[nx, ny].RemovedClosed((MazeWall.Closed)((dir + 2) % 4));

                    visited[nx, ny] = true;
                    stack.Push(new Point(nx, ny));
                }
                else
                {
                    stack.Pop();
                }
            }
        }

        private void GenerateMazeButton_Click(object sender, EventArgs e)
        {
            if (mazeWall != null)
            {
                foreach (var wall in mazeWall)
                {
                    this.Controls.Remove(wall.pictureBox);
                }
            }

            mazeWall = new MazeWall[(int)SizeNumericUpDown.Value, (int)SizeNumericUpDown.Value];
            int size;
            int Widthstart;
            int Heightstart;
            if (Size.Width < Size.Height)
            {
                size = 4 * Size.Width / ((int)SizeNumericUpDown.Value * 5);
                Heightstart = SizeNumericUpDown.Location.Y + SizeNumericUpDown.Size.Height + Size.Height / 10;
                Widthstart = Size.Width / 2 - Convert.ToInt32(size * (int)SizeNumericUpDown.Value / 2.0);
            }
            else
            {
                size = 4 * (Size.Height - SizeNumericUpDown.Location.Y - SizeNumericUpDown.Size.Height) / ((int)SizeNumericUpDown.Value * 5);
                Heightstart = SizeNumericUpDown.Location.Y + SizeNumericUpDown.Size.Height + Size.Height / 10;
                Widthstart = Size.Width / 2 - Convert.ToInt32(size * (int)SizeNumericUpDown.Value / 2.0);
            }

            for (int i = 0; i < mazeWall.GetLength(0); i++)
            {
                for (int j = 0; j < mazeWall.GetLength(1); j++)
                {
                    mazeWall[i, j] = new MazeWall();
                    if (i == 0 && j == 0)
                    {
                        mazeWall[i, j].isNotConnected[0] = true; // Top
                        mazeWall[i, j].isNotConnected[3] = true; // Left
                    }
                    else if (i == 0 && j == mazeWall.GetLength(1) - 1)
                    {
                        mazeWall[i, j].isNotConnected[2] = true; // Bottom
                        mazeWall[i, j].isNotConnected[3] = true; // Left
                    }
                    else if (i == mazeWall.GetLength(0) - 1 && j == 0)
                    {
                        mazeWall[i, j].isNotConnected[0] = true; // Top
                        mazeWall[i, j].isNotConnected[1] = true; // Right
                    }
                    else if (i == mazeWall.GetLength(0) - 1 && j == mazeWall.GetLength(1) - 1)
                    {
                        mazeWall[i, j].isNotConnected[2] = true; // Bottom
                        mazeWall[i, j].isNotConnected[1] = true; // Right
                    }
                    else if (i == 0)
                    {
                        mazeWall[i, j].isNotConnected[3] = true; // Left
                    }
                    else if (j == 0)
                    {
                        mazeWall[i, j].isNotConnected[0] = true; // Top
                    }
                    else if (i == mazeWall.GetLength(0) - 1)
                    {
                        mazeWall[i, j].isNotConnected[1] = true; // Right
                    }
                    else if (j == mazeWall.GetLength(1) - 1)
                    {
                        mazeWall[i, j].isNotConnected[2] = true; // Bottom
                    }
                    mazeWall[i, j].Size = new(size, size);
                    mazeWall[i, j].Location = new(i * size + Widthstart, j * size + Heightstart);
                    mazeWall[i, j].AddAllWalls();
                    this.Controls.Add(mazeWall[i, j].pictureBox);
                }
            }
            GenerateMaze(ref mazeWall, (int)SizeNumericUpDown.Value, (int)SizeNumericUpDown.Value);
            RunButton.Enabled = true;
        }

        private void Maze_SizeChanged(object sender, EventArgs e)
        {
            if (this.Size.Width < 800)
            {
                this.Size = new(800, Size.Height);
            }
            if (this.Size.Height < 800)
            {
                this.Size = new(Size.Width, 800);
            }
        }

        private void SizeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (SizeNumericUpDown.Value < 2)
            {
                SizeNumericUpDown.Value = 2;
            }
            LoopLimitLabel.Text = "n = " + SizeNumericUpDown.Value.ToString();
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            List<Player> players =
            [
                new(Color.Red),
                new(Color.Blue)
            ];
            List<Point> bfs = StartBFS(players[0], mazeWall, (int)SizeNumericUpDown.Value, (int)SizeNumericUpDown.Value);
            List<Point> dfs = StartDFS(players[1], mazeWall, (int)SizeNumericUpDown.Value, (int)SizeNumericUpDown.Value);
            players[0] = SimulateMovement(players[0], bfs, mazeWall);
            players[1] = SimulateMovement(players[1], dfs, mazeWall);
            players[0].Path.RemoveAt(0); // 시작 위치 제외
            players[1].Path.RemoveAt(0); // 시작 위치 제외
            GenerateMazeButton.Enabled = false;
            RunButton.Enabled = false;

            if (isSecond)
            {
                players.Add(new Player(Color.Red));
                players.Add(new Player(Color.Blue));
                List<Point> bfs2 = Start2ndBFS(players[2], mazeWall, (int)SizeNumericUpDown.Value, (int)SizeNumericUpDown.Value, prevDfsVisited);
                List<Point> dfs2 = Start2ndDFS(players[3], mazeWall, (int)SizeNumericUpDown.Value, (int)SizeNumericUpDown.Value, prevBfsVisited);
                players[2] = SimulateMovement(players[2], bfs2, mazeWall);
                players[3] = SimulateMovement(players[3], dfs2, mazeWall);
                players[2].Path.RemoveAt(0); // 시작 위치 제외
                players[3].Path.RemoveAt(0); // 시작 위치 제외
            }
            SimulateMove(players, (int)StraightTimePenaltyNumericUpDown.Value);

            if (isWrite)
            {
                if (!isSecond)
                {
                    WriteCsv(@"\data\maze_data.csv");
                }
                else
                {
                    WriteCsv(@"\data\2_maze_data.csv");
                }
            }
            GenerateMazeButton.Enabled = true;
        }

        private void WriteCsv(string csvFilePath)
        {
            if (csvFilePath == null || csvFilePath == "")
            {
                return;
            }

            decimal[] rowData = [(int)SizeNumericUpDown.Value, (int)StraightTimePenaltyNumericUpDown.Value, decimal.Parse(BfsTimeLabel.Text.Split(" ")[2]), decimal.Parse(DfsTimeLabel.Text.Split(" ")[2])]; ;
            if (isSecond)
            {
                rowData = [(int)SizeNumericUpDown.Value, (int)StraightTimePenaltyNumericUpDown.Value, decimal.Parse(BfsTimeLabel.Text.Split(" ")[2]), decimal.Parse(DfsTimeLabel.Text.Split(" ")[2]), decimal.Parse(Bfs2ndTimeLabel.Text.Split(" ")[2]), decimal.Parse(Dfs2ndTimeLabel.Text.Split(" ")[2])];
            }

            using StreamWriter sw = new(csvFilePath, append: true);
            sw.WriteLine(string.Join(",", rowData));
        }

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

        private void RunLoopButton_Click(object sender, EventArgs e)
        {
            RunLoopButton.Enabled = false;
            SizeNumericUpDown.Enabled = false;
            StraightTimePenaltyNumericUpDown.Enabled = false;
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
            LoopLimitNumericUpDown.Enabled = true;
            LoopCountLabel.Text = "횟수 = 0";
        }

        private void LoopLimitNumericUpDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RunLoopButton.PerformClick();
            }
        }

        private void Check2ndRunButton_Click(object sender, EventArgs e)
        {
            isSecond = !isSecond;
            Check2ndRunButton.Text = "2차 " + (isSecond ? "OFF" : "ON");
            Check2ndLabel.Text = isSecond.ToString();
        }

        private void WriteButton_Click(object sender, EventArgs e)
        {
            isWrite = !isWrite;
            WriteButton.Text = "기록 " + (isWrite ? "OFF" : "ON");
            CheckWriteLabel.Text = isWrite.ToString();
        }
    }

    class Player(Color color)
    {
        public Point Location { get; set; } = new Point(0, 0);
        public Color Color { get; set; } = color;
        public List<Point> Path { get; set; } = [];

        public void Move(Point newLocation, MazeWall[,] mazeWalls)
        {
            Dictionary<Point, Point?> parent = [];
            Queue<Point> queue = new();
            HashSet<Point> visited = [];

            queue.Enqueue(Location);
            visited.Add(Location);
            parent[Location] = null;

            while (queue.Count > 0)
            {
                Point current = queue.Dequeue();

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
                    if (!mazeWalls[current.X, current.Y].isNotConnected[i] && !mazeWalls[current.X, current.Y].closedSides.Contains((MazeWall.Closed)i))
                    {
                        Point next = new(current.X + directions[i].X, current.Y + directions[i].Y);
                        if (!visited.Contains(next))
                        {
                            queue.Enqueue(next);
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

    class MazeWall
    {
        public List<Closed> closedSides;
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

        public MazeWall()
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

        public void AddAllWalls()
        {
            AddClosed(Closed.Top);
            AddClosed(Closed.Right);
            AddClosed(Closed.Bottom);
            AddClosed(Closed.Left);
        }

        public bool AddClosed(Closed closed)
        {
            if (closedSides.Contains(closed))
            {
                return false;
            }

            closedSides.Add(closed);
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

        public void PlayerOn(int R, int G, int B)
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                using Brush transparentRed = new SolidBrush(Color.FromArgb(25, R, G, B));
                g.FillRectangle(transparentRed, 80, 80, 640, 640);
            }
            pictureBox.Image = bitmap;
        }

        public bool RemovedClosed(Closed closed)
        {
            if (!closedSides.Contains(closed))
            {
                return false;
            }

            closedSides.Remove(closed);
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
    }
}
