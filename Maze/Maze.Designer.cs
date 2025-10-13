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
            LoopLimitLabel = new Label();
            RunLoopButton = new Button();
            LoopLimitNumericUpDown = new NumericUpDown();
            LoopCountLabel = new Label();
            Check2ndRunButton = new Button();
            Check2ndLabel = new Label();
            Dfs2ndTimeLabel = new Label();
            Bfs2ndTimeLabel = new Label();
            LoopGroupBox = new GroupBox();
            Run2ndGroupBox = new GroupBox();
            WriteButton = new Button();
            CheckWriteLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)SizeNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)StraightTimePenaltyNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LoopLimitNumericUpDown).BeginInit();
            LoopGroupBox.SuspendLayout();
            Run2ndGroupBox.SuspendLayout();
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
            StraightTimePenalty.Location = new Point(581, 11);
            StraightTimePenalty.Margin = new Padding(2, 0, 2, 0);
            StraightTimePenalty.Name = "StraightTimePenalty";
            StraightTimePenalty.Size = new Size(190, 25);
            StraightTimePenalty.TabIndex = 6;
            StraightTimePenalty.Text = "미로 설정 클럭 수(ms)";
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
            RunLoopButton.Size = new Size(90, 36);
            RunLoopButton.TabIndex = 11;
            RunLoopButton.Text = "Go";
            RunLoopButton.UseVisualStyleBackColor = true;
            RunLoopButton.Click += RunLoopButton_Click;
            // 
            // LoopLimitNumericUpDown
            // 
            LoopLimitNumericUpDown.Location = new Point(8, 54);
            LoopLimitNumericUpDown.Margin = new Padding(2);
            LoopLimitNumericUpDown.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            LoopLimitNumericUpDown.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            LoopLimitNumericUpDown.Name = "LoopLimitNumericUpDown";
            LoopLimitNumericUpDown.Size = new Size(90, 31);
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
            // Check2ndRunButton
            // 
            Check2ndRunButton.Location = new Point(8, 76);
            Check2ndRunButton.Name = "Check2ndRunButton";
            Check2ndRunButton.Size = new Size(87, 34);
            Check2ndRunButton.TabIndex = 14;
            Check2ndRunButton.Text = "2차 ON";
            Check2ndRunButton.UseVisualStyleBackColor = true;
            Check2ndRunButton.Click += Check2ndRunButton_Click;
            // 
            // Check2ndLabel
            // 
            Check2ndLabel.AutoSize = true;
            Check2ndLabel.Location = new Point(8, 113);
            Check2ndLabel.Margin = new Padding(2, 0, 2, 0);
            Check2ndLabel.Name = "Check2ndLabel";
            Check2ndLabel.Size = new Size(52, 25);
            Check2ndLabel.TabIndex = 15;
            Check2ndLabel.Text = "False";
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
            LoopGroupBox.Location = new Point(9, 168);
            LoopGroupBox.Name = "LoopGroupBox";
            LoopGroupBox.Size = new Size(117, 161);
            LoopGroupBox.TabIndex = 19;
            LoopGroupBox.TabStop = false;
            LoopGroupBox.Text = "Loop";
            // 
            // Run2ndGroupBox
            // 
            Run2ndGroupBox.Controls.Add(Bfs2ndTimeLabel);
            Run2ndGroupBox.Controls.Add(Dfs2ndTimeLabel);
            Run2ndGroupBox.Controls.Add(Check2ndRunButton);
            Run2ndGroupBox.Controls.Add(Check2ndLabel);
            Run2ndGroupBox.Location = new Point(9, 347);
            Run2ndGroupBox.Name = "Run2ndGroupBox";
            Run2ndGroupBox.Size = new Size(117, 147);
            Run2ndGroupBox.TabIndex = 20;
            Run2ndGroupBox.TabStop = false;
            Run2ndGroupBox.Text = "2nd";
            // 
            // WriteButton
            // 
            WriteButton.Location = new Point(9, 39);
            WriteButton.Name = "WriteButton";
            WriteButton.Size = new Size(98, 34);
            WriteButton.TabIndex = 21;
            WriteButton.Text = "기록 ON";
            WriteButton.UseVisualStyleBackColor = true;
            WriteButton.Click += WriteButton_Click;
            // 
            // CheckWriteLabel
            // 
            CheckWriteLabel.AutoSize = true;
            CheckWriteLabel.Location = new Point(112, 44);
            CheckWriteLabel.Margin = new Padding(2, 0, 2, 0);
            CheckWriteLabel.Name = "CheckWriteLabel";
            CheckWriteLabel.Size = new Size(52, 25);
            CheckWriteLabel.TabIndex = 22;
            CheckWriteLabel.Text = "False";
            // 
            // Maze
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1062, 570);
            Controls.Add(CheckWriteLabel);
            Controls.Add(WriteButton);
            Controls.Add(Run2ndGroupBox);
            Controls.Add(LoopGroupBox);
            Controls.Add(StraightTimePenaltyNumericUpDown);
            Controls.Add(StraightTimePenalty);
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
            ((System.ComponentModel.ISupportInitialize)LoopLimitNumericUpDown).EndInit();
            LoopGroupBox.ResumeLayout(false);
            LoopGroupBox.PerformLayout();
            Run2ndGroupBox.ResumeLayout(false);
            Run2ndGroupBox.PerformLayout();
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
        private Label LoopLimitLabel;
        private Button RunLoopButton;
        private NumericUpDown LoopLimitNumericUpDown;
        private Label LoopCountLabel;
        private Button Check2ndRunButton;
        private Label Check2ndLabel;
        private Label Dfs2ndTimeLabel;
        private Label Bfs2ndTimeLabel;
        private GroupBox LoopGroupBox;
        private GroupBox Run2ndGroupBox;
        private Button WriteButton;
        private Label CheckWriteLabel;
    }
}
