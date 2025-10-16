namespace Maze
{
    partial class Maze
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SizeNumericUpDown = new NumericUpDown();
            GenerateMazeButton = new Button();
            RunButton = new Button();
            BfsTimeLabel = new Label();
            DfsTimeLabel = new Label();
            MazeSizeLabel = new Label();
            StraightTimePenalty = new Label();
            StraightTimePenaltyNumericUpDown = new NumericUpDown();
            RotationPenaltyLabel = new Label();
            RotationPenaltyNumericUpDown = new NumericUpDown();
            LoopLimitLabel = new Label();
            RunLoopButton = new Button();
            LoopLimitNumericUpDown = new NumericUpDown();
            LoopCountLabel = new Label();
            Dfs2ndTimeLabel = new Label();
            Bfs2ndTimeLabel = new Label();
            LoopGroupBox = new GroupBox();
            Run2ndGroupBox = new GroupBox();
            SecondAlgorithmTableLayoutPanel = new TableLayoutPanel();
            FirstAlgorithmHeaderLabel = new Label();
            SecondBfsHeaderLabel = new Label();
            SecondDfsHeaderLabel = new Label();
            SecondAstarHeaderLabel = new Label();
            BfsFirstHeaderLabel = new Label();
            DfsFirstHeaderLabel = new Label();
            AstarFirstHeaderLabel = new Label();
            BfsFirstBfsSecondCheckBox = new CheckBox();
            BfsFirstDfsSecondCheckBox = new CheckBox();
            BfsFirstAstarSecondCheckBox = new CheckBox();
            DfsFirstBfsSecondCheckBox = new CheckBox();
            DfsFirstDfsSecondCheckBox = new CheckBox();
            DfsFirstAstarSecondCheckBox = new CheckBox();
            AstarFirstBfsSecondCheckBox = new CheckBox();
            AstarFirstDfsSecondCheckBox = new CheckBox();
            AstarFirstAstarSecondCheckBox = new CheckBox();
            Run2ndCheckBox = new CheckBox();
            Astar2ndTimeLabel = new Label();
            DfsCheckBox = new CheckBox();
            AlgorithmsGroupBox = new GroupBox();
            BfsCheckBox = new CheckBox();
            AstarCheckBox = new CheckBox();
            AstarTimeLabel = new Label();
            VisualDisplayCheckBox = new CheckBox();
            WriteCheckBox = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)SizeNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)StraightTimePenaltyNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RotationPenaltyNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LoopLimitNumericUpDown).BeginInit();
            LoopGroupBox.SuspendLayout();
            Run2ndGroupBox.SuspendLayout();
            SecondAlgorithmTableLayoutPanel.SuspendLayout();
            AlgorithmsGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // SizeNumericUpDown
            // 
            SizeNumericUpDown.Location = new Point(143, 9);
            SizeNumericUpDown.Margin = new Padding(2);
            SizeNumericUpDown.Maximum = new decimal(new int[] { 80, 0, 0, 0 });
            SizeNumericUpDown.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            SizeNumericUpDown.Name = "SizeNumericUpDown";
            SizeNumericUpDown.Size = new Size(129, 31);
            SizeNumericUpDown.TabIndex = 0;
            SizeNumericUpDown.Value = new decimal(new int[] { 2, 0, 0, 0 });
            SizeNumericUpDown.ValueChanged += SizeNumericUpDown_ValueChanged;
            SizeNumericUpDown.KeyDown += SizeNumericUpDown_KeyDown;
            // 
            // GenerateMazeButton
            // 
            GenerateMazeButton.Location = new Point(302, 5);
            GenerateMazeButton.Margin = new Padding(2);
            GenerateMazeButton.Name = "GenerateMazeButton";
            GenerateMazeButton.Size = new Size(142, 36);
            GenerateMazeButton.TabIndex = 1;
            GenerateMazeButton.Text = "미로 만들기";
            GenerateMazeButton.UseVisualStyleBackColor = true;
            GenerateMazeButton.Click += GenerateMazeButton_Click;
            // 
            // RunButton
            // 
            RunButton.Enabled = false;
            RunButton.Location = new Point(448, 5);
            RunButton.Margin = new Padding(2);
            RunButton.Name = "RunButton";
            RunButton.Size = new Size(115, 36);
            RunButton.TabIndex = 2;
            RunButton.Text = "미로 실행";
            RunButton.UseVisualStyleBackColor = true;
            RunButton.Click += RunButton_Click;
            // 
            // BfsTimeLabel
            // 
            BfsTimeLabel.AutoSize = true;
            BfsTimeLabel.Location = new Point(9, 84);
            BfsTimeLabel.Margin = new Padding(2, 0, 2, 0);
            BfsTimeLabel.Name = "BfsTimeLabel";
            BfsTimeLabel.Size = new Size(58, 25);
            BfsTimeLabel.TabIndex = 3;
            BfsTimeLabel.Text = "BFS : ";
            // 
            // DfsTimeLabel
            // 
            DfsTimeLabel.AutoSize = true;
            DfsTimeLabel.Location = new Point(9, 116);
            DfsTimeLabel.Margin = new Padding(2, 0, 2, 0);
            DfsTimeLabel.Name = "DfsTimeLabel";
            DfsTimeLabel.Size = new Size(60, 25);
            DfsTimeLabel.TabIndex = 4;
            DfsTimeLabel.Text = "DFS : ";
            // 
            // MazeSizeLabel
            // 
            MazeSizeLabel.AutoSize = true;
            MazeSizeLabel.Location = new Point(1, 11);
            MazeSizeLabel.Margin = new Padding(2, 0, 2, 0);
            MazeSizeLabel.Name = "MazeSizeLabel";
            MazeSizeLabel.Size = new Size(138, 25);
            MazeSizeLabel.TabIndex = 5;
            MazeSizeLabel.Text = "미로 한변 셀 수";
            // 
            // StraightTimePenalty
            // 
            StraightTimePenalty.AutoSize = true;
            StraightTimePenalty.Location = new Point(600, 13);
            StraightTimePenalty.Margin = new Padding(2, 0, 2, 0);
            StraightTimePenalty.Name = "StraightTimePenalty";
            StraightTimePenalty.Size = new Size(172, 25);
            StraightTimePenalty.TabIndex = 6;
            StraightTimePenalty.Text = "직선구간페널티(ms)";
            // 
            // StraightTimePenaltyNumericUpDown
            // 
            StraightTimePenaltyNumericUpDown.Location = new Point(776, 11);
            StraightTimePenaltyNumericUpDown.Margin = new Padding(2);
            StraightTimePenaltyNumericUpDown.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            StraightTimePenaltyNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            StraightTimePenaltyNumericUpDown.Name = "StraightTimePenaltyNumericUpDown";
            StraightTimePenaltyNumericUpDown.Size = new Size(118, 31);
            StraightTimePenaltyNumericUpDown.TabIndex = 7;
            StraightTimePenaltyNumericUpDown.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // RotationPenaltyLabel
            // 
            RotationPenaltyLabel.AutoSize = true;
            RotationPenaltyLabel.Location = new Point(600, 54);
            RotationPenaltyLabel.Margin = new Padding(2, 0, 2, 0);
            RotationPenaltyLabel.Name = "RotationPenaltyLabel";
            RotationPenaltyLabel.Size = new Size(136, 25);
            RotationPenaltyLabel.TabIndex = 8;
            RotationPenaltyLabel.Text = "회전페널티(ms)";
            // 
            // RotationPenaltyNumericUpDown
            // 
            RotationPenaltyNumericUpDown.Location = new Point(776, 52);
            RotationPenaltyNumericUpDown.Margin = new Padding(2);
            RotationPenaltyNumericUpDown.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            RotationPenaltyNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            RotationPenaltyNumericUpDown.Name = "RotationPenaltyNumericUpDown";
            RotationPenaltyNumericUpDown.Size = new Size(118, 31);
            RotationPenaltyNumericUpDown.TabIndex = 9;
            RotationPenaltyNumericUpDown.Value = new decimal(new int[] { 150, 0, 0, 0 });
            // 
            // LoopLimitLabel
            // 
            LoopLimitLabel.AutoSize = true;
            LoopLimitLabel.Location = new Point(8, 27);
            LoopLimitLabel.Margin = new Padding(2, 0, 2, 0);
            LoopLimitLabel.Name = "LoopLimitLabel";
            LoopLimitLabel.Size = new Size(57, 25);
            LoopLimitLabel.TabIndex = 10;
            LoopLimitLabel.Text = "n = 2";
            // 
            // RunLoopButton
            // 
            RunLoopButton.Location = new Point(8, 90);
            RunLoopButton.Margin = new Padding(2);
            RunLoopButton.Name = "RunLoopButton";
            RunLoopButton.Size = new Size(117, 36);
            RunLoopButton.TabIndex = 11;
            RunLoopButton.Text = "Go";
            RunLoopButton.UseVisualStyleBackColor = true;
            RunLoopButton.Click += RunLoopButton_Click;
            // 
            // LoopLimitNumericUpDown
            // 
            LoopLimitNumericUpDown.Location = new Point(8, 54);
            LoopLimitNumericUpDown.Margin = new Padding(2);
            LoopLimitNumericUpDown.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            LoopLimitNumericUpDown.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            LoopLimitNumericUpDown.Name = "LoopLimitNumericUpDown";
            LoopLimitNumericUpDown.Size = new Size(117, 31);
            LoopLimitNumericUpDown.TabIndex = 12;
            LoopLimitNumericUpDown.Value = new decimal(new int[] { 10, 0, 0, 0 });
            LoopLimitNumericUpDown.KeyDown += LoopLimitNumericUpDown_KeyDown;
            // 
            // LoopCountLabel
            // 
            LoopCountLabel.AutoSize = true;
            LoopCountLabel.Location = new Point(8, 128);
            LoopCountLabel.Margin = new Padding(2, 0, 2, 0);
            LoopCountLabel.Name = "LoopCountLabel";
            LoopCountLabel.Size = new Size(83, 25);
            LoopCountLabel.TabIndex = 13;
            LoopCountLabel.Text = "횟수 = 0";
            // 
            // Dfs2ndTimeLabel
            // 
            Dfs2ndTimeLabel.AutoSize = true;
            Dfs2ndTimeLabel.Location = new Point(8, 221);
            Dfs2ndTimeLabel.Margin = new Padding(2, 0, 2, 0);
            Dfs2ndTimeLabel.Name = "Dfs2ndTimeLabel";
            Dfs2ndTimeLabel.Size = new Size(60, 25);
            Dfs2ndTimeLabel.TabIndex = 17;
            Dfs2ndTimeLabel.Text = "DFS : ";
            // 
            // Bfs2ndTimeLabel
            // 
            Bfs2ndTimeLabel.AutoSize = true;
            Bfs2ndTimeLabel.Location = new Point(8, 196);
            Bfs2ndTimeLabel.Margin = new Padding(2, 0, 2, 0);
            Bfs2ndTimeLabel.Name = "Bfs2ndTimeLabel";
            Bfs2ndTimeLabel.Size = new Size(58, 25);
            Bfs2ndTimeLabel.TabIndex = 16;
            Bfs2ndTimeLabel.Text = "BFS : ";
            // 
            // LoopGroupBox
            // 
            LoopGroupBox.Controls.Add(LoopLimitLabel);
            LoopGroupBox.Controls.Add(LoopLimitNumericUpDown);
            LoopGroupBox.Controls.Add(RunLoopButton);
            LoopGroupBox.Controls.Add(LoopCountLabel);
            LoopGroupBox.Location = new Point(9, 187);
            LoopGroupBox.Name = "LoopGroupBox";
            LoopGroupBox.Size = new Size(130, 161);
            LoopGroupBox.TabIndex = 19;
            LoopGroupBox.TabStop = false;
            LoopGroupBox.Text = "Loop";
            // 
            // Run2ndGroupBox
            // 
            Run2ndGroupBox.Controls.Add(SecondAlgorithmTableLayoutPanel);
            Run2ndGroupBox.Controls.Add(Run2ndCheckBox);
            Run2ndGroupBox.Controls.Add(Astar2ndTimeLabel);
            Run2ndGroupBox.Controls.Add(Bfs2ndTimeLabel);
            Run2ndGroupBox.Controls.Add(Dfs2ndTimeLabel);
            Run2ndGroupBox.Location = new Point(9, 366);
            Run2ndGroupBox.Name = "Run2ndGroupBox";
            Run2ndGroupBox.Size = new Size(380, 290);
            Run2ndGroupBox.TabIndex = 20;
            Run2ndGroupBox.TabStop = false;
            Run2ndGroupBox.Text = "2nd";
            // 
            // SecondAlgorithmTableLayoutPanel
            // 
            SecondAlgorithmTableLayoutPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            SecondAlgorithmTableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            SecondAlgorithmTableLayoutPanel.ColumnCount = 4;
            SecondAlgorithmTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            SecondAlgorithmTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            SecondAlgorithmTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            SecondAlgorithmTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            SecondAlgorithmTableLayoutPanel.Controls.Add(FirstAlgorithmHeaderLabel, 0, 0);
            SecondAlgorithmTableLayoutPanel.Controls.Add(SecondBfsHeaderLabel, 1, 0);
            SecondAlgorithmTableLayoutPanel.Controls.Add(SecondDfsHeaderLabel, 2, 0);
            SecondAlgorithmTableLayoutPanel.Controls.Add(SecondAstarHeaderLabel, 3, 0);
            SecondAlgorithmTableLayoutPanel.Controls.Add(BfsFirstHeaderLabel, 0, 1);
            SecondAlgorithmTableLayoutPanel.Controls.Add(DfsFirstHeaderLabel, 0, 2);
            SecondAlgorithmTableLayoutPanel.Controls.Add(AstarFirstHeaderLabel, 0, 3);
            SecondAlgorithmTableLayoutPanel.Controls.Add(BfsFirstBfsSecondCheckBox, 1, 1);
            SecondAlgorithmTableLayoutPanel.Controls.Add(BfsFirstDfsSecondCheckBox, 2, 1);
            SecondAlgorithmTableLayoutPanel.Controls.Add(BfsFirstAstarSecondCheckBox, 3, 1);
            SecondAlgorithmTableLayoutPanel.Controls.Add(DfsFirstBfsSecondCheckBox, 1, 2);
            SecondAlgorithmTableLayoutPanel.Controls.Add(DfsFirstDfsSecondCheckBox, 2, 2);
            SecondAlgorithmTableLayoutPanel.Controls.Add(DfsFirstAstarSecondCheckBox, 3, 2);
            SecondAlgorithmTableLayoutPanel.Controls.Add(AstarFirstBfsSecondCheckBox, 1, 3);
            SecondAlgorithmTableLayoutPanel.Controls.Add(AstarFirstDfsSecondCheckBox, 2, 3);
            SecondAlgorithmTableLayoutPanel.Controls.Add(AstarFirstAstarSecondCheckBox, 3, 3);
            SecondAlgorithmTableLayoutPanel.Location = new Point(8, 66);
            SecondAlgorithmTableLayoutPanel.Name = "SecondAlgorithmTableLayoutPanel";
            SecondAlgorithmTableLayoutPanel.RowCount = 4;
            SecondAlgorithmTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            SecondAlgorithmTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            SecondAlgorithmTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            SecondAlgorithmTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            SecondAlgorithmTableLayoutPanel.Size = new Size(366, 126);
            SecondAlgorithmTableLayoutPanel.TabIndex = 29;
            // 
            // FirstAlgorithmHeaderLabel
            // 
            FirstAlgorithmHeaderLabel.AutoSize = true;
            FirstAlgorithmHeaderLabel.Dock = DockStyle.Fill;
            FirstAlgorithmHeaderLabel.Location = new Point(4, 4);
            FirstAlgorithmHeaderLabel.Margin = new Padding(3);
            FirstAlgorithmHeaderLabel.Name = "FirstAlgorithmHeaderLabel";
            FirstAlgorithmHeaderLabel.Size = new Size(114, 24);
            FirstAlgorithmHeaderLabel.TabIndex = 0;
            FirstAlgorithmHeaderLabel.Text = "1차알고리즘";
            FirstAlgorithmHeaderLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // SecondBfsHeaderLabel
            // 
            SecondBfsHeaderLabel.AutoSize = true;
            SecondBfsHeaderLabel.Dock = DockStyle.Fill;
            SecondBfsHeaderLabel.Location = new Point(125, 4);
            SecondBfsHeaderLabel.Margin = new Padding(3);
            SecondBfsHeaderLabel.Name = "SecondBfsHeaderLabel";
            SecondBfsHeaderLabel.Size = new Size(74, 24);
            SecondBfsHeaderLabel.TabIndex = 1;
            SecondBfsHeaderLabel.Text = "2차BFS";
            SecondBfsHeaderLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // SecondDfsHeaderLabel
            // 
            SecondDfsHeaderLabel.AutoSize = true;
            SecondDfsHeaderLabel.Dock = DockStyle.Fill;
            SecondDfsHeaderLabel.Location = new Point(206, 4);
            SecondDfsHeaderLabel.Margin = new Padding(3);
            SecondDfsHeaderLabel.Name = "SecondDfsHeaderLabel";
            SecondDfsHeaderLabel.Size = new Size(74, 24);
            SecondDfsHeaderLabel.TabIndex = 2;
            SecondDfsHeaderLabel.Text = "2차DFS";
            SecondDfsHeaderLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // SecondAstarHeaderLabel
            // 
            SecondAstarHeaderLabel.AutoSize = true;
            SecondAstarHeaderLabel.Dock = DockStyle.Fill;
            SecondAstarHeaderLabel.Location = new Point(287, 4);
            SecondAstarHeaderLabel.Margin = new Padding(3);
            SecondAstarHeaderLabel.Name = "SecondAstarHeaderLabel";
            SecondAstarHeaderLabel.Size = new Size(75, 24);
            SecondAstarHeaderLabel.TabIndex = 3;
            SecondAstarHeaderLabel.Text = "2차 A*";
            SecondAstarHeaderLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // BfsFirstHeaderLabel
            // 
            BfsFirstHeaderLabel.AutoSize = true;
            BfsFirstHeaderLabel.Dock = DockStyle.Fill;
            BfsFirstHeaderLabel.Location = new Point(4, 35);
            BfsFirstHeaderLabel.Margin = new Padding(3);
            BfsFirstHeaderLabel.Name = "BfsFirstHeaderLabel";
            BfsFirstHeaderLabel.Size = new Size(114, 24);
            BfsFirstHeaderLabel.TabIndex = 4;
            BfsFirstHeaderLabel.Text = "1차 BFS";
            BfsFirstHeaderLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // DfsFirstHeaderLabel
            // 
            DfsFirstHeaderLabel.AutoSize = true;
            DfsFirstHeaderLabel.Dock = DockStyle.Fill;
            DfsFirstHeaderLabel.Location = new Point(4, 66);
            DfsFirstHeaderLabel.Margin = new Padding(3);
            DfsFirstHeaderLabel.Name = "DfsFirstHeaderLabel";
            DfsFirstHeaderLabel.Size = new Size(114, 24);
            DfsFirstHeaderLabel.TabIndex = 5;
            DfsFirstHeaderLabel.Text = "1차 DFS";
            DfsFirstHeaderLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // AstarFirstHeaderLabel
            // 
            AstarFirstHeaderLabel.AutoSize = true;
            AstarFirstHeaderLabel.Dock = DockStyle.Fill;
            AstarFirstHeaderLabel.Location = new Point(4, 97);
            AstarFirstHeaderLabel.Margin = new Padding(3);
            AstarFirstHeaderLabel.Name = "AstarFirstHeaderLabel";
            AstarFirstHeaderLabel.Size = new Size(114, 25);
            AstarFirstHeaderLabel.TabIndex = 6;
            AstarFirstHeaderLabel.Text = "1차 A*";
            AstarFirstHeaderLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // BfsFirstBfsSecondCheckBox
            // 
            BfsFirstBfsSecondCheckBox.Anchor = AnchorStyles.None;
            BfsFirstBfsSecondCheckBox.AutoSize = true;
            BfsFirstBfsSecondCheckBox.Location = new Point(151, 36);
            BfsFirstBfsSecondCheckBox.Margin = new Padding(4, 3, 4, 3);
            BfsFirstBfsSecondCheckBox.Name = "BfsFirstBfsSecondCheckBox";
            BfsFirstBfsSecondCheckBox.Size = new Size(22, 21);
            BfsFirstBfsSecondCheckBox.TabIndex = 30;
            BfsFirstBfsSecondCheckBox.UseVisualStyleBackColor = true;
            // 
            // BfsFirstDfsSecondCheckBox
            // 
            BfsFirstDfsSecondCheckBox.Anchor = AnchorStyles.None;
            BfsFirstDfsSecondCheckBox.AutoSize = true;
            BfsFirstDfsSecondCheckBox.Location = new Point(232, 36);
            BfsFirstDfsSecondCheckBox.Margin = new Padding(4, 3, 4, 3);
            BfsFirstDfsSecondCheckBox.Name = "BfsFirstDfsSecondCheckBox";
            BfsFirstDfsSecondCheckBox.Size = new Size(22, 21);
            BfsFirstDfsSecondCheckBox.TabIndex = 31;
            BfsFirstDfsSecondCheckBox.UseVisualStyleBackColor = true;
            // 
            // BfsFirstAstarSecondCheckBox
            // 
            BfsFirstAstarSecondCheckBox.Anchor = AnchorStyles.None;
            BfsFirstAstarSecondCheckBox.AutoSize = true;
            BfsFirstAstarSecondCheckBox.Location = new Point(313, 36);
            BfsFirstAstarSecondCheckBox.Margin = new Padding(4, 3, 4, 3);
            BfsFirstAstarSecondCheckBox.Name = "BfsFirstAstarSecondCheckBox";
            BfsFirstAstarSecondCheckBox.Size = new Size(22, 21);
            BfsFirstAstarSecondCheckBox.TabIndex = 32;
            BfsFirstAstarSecondCheckBox.UseVisualStyleBackColor = true;
            // 
            // DfsFirstBfsSecondCheckBox
            // 
            DfsFirstBfsSecondCheckBox.Anchor = AnchorStyles.None;
            DfsFirstBfsSecondCheckBox.AutoSize = true;
            DfsFirstBfsSecondCheckBox.Location = new Point(151, 67);
            DfsFirstBfsSecondCheckBox.Margin = new Padding(4, 3, 4, 3);
            DfsFirstBfsSecondCheckBox.Name = "DfsFirstBfsSecondCheckBox";
            DfsFirstBfsSecondCheckBox.Size = new Size(22, 21);
            DfsFirstBfsSecondCheckBox.TabIndex = 33;
            DfsFirstBfsSecondCheckBox.UseVisualStyleBackColor = true;
            // 
            // DfsFirstDfsSecondCheckBox
            // 
            DfsFirstDfsSecondCheckBox.Anchor = AnchorStyles.None;
            DfsFirstDfsSecondCheckBox.AutoSize = true;
            DfsFirstDfsSecondCheckBox.Location = new Point(232, 67);
            DfsFirstDfsSecondCheckBox.Margin = new Padding(4, 3, 4, 3);
            DfsFirstDfsSecondCheckBox.Name = "DfsFirstDfsSecondCheckBox";
            DfsFirstDfsSecondCheckBox.Size = new Size(22, 21);
            DfsFirstDfsSecondCheckBox.TabIndex = 34;
            DfsFirstDfsSecondCheckBox.UseVisualStyleBackColor = true;
            // 
            // DfsFirstAstarSecondCheckBox
            // 
            DfsFirstAstarSecondCheckBox.Anchor = AnchorStyles.None;
            DfsFirstAstarSecondCheckBox.AutoSize = true;
            DfsFirstAstarSecondCheckBox.Location = new Point(313, 67);
            DfsFirstAstarSecondCheckBox.Margin = new Padding(4, 3, 4, 3);
            DfsFirstAstarSecondCheckBox.Name = "DfsFirstAstarSecondCheckBox";
            DfsFirstAstarSecondCheckBox.Size = new Size(22, 21);
            DfsFirstAstarSecondCheckBox.TabIndex = 35;
            DfsFirstAstarSecondCheckBox.UseVisualStyleBackColor = true;
            // 
            // AstarFirstBfsSecondCheckBox
            // 
            AstarFirstBfsSecondCheckBox.Anchor = AnchorStyles.None;
            AstarFirstBfsSecondCheckBox.AutoSize = true;
            AstarFirstBfsSecondCheckBox.Location = new Point(151, 99);
            AstarFirstBfsSecondCheckBox.Margin = new Padding(4, 3, 4, 3);
            AstarFirstBfsSecondCheckBox.Name = "AstarFirstBfsSecondCheckBox";
            AstarFirstBfsSecondCheckBox.Size = new Size(22, 21);
            AstarFirstBfsSecondCheckBox.TabIndex = 36;
            AstarFirstBfsSecondCheckBox.UseVisualStyleBackColor = true;
            // 
            // AstarFirstDfsSecondCheckBox
            // 
            AstarFirstDfsSecondCheckBox.Anchor = AnchorStyles.None;
            AstarFirstDfsSecondCheckBox.AutoSize = true;
            AstarFirstDfsSecondCheckBox.Location = new Point(232, 99);
            AstarFirstDfsSecondCheckBox.Margin = new Padding(4, 3, 4, 3);
            AstarFirstDfsSecondCheckBox.Name = "AstarFirstDfsSecondCheckBox";
            AstarFirstDfsSecondCheckBox.Size = new Size(22, 21);
            AstarFirstDfsSecondCheckBox.TabIndex = 37;
            AstarFirstDfsSecondCheckBox.UseVisualStyleBackColor = true;
            // 
            // AstarFirstAstarSecondCheckBox
            // 
            AstarFirstAstarSecondCheckBox.Anchor = AnchorStyles.None;
            AstarFirstAstarSecondCheckBox.AutoSize = true;
            AstarFirstAstarSecondCheckBox.Location = new Point(313, 99);
            AstarFirstAstarSecondCheckBox.Margin = new Padding(4, 3, 4, 3);
            AstarFirstAstarSecondCheckBox.Name = "AstarFirstAstarSecondCheckBox";
            AstarFirstAstarSecondCheckBox.Size = new Size(22, 21);
            AstarFirstAstarSecondCheckBox.TabIndex = 38;
            AstarFirstAstarSecondCheckBox.UseVisualStyleBackColor = true;
            // 
            // Run2ndCheckBox
            // 
            Run2ndCheckBox.AutoSize = true;
            Run2ndCheckBox.Location = new Point(8, 30);
            Run2ndCheckBox.Name = "Run2ndCheckBox";
            Run2ndCheckBox.Size = new Size(108, 29);
            Run2ndCheckBox.TabIndex = 28;
            Run2ndCheckBox.Text = "2차 탐색";
            Run2ndCheckBox.UseVisualStyleBackColor = true;
            // 
            // Astar2ndTimeLabel
            // 
            Astar2ndTimeLabel.AutoSize = true;
            Astar2ndTimeLabel.Location = new Point(8, 246);
            Astar2ndTimeLabel.Margin = new Padding(2, 0, 2, 0);
            Astar2ndTimeLabel.Name = "Astar2ndTimeLabel";
            Astar2ndTimeLabel.Size = new Size(48, 25);
            Astar2ndTimeLabel.TabIndex = 18;
            Astar2ndTimeLabel.Text = "A* : ";
            // 
            // DfsCheckBox
            // 
            DfsCheckBox.AutoSize = true;
            DfsCheckBox.Checked = true;
            DfsCheckBox.CheckState = CheckState.Checked;
            DfsCheckBox.Location = new Point(6, 43);
            DfsCheckBox.Name = "DfsCheckBox";
            DfsCheckBox.Size = new Size(70, 29);
            DfsCheckBox.TabIndex = 23;
            DfsCheckBox.Text = "DFS";
            DfsCheckBox.UseVisualStyleBackColor = true;
            // 
            // AlgorithmsGroupBox
            // 
            AlgorithmsGroupBox.Controls.Add(BfsCheckBox);
            AlgorithmsGroupBox.Controls.Add(AstarCheckBox);
            AlgorithmsGroupBox.Controls.Add(DfsCheckBox);
            AlgorithmsGroupBox.Location = new Point(919, 5);
            AlgorithmsGroupBox.Name = "AlgorithmsGroupBox";
            AlgorithmsGroupBox.Size = new Size(269, 78);
            AlgorithmsGroupBox.TabIndex = 24;
            AlgorithmsGroupBox.TabStop = false;
            AlgorithmsGroupBox.Text = "Algorithms";
            // 
            // BfsCheckBox
            // 
            BfsCheckBox.AutoSize = true;
            BfsCheckBox.Checked = true;
            BfsCheckBox.CheckState = CheckState.Checked;
            BfsCheckBox.Location = new Point(82, 43);
            BfsCheckBox.Name = "BfsCheckBox";
            BfsCheckBox.Size = new Size(68, 29);
            BfsCheckBox.TabIndex = 25;
            BfsCheckBox.Text = "BFS";
            BfsCheckBox.UseVisualStyleBackColor = true;
            // 
            // AstarCheckBox
            // 
            AstarCheckBox.AutoSize = true;
            AstarCheckBox.Checked = true;
            AstarCheckBox.CheckState = CheckState.Checked;
            AstarCheckBox.Location = new Point(156, 43);
            AstarCheckBox.Name = "AstarCheckBox";
            AstarCheckBox.Size = new Size(58, 29);
            AstarCheckBox.TabIndex = 24;
            AstarCheckBox.Text = "A*";
            AstarCheckBox.UseVisualStyleBackColor = true;
            // 
            // AstarTimeLabel
            // 
            AstarTimeLabel.AutoSize = true;
            AstarTimeLabel.Location = new Point(9, 149);
            AstarTimeLabel.Margin = new Padding(2, 0, 2, 0);
            AstarTimeLabel.Name = "AstarTimeLabel";
            AstarTimeLabel.Size = new Size(48, 25);
            AstarTimeLabel.TabIndex = 25;
            AstarTimeLabel.Text = "A* : ";
            // 
            // VisualDisplayCheckBox
            // 
            VisualDisplayCheckBox.AutoSize = true;
            VisualDisplayCheckBox.Checked = true;
            VisualDisplayCheckBox.CheckState = CheckState.Checked;
            VisualDisplayCheckBox.Location = new Point(302, 52);
            VisualDisplayCheckBox.Name = "VisualDisplayCheckBox";
            VisualDisplayCheckBox.Size = new Size(148, 29);
            VisualDisplayCheckBox.TabIndex = 26;
            VisualDisplayCheckBox.Text = "Visual Display";
            VisualDisplayCheckBox.UseVisualStyleBackColor = true;
            // 
            // WriteCheckBox
            // 
            WriteCheckBox.AutoSize = true;
            WriteCheckBox.Location = new Point(17, 52);
            WriteCheckBox.Name = "WriteCheckBox";
            WriteCheckBox.Size = new Size(146, 29);
            WriteCheckBox.TabIndex = 27;
            WriteCheckBox.Text = "Write on CSV";
            WriteCheckBox.UseVisualStyleBackColor = true;
            WriteCheckBox.CheckedChanged += WriteCheckBox_CheckedChanged;
            // 
            // Maze
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1200, 800);
            Controls.Add(WriteCheckBox);
            Controls.Add(VisualDisplayCheckBox);
            Controls.Add(AstarTimeLabel);
            Controls.Add(AlgorithmsGroupBox);
            Controls.Add(Run2ndGroupBox);
            Controls.Add(LoopGroupBox);
            Controls.Add(StraightTimePenaltyNumericUpDown);
            Controls.Add(StraightTimePenalty);
            Controls.Add(RotationPenaltyNumericUpDown);
            Controls.Add(RotationPenaltyLabel);
            Controls.Add(MazeSizeLabel);
            Controls.Add(DfsTimeLabel);
            Controls.Add(BfsTimeLabel);
            Controls.Add(RunButton);
            Controls.Add(GenerateMazeButton);
            Controls.Add(SizeNumericUpDown);
            Margin = new Padding(2);
            Name = "Maze";
            ShowIcon = false;
            SizeChanged += Maze_SizeChanged;
            ((System.ComponentModel.ISupportInitialize)SizeNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)StraightTimePenaltyNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)RotationPenaltyNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)LoopLimitNumericUpDown).EndInit();
            LoopGroupBox.ResumeLayout(false);
            LoopGroupBox.PerformLayout();
            Run2ndGroupBox.ResumeLayout(false);
            Run2ndGroupBox.PerformLayout();
            SecondAlgorithmTableLayoutPanel.ResumeLayout(false);
            SecondAlgorithmTableLayoutPanel.PerformLayout();
            AlgorithmsGroupBox.ResumeLayout(false);
            AlgorithmsGroupBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown SizeNumericUpDown;
        private Button GenerateMazeButton;
        private Button RunButton;
        private Label BfsTimeLabel;
        private Label DfsTimeLabel;
        private Label MazeSizeLabel;
        private Label StraightTimePenalty;
        private NumericUpDown StraightTimePenaltyNumericUpDown;
        private Label RotationPenaltyLabel;
        private NumericUpDown RotationPenaltyNumericUpDown;
        private Label LoopLimitLabel;
        private Button RunLoopButton;
        private NumericUpDown LoopLimitNumericUpDown;
        private Label LoopCountLabel;
        private Label Dfs2ndTimeLabel;
        private Label Bfs2ndTimeLabel;
        private GroupBox LoopGroupBox;
        private GroupBox Run2ndGroupBox;
        private TableLayoutPanel SecondAlgorithmTableLayoutPanel;
        private CheckBox DfsCheckBox;
        private GroupBox AlgorithmsGroupBox;
        private CheckBox checkBox4;
        private CheckBox BfsCheckBox;
        private CheckBox AstarCheckBox;
        private Label Astar2ndTimeLabel;
        private Label AstarTimeLabel;
        private CheckBox VisualDisplayCheckBox;
        private CheckBox WriteCheckBox;
        private CheckBox Run2ndCheckBox;
        private CheckBox BfsFirstBfsSecondCheckBox;
        private CheckBox BfsFirstDfsSecondCheckBox;
        private CheckBox BfsFirstAstarSecondCheckBox;
        private CheckBox DfsFirstBfsSecondCheckBox;
        private CheckBox DfsFirstDfsSecondCheckBox;
        private CheckBox DfsFirstAstarSecondCheckBox;
        private CheckBox AstarFirstBfsSecondCheckBox;
        private CheckBox AstarFirstDfsSecondCheckBox;
        private CheckBox AstarFirstAstarSecondCheckBox;
        private Label FirstAlgorithmHeaderLabel;
        private Label SecondBfsHeaderLabel;
        private Label SecondDfsHeaderLabel;
        private Label SecondAstarHeaderLabel;
        private Label BfsFirstHeaderLabel;
        private Label DfsFirstHeaderLabel;
        private Label AstarFirstHeaderLabel;
    }
}
