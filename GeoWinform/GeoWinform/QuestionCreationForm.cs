using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeoWinform
{
    public partial class QuestionCreationForm : Form
    {

        private GameCreator _gameCreator;
        private bool correctAnswer;
        private List<String> OptionList = new() { "Choice 1", "Choice 2", "Choice 3", "Choice 4" };

        public QuestionCreationForm(GameCreator gameCreator)
        {
            InitializeComponent();
            ThemeManager.SetTheme(this);
            // bind value
            comboBoxQuestionType.DataSource = Enum.GetValues(typeof(QuestionType));
            _gameCreator = gameCreator;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxQuestionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //panel1.Controls.Clear(); // Clear any existing controls

            if (comboBoxQuestionType.SelectedItem is QuestionType selectedType)
            {
                switch (selectedType)
                {
                    case QuestionType.OpenEnded:
                        ShowOpenEndedInput();
                        break;
                    case QuestionType.MultipleChoice:
                        ShowMultipleChoiceInput();
                        break;
                    case QuestionType.TrueFalse:
                        ShowTrueFalseInput();
                        break;
                }
            }
        }
        // 0,0 = 231,135
        private void ShowOpenEndedInput()
        {

            panel2.Show();

            panel3.Hide();
            panel4.Hide();

        }

        private void ShowMultipleChoiceInput()
        {
            // Clear previous controls from the panel if any
            //panel1.Controls.Clear();
            panel4.Show();
            panel2.Hide();
            panel3.Hide();

        }


        private void ShowTrueFalseInput()
        {
            // Clear previous controls from the panel if any
            //panel1.Controls.Clear();
            panel3.Show();
            panel2.Hide();
            panel4.Hide();



        }

        // Function to validate the user inputs
        private bool ValidateInputs(string questionText, string questionType)
        {
            if (string.IsNullOrEmpty(questionText) || string.IsNullOrEmpty(questionType))
            {
                return false;
            }

            // Add further checks based on question type
            if (questionType == "Multiple Choice")
            {
                if (string.IsNullOrEmpty(textBoxChoice1.Text) || string.IsNullOrEmpty(textBoxChoice2.Text) ||
                    string.IsNullOrEmpty(textBoxChoice3.Text) || string.IsNullOrEmpty(textBoxChoice4.Text) ||
                    comboBoxCorrectAnswer.SelectedItem == null)
                {
                    return false;
                }
            }
            else if (questionType == "Open-Ended")
            {
                if (string.IsNullOrEmpty(textBoxAnswer.Text))
                {
                    return false;
                }
            }
            else if (questionType == "True/False")
            {
                if (!radioButtonTrue.Checked && !radioButtonFalse.Checked)
                {
                    return false;
                }
            }
            return true;
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AddQuestionBtn_Click(object sender, EventArgs e)
        {
            // Get values from the input controls
            string questionText = QuestionTextBox.Text.Trim();
            string questionType = comboBoxQuestionType.SelectedItem?.ToString();
        
            // Validate inputs
            if (!ValidateInputs(questionText, questionType))
            {
                MessageBox.Show("Please fill in all required fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Create question based on selected type
            Question newQuestion = CreateQuestion(questionText, questionType);
            System.Diagnostics.Debug.WriteLine(newQuestion.ToString());
            if (newQuestion != null)
            {
                // Add the question to your question list or database

                _gameCreator.AddQuestion(newQuestion);
                MessageBox.Show("Question added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Function to create the question object based on type
        private Question? CreateQuestion(string questionText, string questionType)
        {
            int _nextId = _gameCreator.GetNextQuestionId();
            switch (questionType)
            {
                case "MultipleChoice":
                    int correctAnswerIndex = comboBoxCorrectAnswer.SelectedIndex; // Get the index as int

                    // Check if a valid index is selected (i.e., not -1)
                    if (correctAnswerIndex == -1)
                    {
                        MessageBox.Show("Please select the correct answer.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                    
                    return new MultipleChoiceQuestion(_nextId, questionText, new List<string>
            {
            textBoxChoice1.Text, textBoxChoice2.Text, textBoxChoice3.Text, textBoxChoice4.Text
            }, correctAnswerIndex);

                case "OpenEnded":
                    string texxt = textBoxAnswer.Text;
                    return new OpenEndedQuestion(_nextId, questionText, new List<string> { texxt });

                case "TrueFalse":
                    if (radioButtonTrue != null && radioButtonFalse != null)
                    {
                        // Get the selected answer
                        bool correctAnswer = radioButtonTrue.Checked;

                        // You can now use correctAnswer as needed
                    }
                    else
                    {
                        MessageBox.Show("True/False options have not been initialized.");
                        
                    }
                    //bool correctAnswer = radioButtonTrue.Checked;
                    return new TrueFalseQuestion(_nextId, questionText, correctAnswer);

                default:
                    return null;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBoxChoice1_TextChanged(object sender, EventArgs e)
        {
            OptionList[0] = textBoxChoice1.Text;
            comboBoxCorrectAnswer.DataSource = null;
            comboBoxCorrectAnswer.DataSource = new List<string>(OptionList);
        }

        private void textBoxChoice2_TextChanged(object sender, EventArgs e)
        {
            OptionList[1] = textBoxChoice2.Text;
            comboBoxCorrectAnswer.DataSource = null;
            comboBoxCorrectAnswer.DataSource = new List<string>(OptionList);
        }


        private void TextBoxChoice3_TextChanged(object sender, EventArgs e)
        {
            OptionList[2] = textBoxChoice3.Text;
            comboBoxCorrectAnswer.DataSource = null;
            comboBoxCorrectAnswer.DataSource = new List<string>(OptionList);
        }

        private void textBoxChoice4_TextChanged(object sender, EventArgs e)
        {
            OptionList[3] = textBoxChoice4.Text;
            comboBoxCorrectAnswer.DataSource = null;
            comboBoxCorrectAnswer.DataSource = new List<string>(OptionList);
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.Close();

        }
    }
}
