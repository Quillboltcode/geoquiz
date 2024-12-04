namespace GeoWinform
{
    partial class Gamecreation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            DeleteBtn = new Button();
            label3 = new Label();
            SearchTxtBox = new TextBox();
            EditBtn = new Button();
            CreateBtn = new Button();
            openFileDialog1 = new OpenFileDialog();
            GameMasterlbl = new Label();
            BackBtn = new Button();
            dgvQuestion = new DataGridView();
            AddUserBtn = new Button();
            SearchBtn = new Button();
            groupBoxAnswers = new GroupBox();
            tboxTitle = new TextBox();
            lblQuestionTxt = new Label();
            lblYourAnswer = new Label();
            ViewResultBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvQuestion).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(44, 9);
            label1.Name = "label1";
            label1.Size = new Size(116, 20);
            label1.TabIndex = 0;
            label1.Text = "Search Question";
            // 
            // DeleteBtn
            // 
            DeleteBtn.Location = new Point(381, 401);
            DeleteBtn.Name = "DeleteBtn";
            DeleteBtn.Size = new Size(94, 29);
            DeleteBtn.TabIndex = 1;
            DeleteBtn.Text = "Delete";
            DeleteBtn.UseVisualStyleBackColor = true;
            DeleteBtn.Click += Delete_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(44, 66);
            label3.Name = "label3";
            label3.Size = new Size(115, 20);
            label3.TabIndex = 5;
            label3.Text = "Question viewer";
            // 
            // SearchTxtBox
            // 
            SearchTxtBox.Location = new Point(169, 6);
            SearchTxtBox.Name = "SearchTxtBox";
            SearchTxtBox.Size = new Size(212, 27);
            SearchTxtBox.TabIndex = 6;
            // 
            // EditBtn
            // 
            EditBtn.Location = new Point(493, 399);
            EditBtn.Name = "EditBtn";
            EditBtn.Size = new Size(94, 29);
            EditBtn.TabIndex = 7;
            EditBtn.Text = "Edit";
            EditBtn.UseVisualStyleBackColor = true;
            EditBtn.Click += UpdateButton_Click;
            // 
            // CreateBtn
            // 
            CreateBtn.Location = new Point(246, 401);
            CreateBtn.Name = "CreateBtn";
            CreateBtn.Size = new Size(94, 29);
            CreateBtn.TabIndex = 8;
            CreateBtn.Text = "Create";
            CreateBtn.UseVisualStyleBackColor = true;
            CreateBtn.Click += CreateBtn_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // GameMasterlbl
            // 
            GameMasterlbl.AutoSize = true;
            GameMasterlbl.Location = new Point(653, 13);
            GameMasterlbl.Name = "GameMasterlbl";
            GameMasterlbl.Size = new Size(97, 20);
            GameMasterlbl.TabIndex = 9;
            GameMasterlbl.Text = "Game master";
            // 
            // BackBtn
            // 
            BackBtn.Location = new Point(659, 399);
            BackBtn.Name = "BackBtn";
            BackBtn.Size = new Size(94, 29);
            BackBtn.TabIndex = 10;
            BackBtn.Text = "Back";
            BackBtn.UseVisualStyleBackColor = true;
            BackBtn.Click += BackBtn_Click;
            // 
            // dgvQuestion
            // 
            dgvQuestion.AllowUserToAddRows = false;
            dgvQuestion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvQuestion.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvQuestion.Location = new Point(21, 115);
            dgvQuestion.MultiSelect = false;
            dgvQuestion.Name = "dgvQuestion";
            dgvQuestion.ReadOnly = true;
            dgvQuestion.RowHeadersWidth = 51;
            dgvQuestion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvQuestion.Size = new Size(319, 261);
            dgvQuestion.TabIndex = 11;
            dgvQuestion.CellClick += DgvQuestion_CellClick;
            // 
            // AddUserBtn
            // 
            AddUserBtn.Location = new Point(21, 387);
            AddUserBtn.Name = "AddUserBtn";
            AddUserBtn.Size = new Size(78, 56);
            AddUserBtn.TabIndex = 12;
            AddUserBtn.Text = "Add new player";
            AddUserBtn.UseVisualStyleBackColor = true;
            AddUserBtn.Click += AddUserBtn_Click;
            // 
            // SearchBtn
            // 
            SearchBtn.Location = new Point(411, 4);
            SearchBtn.Name = "SearchBtn";
            SearchBtn.Size = new Size(94, 29);
            SearchBtn.TabIndex = 13;
            SearchBtn.Text = "Search";
            SearchBtn.UseVisualStyleBackColor = true;
            SearchBtn.Click += SearchBtn_Click;
            // 
            // groupBoxAnswers
            // 
            groupBoxAnswers.Location = new Point(355, 115);
            groupBoxAnswers.Name = "groupBoxAnswers";
            groupBoxAnswers.Size = new Size(411, 245);
            groupBoxAnswers.TabIndex = 14;
            groupBoxAnswers.TabStop = false;
            // 
            // tboxTitle
            // 
            tboxTitle.Location = new Point(415, 63);
            tboxTitle.Name = "tboxTitle";
            tboxTitle.Size = new Size(351, 27);
            tboxTitle.TabIndex = 3;
            // 
            // lblQuestionTxt
            // 
            lblQuestionTxt.AutoSize = true;
            lblQuestionTxt.Location = new Point(310, 66);
            lblQuestionTxt.Name = "lblQuestionTxt";
            lblQuestionTxt.Size = new Size(99, 20);
            lblQuestionTxt.TabIndex = 2;
            lblQuestionTxt.Text = "Question Text";
            // 
            // lblYourAnswer
            // 
            lblYourAnswer.AutoSize = true;
            lblYourAnswer.Location = new Point(352, 92);
            lblYourAnswer.Name = "lblYourAnswer";
            lblYourAnswer.Size = new Size(57, 20);
            lblYourAnswer.TabIndex = 1;
            lblYourAnswer.Text = "Answer";
            // 
            // ViewResultBtn
            // 
            ViewResultBtn.Location = new Point(123, 401);
            ViewResultBtn.Name = "ViewResultBtn";
            ViewResultBtn.Size = new Size(94, 29);
            ViewResultBtn.TabIndex = 15;
            ViewResultBtn.Text = "View Result";
            ViewResultBtn.UseVisualStyleBackColor = true;
            ViewResultBtn.Click += ViewResultBtn_Click;
            // 
            // Gamecreation
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(800, 450);
            Controls.Add(ViewResultBtn);
            Controls.Add(lblYourAnswer);
            Controls.Add(tboxTitle);
            Controls.Add(groupBoxAnswers);
            Controls.Add(lblQuestionTxt);
            Controls.Add(SearchBtn);
            Controls.Add(AddUserBtn);
            Controls.Add(dgvQuestion);
            Controls.Add(BackBtn);
            Controls.Add(GameMasterlbl);
            Controls.Add(CreateBtn);
            Controls.Add(EditBtn);
            Controls.Add(SearchTxtBox);
            Controls.Add(label3);
            Controls.Add(DeleteBtn);
            Controls.Add(label1);
            Name = "Gamecreation";
            Text = " ";
            ((System.ComponentModel.ISupportInitialize)dgvQuestion).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }





        #endregion

        private Label label1;
        private Button DeleteBtn;
        private Label label3;
        private TextBox SearchTxtBox;
        private Button EditBtn;
        private Button CreateBtn;
        private OpenFileDialog openFileDialog1;
        private Label GameMasterlbl;
        private Button BackBtn;
        private DataGridView dgvQuestion;
        private Button AddUserBtn;
        private Button SearchBtn;
        private GroupBox groupBoxAnswers;
        private Label lblYourAnswer;
        private Label lblQuestionTxt;
        private TextBox tboxTitle;
        private Button ViewResultBtn;
    }
}