namespace IMDbTop250Scraper
{
    partial class Form1
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
            if(disposing && (components != null))
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
            dataGridView1 = new DataGridView();
            btnParse = new Button();
            btnSave = new Button();
            tbxUrl = new TextBox();
            progressBar1 = new ProgressBar();
            lblStatus = new Label();
            lblProgress = new Label();
            tbxSearch = new TextBox();
            numMinRating = new NumericUpDown();
            numMinVotes = new NumericUpDown();
            minYear = new NumericUpDown();
            lblRating = new Label();
            lblVotes = new Label();
            lblYear = new Label();
            cbSort1 = new ComboBox();
            label1 = new Label();
            cbSort2 = new ComboBox();
            cbSort3 = new ComboBox();
            cbDir1 = new ComboBox();
            cbDir3 = new ComboBox();
            cbDir2 = new ComboBox();
            lblUrl = new Label();
            btnLoadDb = new Button();
            btnClearDb = new Button();
            btnDarkTheme = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMinRating).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMinVotes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)minYear).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(772, 420);
            dataGridView1.TabIndex = 0;
            // 
            // btnParse
            // 
            btnParse.Location = new Point(844, 78);
            btnParse.Name = "btnParse";
            btnParse.Size = new Size(123, 40);
            btnParse.TabIndex = 1;
            btnParse.Text = "Parse";
            btnParse.UseVisualStyleBackColor = true;
            btnParse.Click += btnParse_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(1004, 78);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(127, 40);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save to Excel";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // tbxUrl
            // 
            tbxUrl.Location = new Point(844, 25);
            tbxUrl.Name = "tbxUrl";
            tbxUrl.Size = new Size(395, 27);
            tbxUrl.TabIndex = 3;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(182, 455);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(452, 45);
            progressBar1.TabIndex = 5;
            // 
            // lblStatus
            // 
            lblStatus.Location = new Point(31, 447);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(121, 67);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "label1";
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblProgress
            // 
            lblProgress.Location = new Point(666, 447);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(118, 67);
            lblProgress.TabIndex = 7;
            lblProgress.Text = "label1";
            lblProgress.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbxSearch
            // 
            tbxSearch.Location = new Point(835, 146);
            tbxSearch.Name = "tbxSearch";
            tbxSearch.Size = new Size(150, 27);
            tbxSearch.TabIndex = 8;
            tbxSearch.TextChanged += tbxSearch_TextChanged;
            // 
            // numMinRating
            // 
            numMinRating.Location = new Point(835, 199);
            numMinRating.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numMinRating.Name = "numMinRating";
            numMinRating.Size = new Size(150, 27);
            numMinRating.TabIndex = 12;
            // 
            // numMinVotes
            // 
            numMinVotes.Location = new Point(835, 252);
            numMinVotes.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            numMinVotes.Name = "numMinVotes";
            numMinVotes.Size = new Size(150, 27);
            numMinVotes.TabIndex = 13;
            // 
            // minYear
            // 
            minYear.Location = new Point(835, 303);
            minYear.Maximum = new decimal(new int[] { 2026, 0, 0, 0 });
            minYear.Name = "minYear";
            minYear.Size = new Size(150, 27);
            minYear.TabIndex = 14;
            // 
            // lblRating
            // 
            lblRating.AutoSize = true;
            lblRating.Location = new Point(1004, 206);
            lblRating.Name = "lblRating";
            lblRating.Size = new Size(50, 20);
            lblRating.TabIndex = 15;
            lblRating.Text = "label1";
            // 
            // lblVotes
            // 
            lblVotes.AutoSize = true;
            lblVotes.Location = new Point(1004, 259);
            lblVotes.Name = "lblVotes";
            lblVotes.Size = new Size(50, 20);
            lblVotes.TabIndex = 16;
            lblVotes.Text = "label2";
            // 
            // lblYear
            // 
            lblYear.AutoSize = true;
            lblYear.Location = new Point(1004, 310);
            lblYear.Name = "lblYear";
            lblYear.Size = new Size(50, 20);
            lblYear.TabIndex = 17;
            lblYear.Text = "label3";
            // 
            // cbSort1
            // 
            cbSort1.FormattingEnabled = true;
            cbSort1.Location = new Point(806, 360);
            cbSort1.Name = "cbSort1";
            cbSort1.Size = new Size(151, 28);
            cbSort1.TabIndex = 18;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1004, 149);
            label1.Name = "label1";
            label1.Size = new Size(110, 20);
            label1.TabIndex = 19;
            label1.Text = "Поиск фильма";
            // 
            // cbSort2
            // 
            cbSort2.FormattingEnabled = true;
            cbSort2.Location = new Point(980, 360);
            cbSort2.Name = "cbSort2";
            cbSort2.Size = new Size(151, 28);
            cbSort2.TabIndex = 20;
            // 
            // cbSort3
            // 
            cbSort3.FormattingEnabled = true;
            cbSort3.Location = new Point(1151, 360);
            cbSort3.Name = "cbSort3";
            cbSort3.Size = new Size(151, 28);
            cbSort3.TabIndex = 21;
            // 
            // cbDir1
            // 
            cbDir1.FormattingEnabled = true;
            cbDir1.Location = new Point(806, 411);
            cbDir1.Name = "cbDir1";
            cbDir1.Size = new Size(151, 28);
            cbDir1.TabIndex = 23;
            // 
            // cbDir3
            // 
            cbDir3.FormattingEnabled = true;
            cbDir3.Location = new Point(1151, 411);
            cbDir3.Name = "cbDir3";
            cbDir3.Size = new Size(151, 28);
            cbDir3.TabIndex = 24;
            // 
            // cbDir2
            // 
            cbDir2.FormattingEnabled = true;
            cbDir2.Location = new Point(980, 411);
            cbDir2.Name = "cbDir2";
            cbDir2.Size = new Size(151, 28);
            cbDir2.TabIndex = 25;
            // 
            // lblUrl
            // 
            lblUrl.AutoSize = true;
            lblUrl.Location = new Point(1262, 25);
            lblUrl.Name = "lblUrl";
            lblUrl.Size = new Size(50, 20);
            lblUrl.TabIndex = 26;
            lblUrl.Text = "label2";
            // 
            // btnLoadDb
            // 
            btnLoadDb.Location = new Point(1179, 78);
            btnLoadDb.Name = "btnLoadDb";
            btnLoadDb.Size = new Size(133, 40);
            btnLoadDb.TabIndex = 27;
            btnLoadDb.Text = "Load DB";
            btnLoadDb.UseVisualStyleBackColor = true;
            btnLoadDb.Click += btnLoadDb_Click_1;
            // 
            // btnClearDb
            // 
            btnClearDb.Location = new Point(1179, 139);
            btnClearDb.Name = "btnClearDb";
            btnClearDb.Size = new Size(133, 40);
            btnClearDb.TabIndex = 28;
            btnClearDb.Text = "Clear DB";
            btnClearDb.UseVisualStyleBackColor = true;
            btnClearDb.Click += btnClearDb_Click_1;
            // 
            // btnDarkTheme
            // 
            btnDarkTheme.Location = new Point(1162, 520);
            btnDarkTheme.Name = "btnDarkTheme";
            btnDarkTheme.Size = new Size(140, 45);
            btnDarkTheme.TabIndex = 29;
            btnDarkTheme.Text = "Dark Theme";
            btnDarkTheme.UseVisualStyleBackColor = true;
            btnDarkTheme.Click += btnDarkTheme_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1363, 591);
            Controls.Add(btnDarkTheme);
            Controls.Add(btnClearDb);
            Controls.Add(btnLoadDb);
            Controls.Add(lblUrl);
            Controls.Add(cbDir2);
            Controls.Add(cbDir3);
            Controls.Add(cbDir1);
            Controls.Add(cbSort3);
            Controls.Add(cbSort2);
            Controls.Add(label1);
            Controls.Add(cbSort1);
            Controls.Add(lblYear);
            Controls.Add(lblVotes);
            Controls.Add(lblRating);
            Controls.Add(minYear);
            Controls.Add(numMinVotes);
            Controls.Add(numMinRating);
            Controls.Add(tbxSearch);
            Controls.Add(lblProgress);
            Controls.Add(lblStatus);
            Controls.Add(progressBar1);
            Controls.Add(tbxUrl);
            Controls.Add(btnSave);
            Controls.Add(btnParse);
            Controls.Add(dataGridView1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMinRating).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMinVotes).EndInit();
            ((System.ComponentModel.ISupportInitialize)minYear).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button btnParse;
        private Button btnSave;
        private TextBox tbxUrl;
        private Label lblCount;
        private ProgressBar progressBar1;
        private Label lblStatus;
        private Label lblProgress;
        private TextBox tbxSearch;
        private NumericUpDown numMinRating;
        private NumericUpDown numMinVotes;
        private NumericUpDown minYear;
        private Label lblRating;
        private Label lblVotes;
        private Label lblYear;
        private ComboBox cbSort1;
        private Label label1;
        private ComboBox cbSort2;
        private ComboBox cbSort3;
        private ComboBox cbDir1;
        private ComboBox cbDir3;
        private ComboBox cbDir2;
        private Label lblUrl;
        private Button btnLoadDb;
        private Button btnClearDb;
        private Button btnDarkTheme;
    }
}
