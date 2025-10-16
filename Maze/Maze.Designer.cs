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
            Dfs2ndTimeLabel.Location = new Point(8, 52);
            Dfs2ndTimeLabel.Margin = new Padding(2, 0, 2, 0);
            Dfs2ndTimeLabel.Name = "Dfs2ndTimeLabel";
            Dfs2ndTimeLabel.Size = new Size(60, 25);
            Dfs2ndTimeLabel.TabIndex = 17;
            Dfs2ndTimeLabel.Text = "DFS : ";
            // 
            // Bfs2ndTimeLabel
            // 
            Bfs2ndTimeLabel.AutoSize = true;
            Bfs2ndTimeLabel.Location = new Point(8, 27);
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
            Run2ndGroupBox.Controls.Add(Run2ndCheckBox);
            Run2ndGroupBox.Controls.Add(Astar2ndTimeLabel);
            Run2ndGroupBox.Controls.Add(Bfs2ndTimeLabel);
            Run2ndGroupBox.Controls.Add(Dfs2ndTimeLabel);
            Run2ndGroupBox.Location = new Point(9, 366);
            Run2ndGroupBox.Name = "Run2ndGroupBox";
            Run2ndGroupBox.Size = new Size(130, 142);
            Run2ndGroupBox.TabIndex = 20;
            Run2ndGroupBox.TabStop = false;
            Run2ndGroupBox.Text = "2nd";
            // 
            // Run2ndCheckBox
            // 
            Run2ndCheckBox.AutoSize = true;
            Run2ndCheckBox.Location = new Point(8, 108);
            Run2ndCheckBox.Name = "Run2ndCheckBox";
            Run2ndCheckBox.Size = new Size(108, 29);
            Run2ndCheckBox.TabIndex = 28;
            Run2ndCheckBox.Text = "2차 탐색";
            Run2ndCheckBox.UseVisualStyleBackColor = true;
            // 
            // Astar2ndTimeLabel
            // 
            Astar2ndTimeLabel.AutoSize = true;
            Astar2ndTimeLabel.Location = new Point(8, 80);
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
    }
}
