namespace GeoWinform
{
    partial class QuestionCreationForm
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
            Question_label = new Label();
            QuestionTextBox = new TextBox();
            comboBoxQuestionType = new ComboBox();
            AnswerDetailLabel = new Label();
            QuestionTypelabel = new Label();
            AddQuestionBtn = new Button();
            CancelBtn = new Button();
            textBoxAnswer = new TextBox();
            labelQuestion = new Label();
            textBoxChoice1 = new TextBox();
            textBoxChoice2 = new TextBox();
            textBoxChoice3 = new TextBox();
            textBoxChoice4 = new TextBox();
            labelAnswer = new Label();
            comboBoxCorrectAnswer = new ComboBox();
            label = new Label();
            radioButtonTrue = new RadioButton();
            radioButtonFalse = new RadioButton();
            panel1 = new Panel();
            panel2 = new Panel();
            panel4 = new Panel();
            panel3 = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // Question_label
            // 
            Question_label.AutoSize = true;
            Question_label.Location = new Point(40, 24);
            Question_label.Name = "Question_label";
            Question_label.Size = new Size(68, 20);
            Question_label.TabIndex = 0;
            Question_label.Text = "Question";
            Question_label.Click += label1_Click;
            // 
            // QuestionTextBox
            // 
            QuestionTextBox.Location = new Point(231, 24);
            QuestionTextBox.Name = "QuestionTextBox";
            QuestionTextBox.Size = new Size(388, 27);
            QuestionTextBox.TabIndex = 1;
            // 
            // comboBoxQuestionType
            // 
            comboBoxQuestionType.FormattingEnabled = true;
            comboBoxQuestionType.Location = new Point(231, 77);
            comboBoxQuestionType.Name = "comboBoxQuestionType";
            comboBoxQuestionType.Size = new Size(287, 28);
            comboBoxQuestionType.TabIndex = 2;
            comboBoxQuestionType.SelectedIndexChanged += comboBoxQuestionType_SelectedIndexChanged;
            // 
            // AnswerDetailLabel
            // 
            AnswerDetailLabel.AutoSize = true;
            AnswerDetailLabel.Location = new Point(40, 169);
            AnswerDetailLabel.Name = "AnswerDetailLabel";
            AnswerDetailLabel.Size = new Size(99, 20);
            AnswerDetailLabel.TabIndex = 4;
            AnswerDetailLabel.Text = "Answer detail";
            // 
            // QuestionTypelabel
            // 
            QuestionTypelabel.AutoSize = true;
            QuestionTypelabel.Location = new Point(46, 88);
            QuestionTypelabel.Name = "QuestionTypelabel";
            QuestionTypelabel.Size = new Size(99, 20);
            QuestionTypelabel.TabIndex = 5;
            QuestionTypelabel.Text = "QuestionType";
            QuestionTypelabel.Click += label2_Click;
            // 
            // AddQuestionBtn
            // 
            AddQuestionBtn.Location = new Point(231, 409);
            AddQuestionBtn.Name = "AddQuestionBtn";
            AddQuestionBtn.Size = new Size(94, 29);
            AddQuestionBtn.TabIndex = 6;
            AddQuestionBtn.Text = "Add";
            AddQuestionBtn.UseVisualStyleBackColor = true;
            AddQuestionBtn.Click += AddQuestionBtn_Click;
            // 
            // CancelBtn
            // 
            CancelBtn.Location = new Point(614, 409);
            CancelBtn.Name = "CancelBtn";
            CancelBtn.Size = new Size(94, 29);
            CancelBtn.TabIndex = 8;
            CancelBtn.Text = "Cancel";
            CancelBtn.UseVisualStyleBackColor = true;
            CancelBtn.Click += CancelBtn_Click;
            // 
            // textBoxAnswer
            // 
            textBoxAnswer.Location = new Point(0, 0);
            textBoxAnswer.Margin = new Padding(5);
            textBoxAnswer.Multiline = true;
            textBoxAnswer.Name = "textBoxAnswer";
            textBoxAnswer.PlaceholderText = "Enter opened answer";
            textBoxAnswer.ScrollBars = ScrollBars.Vertical;
            textBoxAnswer.Size = new Size(508, 30);
            textBoxAnswer.TabIndex = 0;
            // 
            // labelQuestion
            // 
            labelQuestion.Location = new Point(0, 0);
            labelQuestion.Name = "labelQuestion";
            labelQuestion.Size = new Size(100, 23);
            labelQuestion.TabIndex = 0;
            // 
            // textBoxChoice1
            // 
            textBoxChoice1.Location = new Point(1, 1);
            textBoxChoice1.Name = "textBoxChoice1";
            textBoxChoice1.PlaceholderText = "Choice 1";
            textBoxChoice1.Size = new Size(510, 27);
            textBoxChoice1.TabIndex = 0;
            textBoxChoice1.TextChanged += textBoxChoice1_TextChanged;
            // 
            // textBoxChoice2
            // 
            textBoxChoice2.Location = new Point(1, 41);
            textBoxChoice2.Name = "textBoxChoice2";
            textBoxChoice2.PlaceholderText = "Choice 2";
            textBoxChoice2.Size = new Size(505, 27);
            textBoxChoice2.TabIndex = 0;
            textBoxChoice2.TextChanged += textBoxChoice2_TextChanged;
            // 
            // textBoxChoice3
            // 
            textBoxChoice3.Location = new Point(1, 82);
            textBoxChoice3.Name = "textBoxChoice3";
            textBoxChoice3.PlaceholderText = "Choice 3";
            textBoxChoice3.Size = new Size(505, 27);
            textBoxChoice3.TabIndex = 0;
            textBoxChoice3.TextChanged += TextBoxChoice3_TextChanged;
            // 
            // textBoxChoice4
            // 
            textBoxChoice4.Location = new Point(1, 123);
            textBoxChoice4.Name = "textBoxChoice4";
            textBoxChoice4.PlaceholderText = "Choice 4";
            textBoxChoice4.Size = new Size(505, 27);
            textBoxChoice4.TabIndex = 0;
            textBoxChoice4.TextChanged += textBoxChoice4_TextChanged;
            // 
            // labelAnswer
            // 
            labelAnswer.Location = new Point(0, 0);
            labelAnswer.Name = "labelAnswer";
            labelAnswer.Size = new Size(100, 23);
            labelAnswer.TabIndex = 0;
            // 
            // comboBoxCorrectAnswer
            // 
            comboBoxCorrectAnswer.Location = new Point(1, 164);
            comboBoxCorrectAnswer.Name = "comboBoxCorrectAnswer";
            comboBoxCorrectAnswer.DataSource = OptionList;
            comboBoxCorrectAnswer.Size = new Size(252, 27);
            comboBoxCorrectAnswer.TabIndex = 0;
            // 
            // label
            // 
            label.Location = new Point(0, 0);
            label.Name = "label";
            label.Size = new Size(100, 23);
            label.TabIndex = 0;
            // 
            // radioButtonTrue
            // 
            radioButtonTrue.Location = new Point(20, 40);
            radioButtonTrue.Name = "radioButtonTrue";
            radioButtonTrue.Text = "True";
            radioButtonTrue.Size = new Size(104, 24);
            radioButtonTrue.TabIndex = 0;
            // 
            // radioButtonFalse
            // 
            radioButtonFalse.Location = new Point(20, 70);
            radioButtonFalse.Name = "radioButtonFalse";
            radioButtonFalse.Text = "False";
            radioButtonFalse.Size = new Size(104, 24);
            radioButtonFalse.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(panel3);
            panel1.Location = new Point(231, 147);
            panel1.Name = "panel1";
            panel1.Size = new Size(510, 212);
            panel1.TabIndex = 9;
            // 
            // panel2
            // 
            panel2.Controls.Add(textBoxAnswer);
            panel1.Controls.Add(panel4);
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(510, 212);
            panel2.TabIndex = 10;
            // 
            // panel4
            // 
            panel4.Controls.Add(textBoxChoice1);
            panel4.Controls.Add(textBoxChoice2);
            panel4.Controls.Add(textBoxChoice3);
            panel4.Controls.Add(textBoxChoice4);
            panel4.Controls.Add(comboBoxCorrectAnswer);
            panel4.Location = new Point(3, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(510, 212);
            panel4.TabIndex = 10;
            // 
            // panel3
            // 
            panel3.Controls.Add(radioButtonTrue);
            panel3.Controls.Add(radioButtonFalse);
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(510, 212);
            panel3.TabIndex = 10;
            // 
            // QuestionCreationForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(CancelBtn);
            Controls.Add(AddQuestionBtn);
            Controls.Add(QuestionTypelabel);
            Controls.Add(AnswerDetailLabel);
            Controls.Add(comboBoxQuestionType);
            Controls.Add(QuestionTextBox);
            Controls.Add(Question_label);
            Name = "QuestionCreationForm";
            Text = "QuestionCreationForm";
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

  



        #endregion

        private Label Question_label;
        private TextBox QuestionTextBox;
        private ComboBox comboBoxQuestionType;
        private Label AnswerDetailLabel;
        private Label QuestionTypelabel;
        private Button AddQuestionBtn;
        private Button CancelBtn;
        private TextBox textBoxChoice1;
        private TextBox textBoxChoice2;
        private TextBox textBoxChoice3;
        private TextBox textBoxChoice4;
        private TextBox textBoxAnswer;
        private ComboBox comboBoxCorrectAnswer;
        private RadioButton radioButtonTrue;
        private RadioButton radioButtonFalse;
        private Label labelQuestion;
        private Label labelAnswer;
        private Label label;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
    }
}