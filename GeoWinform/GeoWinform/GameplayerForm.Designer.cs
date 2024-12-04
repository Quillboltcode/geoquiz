using System.ComponentModel;

namespace GeoWinform
{
    partial class GameplayerForm
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
            lblTitle = new Label();
            dataGridView1 = new DataGridView();
            Question = new DataGridViewTextBoxColumn();
            FinishBtn = new Button();
            groupBoxAnswers = new GroupBox();
            lblYourAnswer = new Label();
            lblQuestionTxt = new Label();
            textBoxQuestion = new TextBox();
            lblName = new Label();
            lblTimer = new Label();
            ((ISupportInitialize)dataGridView1).BeginInit();
            groupBoxAnswers.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(18, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(123, 20);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Your Quiz History";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(20, 62);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(181, 188);
            dataGridView1.TabIndex = 1;
            dataGridView1.DataSource = _questions;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // Question
            // 
            Question.HeaderText = "Question";
            Question.MinimumWidth = 6;
            Question.Name = "Question";
            Question.Width = 125;
            // 
            // FinishBtn
            // 
            FinishBtn.Location = new Point(670, 409);
            FinishBtn.Name = "FinishBtn";
            FinishBtn.Size = new Size(94, 29);
            FinishBtn.TabIndex = 2;
            FinishBtn.Text = "Finish";
            FinishBtn.UseVisualStyleBackColor = true;
            FinishBtn.Click += FinishBtn_Click;
            // 
            // groupBoxAnswers
            // 
            groupBoxAnswers.Controls.Add(lblYourAnswer);
            groupBoxAnswers.Location = new Point(234, 62);
            groupBoxAnswers.Name = "groupBoxAnswers";
            groupBoxAnswers.Size = new Size(500, 312);
            groupBoxAnswers.TabIndex = 3;
            groupBoxAnswers.TabStop = false;
            // 
            // lblYourAnswer
            // 
            lblYourAnswer.AutoSize = true;
            lblYourAnswer.Location = new Point(9, 126);
            lblYourAnswer.Name = "lblYourAnswer";
            lblYourAnswer.Size = new Size(90, 20);
            lblYourAnswer.TabIndex = 1;
            lblYourAnswer.Text = "Your Answer";
            // 
            // lblQuestionTxt
            // 
            lblQuestionTxt.AutoSize = true;
            lblQuestionTxt.Location = new Point(243, 21);
            lblQuestionTxt.Name = "lblQuestionTxt";
            lblQuestionTxt.Size = new Size(99, 20);
            lblQuestionTxt.TabIndex = 0;
            lblQuestionTxt.Text = "Question Text";
            // 
            // textBoxQuestion
            // 
            textBoxQuestion.Location = new Point(357, 18);
            textBoxQuestion.Name = "textBoxQuestion";
            textBoxQuestion.ReadOnly = true;
            textBoxQuestion.Size = new Size(351, 27);
            textBoxQuestion.TabIndex = 2;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(714, 18);
            lblName.Name = "lblName";
            lblName.Size = new Size(73, 20);
            lblName.TabIndex = 4;
            lblName.Text = "username";
            // 
            // lblTimer
            // 
            lblTimer.AutoSize = true;
            lblTimer.Location = new Point(20, 301);
            lblTimer.Name = "lblTimer";
            lblTimer.Size = new Size(50, 20);
            lblTimer.TabIndex = 5;
            lblTimer.Text = "label1";
            // 
            // GameplayerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblTimer);
            Controls.Add(textBoxQuestion);
            Controls.Add(lblQuestionTxt);
            Controls.Add(lblName);
            Controls.Add(groupBoxAnswers);
            Controls.Add(FinishBtn);
            Controls.Add(dataGridView1);
            Controls.Add(lblTitle);
            Name = "GameplayerForm";
            Text = "Gameplayer";
            ((ISupportInitialize)dataGridView1).EndInit();
            groupBoxAnswers.ResumeLayout(false);
            groupBoxAnswers.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private Label lblTitle;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Question;
        private Button FinishBtn;
        private GroupBox groupBoxAnswers;
        private TextBox textBoxQuestion;
        private Label lblYourAnswer;
        private Label lblQuestionTxt;
        private Label lblName;
        private Label lblTimer;
    }
}