namespace Maze
{
	public partial class Maze : Form
	{
		MazeWall[,] mazeWall = null!;
		bool isSecond = false;
		bool isWrite = false;
		HashSet<Point> prevBfsVisited = [];
		HashSet<Point> prevDfsVisited = [];
		HashSet<Point> prevDijkstraVisited = [];

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
		/// <param name="mazeWalls">미로 칸 배열</param>
		/// <param name="width">너비</param>
		/// <param name="height">높이</param>
		/// <returns>이동 경로</returns>
		private List<Point> StartBFS(Player player, MazeWall[,] mazeWalls, int width, int height)
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
					if (!mazeWalls[current.X, current.Y].isNotConnected[i] && !mazeWalls[current.X, current.Y].closedSides.Contains((MazeWall.Closed)i))
					{
						Point next = new(current.X + directions[i].X, current.Y + directions[i].Y);
						if (!visited.Contains(next))
						{
							bfsQueue.Enqueue(next);
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

		/// <summary>
		/// 2차 BFS 탐색 시작
		/// </summary>
		/// <param name="player">이동 개체</param>
		/// <param name="mazeWalls">미로 배열</param>
		/// <param name="width">너비</param>
		/// <param name="height">높이</param>
		/// <param name="visited1st">1차 탐색 visited</param>
		/// <returns>이동 경로</returns>
		private static List<Point> Start2ndBFS(Player player, MazeWall[,] mazeWalls, int width, int height, HashSet<Point> visited1st)
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
					if (!mazeWalls[current.X, current.Y].isNotConnected[i] && !mazeWalls[current.X, current.Y].closedSides.Contains((MazeWall.Closed)i))
					{
						Point next = new(current.X + directions[i].X, current.Y + directions[i].Y);
						if (!visited.Contains(next) && visited1st.Contains(next))
						{
							bfsQueue.Enqueue(next);
							visited.Add(next);
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
		/// <param name="mazeWalls">미로 배열</param>
		/// <param name="width">너비</param>
		/// <param name="height">높이</param>
		/// <returns>이동 경로</returns>
		private List<Point> StartDFS(Player player, MazeWall[,] mazeWalls, int width, int height)
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
					if (!mazeWalls[current.X, current.Y].isNotConnected[i] && !mazeWalls[current.X, current.Y].closedSides.Contains((MazeWall.Closed)i))
					{
						Point next = new(current.X + directions[i].X, current.Y + directions[i].Y);
						if (!visited.Contains(next))
						{
							dfsStack.Push(next);
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

		/// <summary>
		/// 2차 DFS 탐색 시작
		/// </summary>
		/// <param name="player">이동 개체</param>
		/// <param name="mazeWalls">미로 배열</param>
		/// <param name="width">너비</param>
		/// <param name="height">높이</param>
		/// <param name="visited1st">1차 visited</param>
		/// <returns>이동 경로</returns>
		private static List<Point> Start2ndDFS(Player player, MazeWall[,] mazeWalls, int width, int height, HashSet<Point> visited1st)
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
					if (!mazeWalls[current.X, current.Y].isNotConnected[i] && !mazeWalls[current.X, current.Y].closedSides.Contains((MazeWall.Closed)i))
					{
						Point next = new(current.X + directions[i].X, current.Y + directions[i].Y);
						if (!visited.Contains(next) && visited1st.Contains(next))
						{
							dfsStack.Push(next);
							visited.Add(next);
						}
					}
				}
			}

			return dfsMoves;
		}

		/// <summary>
		/// 다익스트라 탐색
		/// </summary>
		/// <param name="player">이동 개체</param>
		/// <param name="mazeWalls">미로 배열</param>
		/// <param name="width">너비</param>
		/// <param name="height">높이</param>
		/// <returns>이동 경로</returns>
		private static List<Point> StartDijkstra(Player player, MazeWall[,] mazeWalls, int width, int height) // TODO: Implementation
		{
			List<Point> dijkstraMoves = [];

			return dijkstraMoves;
        }

        /// <summary>
        /// 2차 다익스트라 탐색
        /// </summary>
        /// <param name="player">이동 개체</param>
        /// <param name="mazeWalls">미로 배열</param>
        /// <param name="width">너비</param>
        /// <param name="height">높이</param>
        /// <param name="visited1st">1차 visited</param>
        /// <returns>이동 경로</returns>
        private static List<Point> Start2ndDijkstra(Player player, MazeWall[,] mazeWalls, int width, int height, HashSet<Point> visited1st) // TODO: Implementation
		{
			List<Point> dijkstraMoves = [];

			return dijkstraMoves;
        }

        /// <summary>
        /// 움직임 시각화
        /// </summary>
        /// <param name="player">이동 개체</param>
        /// <param name="moveSequence">이동 경로</param>
        /// <param name="mazeWalls">미로 배열</param>
        /// <returns>개체</returns>
        private static Player SimulateMovement(Player player, List<Point> moveSequence, MazeWall[,] mazeWalls)
		{
			for (int moveIndex = 0; moveIndex < moveSequence.Count; moveIndex++)
			{
				player.Move(moveSequence[moveIndex], mazeWalls); // 내부적으로 Path 갱신됨
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
                        mazeWall[nextLocation.X, nextLocation.Y].PlayerOn(players[i].Color.R, players[i].Color.G, players[i].Color.B);
						maxDelay = Math.Max(maxDelay, delay);
					}
				}
				if (maxDelay > 0)
				{
					// await Task.Delay(maxDelay);
				}
				if (!isPlayerMoved)
				{
					BfsTimeLabel.Text = "BFS : " + (BfsCheckBox.Checked ? (time["BFS"] / 1000.0 + " s") : " ");
					DfsTimeLabel.Text = "DFS : " + (DfsCheckBox.Checked ? (time["DFS"] / 1000.0 + " s") : " ");
					DijkstraTimeLabel.Text = "Dijkstra: " + (DijkstraCheckBox.Checked ? (time["Dijkstra"] / 1000.0 + " s") : " ");

                    if (isSecond)
					{
						Bfs2ndTimeLabel.Text = "BFS : " + time["BFS2"] / 1000.0 + " s";
						Dfs2ndTimeLabel.Text = "DFS : " + time["DFS2"] / 1000.0 + " s";
						Dijkstra2ndTimeLabel.Text = "Dijkstra: " + (DijkstraCheckBox.Checked ? (time["Dijkstra2"] / 1000.0 + " s") : " ");
                    }
					else
					{
						Bfs2ndTimeLabel.Text = "BFS : ";
						Dfs2ndTimeLabel.Text = "DFS : ";
						Dijkstra2ndTimeLabel.Text = "Dijkstra: ";
                    }
					break; // 이동할 수 없을 때 종료
				}
			}
		}

		/// <summary>
		/// 미로 생성 (DFS 백트래킹)
		/// </summary>
		/// <param name="mazeWalls">미로 배열</param>
		/// <param name="width">너비</param>
		/// <param name="height">높이</param>
		private static void GenerateMaze(ref MazeWall[,] mazeWalls, int width, int height)
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

					mazeWalls[current.X, current.Y].RemovedClosed((MazeWall.Closed)dir);
					mazeWalls[nx, ny].RemovedClosed((MazeWall.Closed)((dir + 2) % 4));

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
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GenerateMazeButton_Click(object sender, EventArgs e)
		{
			if (mazeWall != null)
			{
				foreach (var wall in mazeWall)
				{
					this.Controls.Remove(wall.pictureBox);
					wall.Dispose();
				}
			}

			mazeWall = new MazeWall[(int)SizeNumericUpDown.Value, (int)SizeNumericUpDown.Value];
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
					mazeWall[i, j].Location = new(i * size + widthStart, j * size + heightStart);
					mazeWall[i, j].AddAllWalls();
					this.Controls.Add(mazeWall[i, j].pictureBox);
				}
			}
			GenerateMaze(ref mazeWall, (int)SizeNumericUpDown.Value, (int)SizeNumericUpDown.Value);
			RunButton.Enabled = true;
		}

		/// <summary>
		/// 사이즈 변경 시 최소 크기 제한
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RunButton_Click(object sender, EventArgs e)
		{
			List<Player> players = [];
			if (!DfsCheckBox.Checked && !BfsCheckBox.Checked)
			{
				MessageBox.Show("알고리즘을 선택해야 합니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (DfsCheckBox.Checked)
			{
				Player player = new(Color.Blue);
				List<Point> dfs = StartDFS(player, mazeWall, (int)SizeNumericUpDown.Value, (int)SizeNumericUpDown.Value);
				player = SimulateMovement(player, dfs, mazeWall);
				player.Path.RemoveAt(0); // 시작 위치 제외
				player.Name = "DFS";
				players.Add(player);
			}
			if (BfsCheckBox.Checked)
			{
				Player player = new(Color.Red);
				List<Point> bfs = StartBFS(player, mazeWall, (int)SizeNumericUpDown.Value, (int)SizeNumericUpDown.Value);
				player = SimulateMovement(player, bfs, mazeWall);
				player.Path.RemoveAt(0); // 시작 위치 제외
				player.Name = "BFS";
				players.Add(player);
			}
			if (DijkstraCheckBox.Checked)
			{
                Player player = new(Color.Green);
                List<Point> dijkstra = StartDijkstra(player, mazeWall, (int)SizeNumericUpDown.Value, (int)SizeNumericUpDown.Value);
                player = SimulateMovement(player, dijkstra, mazeWall);
                player.Path.RemoveAt(0); // 시작 위치 제외
                player.Name = "Dijkstra";
                players.Add(player);
            }
			GenerateMazeButton.Enabled = false;
			RunButton.Enabled = false;

			if (isSecond)
			{
				if (DfsCheckBox.Checked)
				{
					Player player = new(Color.Blue);
					List<Point> dfs = Start2ndDFS(player, mazeWall, (int)SizeNumericUpDown.Value, (int)SizeNumericUpDown.Value, prevDfsVisited);
					player = SimulateMovement(player, dfs, mazeWall);
					player.Path.RemoveAt(0); // 시작 위치 제외
					player.Name = "DFS2";
					players.Add(player);
				}
				if (BfsCheckBox.Checked)
				{
					Player player = new(Color.Red);
					List<Point> bfs = Start2ndBFS(player, mazeWall, (int)SizeNumericUpDown.Value, (int)SizeNumericUpDown.Value, prevBfsVisited);
					player = SimulateMovement(player, bfs, mazeWall);
					player.Path.RemoveAt(0); // 시작 위치 제외
					player.Name = "BFS2";
					players.Add(player);
				}
				if (DijkstraCheckBox.Checked)
				{
					Player player = new(Color.Green);
					List<Point> dijkstra = Start2ndDijkstra(player, mazeWall, (int)SizeNumericUpDown.Value, (int)SizeNumericUpDown.Value, prevDijkstraVisited);
					player = SimulateMovement(player, dijkstra, mazeWall);
					player.Path.RemoveAt(0); // 시작 위치 제외
					player.Name = "Dijkstra2";
					players.Add(player);
                }
            }
            SimulateMove(players, (int)StraightTimePenaltyNumericUpDown.Value, (int)RotationPenaltyNumericUpDown.Value);

			if (isWrite)
			{
				if (!isSecond)
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

			int fork = 0, deadEnd = 0;
            foreach (var cell in mazeWall)
			{
				if (cell.closedSides.Count == 1)
				{
					deadEnd++;
                }
				else if (cell.closedSides.Count == 3)
				{
					fork++;
				}
            }

            decimal[] rowData =
			[
				(int)SizeNumericUpDown.Value,
				(int)StraightTimePenaltyNumericUpDown.Value,
				(int)RotationPenaltyNumericUpDown.Value,
				fork, deadEnd,
                decimal.Parse(BfsTimeLabel.Text.Split(" ")[2]),
				decimal.Parse(DfsTimeLabel.Text.Split(" ")[2]),
				decimal.Parse(DijkstraTimeLabel.Text.Split(" ")[1])
            ];

			if (isSecond)
			{
				rowData = rowData.Append(decimal.Parse(Bfs2ndTimeLabel.Text.Split(" ")[2])).ToArray();
				rowData = rowData.Append(decimal.Parse(Dfs2ndTimeLabel.Text.Split(" ")[2])).ToArray();
                rowData = rowData.Append(decimal.Parse(Dijkstra2ndTimeLabel.Text.Split(" ")[1])).ToArray();
            }

			try
			{
				using StreamWriter sw = new(csvFilePath, append: true);
				sw.WriteLine(string.Join(",", rowData));
			}
			catch (Exception ex)
			{
				MessageBox.Show("CSV 파일 기록 중 오류가 발생했습니다. 경로를 확인하세요.\n" + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
				System.Diagnostics.Process.Start("explorer.exe", csvFilePath);
			}
		}

		/// <summary>
		/// keydown 이벤트 (Enter: 미로 생성, Space: 실행)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LoopLimitNumericUpDown_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				RunLoopButton.PerformClick();
			}
		}

		/// <summary>
		/// 2단계 탐색 버튼 클릭 시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Check2ndRunButton_Click(object sender, EventArgs e)
		{
			isSecond = !isSecond;
			Check2ndRunButton.Text = "2차 " + (isSecond ? "OFF" : "ON");
			Check2ndLabel.Text = isSecond.ToString();
		}

		/// <summary>
		/// 기록 버튼 클릭 시 isWrite 설정
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WriteButton_Click(object sender, EventArgs e)
		{
			isWrite = !isWrite;
			WriteButton.Text = "기록 " + (isWrite ? "OFF" : "ON");
			CheckWriteLabel.Text = isWrite.ToString();
			if (isWrite)
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
						sw.WriteLine("Size,StraightTimePenalty,RotationPenalty,Fork,DeadEnd,BFS,DFS,Dijkstra");
					}
					if (!File.Exists(csv2ndDataFilePath))
					{
						using StreamWriter sw = new(csv2ndDataFilePath, append: false);
						sw.WriteLine("Size,StraightTimePenalty,RotationPenalty,Fork,DeadEnd,BFS,DFS,Dijkstra,BFS_2nd,DFS_2nd,Dijkstra_2nd");
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
	class Player(Color color)
	{
		public string Name { get; set; } = "Player";
		public Point Location { get; set; } = new Point(0, 0);
		public Color Color { get; set; } = color;
		public List<Point> Path { get; set; } = [];

		/// <summary>
		/// player 이동
		/// </summary>
		/// <param name="newLocation">이동할 위치</param>
		/// <param name="mazeWalls">미로 배열 정보</param>
		public void Move(Point newLocation, MazeWall[,] mazeWalls)
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
					if (!mazeWalls[current.X, current.Y].isNotConnected[i] && 
						!mazeWalls[current.X, current.Y].closedSides.Contains((MazeWall.Closed)i))
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
	class MazeWall
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
        public bool IsSideClosed(Closed closed) => closedSides.Contains(closed);

        /// <summary>
        /// player 위치 색칠
        /// </summary>
        /// <param name="R"></param>
        /// <param name="G"></param>
        /// <param name="B"></param>
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