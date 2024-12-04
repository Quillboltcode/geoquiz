using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeoWinform
{

    public partial class GameplayerForm : Form

    {
        private readonly string _username;
        private readonly Dictionary<int, string> _userAnswers;
        private readonly GamePlayer _gamePlayer;
        private bool _quizCompleted;
        private readonly List<Question> _questions;
        private System.Windows.Forms.Timer _quizTimer;
        public GameplayerForm(string username, GamePlayer gamePlayer, List<Question> questions)
        {
            ThemeManager.SetTheme(this);
            _username = username ?? "username";
            _gamePlayer = gamePlayer;
            _gamePlayer.StartQuizTimer();
            _questions = questions;
            _userAnswers = new Dictionary<int, string>();
            _quizCompleted = false;


            InitializeComponent();
            //DisplayQuestion(_questions.First());
            _quizTimer = new System.Windows.Forms.Timer
            {
                Interval = 1000 // 1 second interval
            };
            _quizTimer.Tick += QuizTimer_Tick;
            lblTimer.Text = "Time: 00:00";
            _quizTimer.Start();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void QuizTimer_Tick(object sender, EventArgs e)
        {
            // Update the timer label with the elapsed time
            TimeSpan elapsed = _gamePlayer.ElaspedTime;
            lblTimer.Text = $"Time: {elapsed.Minutes:D2}:{elapsed.Seconds:D2}";
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure it's not the header row
            {

                // Get the currently displayed question ID
                if (groupBoxAnswers.Tag is int currentQuestionId)
                {
                    // Save the user's answer for the current question
                    SaveUserAnswer(currentQuestionId);
                }
                // Get the selected question ID
                var selectedId = (int)dataGridView1.Rows[e.RowIndex].Cells["Id"].Value;

                // Find the corresponding question
                var selectedQuestion = _questions.FirstOrDefault(q => q.Id == selectedId);
                if (selectedQuestion != null)
                {
                    groupBoxAnswers.Tag = selectedId;
                    DisplayQuestion(selectedQuestion);
                }
            }
        }
        private void DisplayQuestion(Question question)
        {
            // Clear existing controls in the GroupBox
            groupBoxAnswers.Controls.Clear();
            textBoxQuestion.Text = question.Title;

            switch (question.Type)
            {
                case QuestionType.MultipleChoice:
                    DisplayMultipleChoiceOptions((MultipleChoiceQuestion)question);
                    break;

                case QuestionType.OpenEnded:
                    DisplayOpenEndedInput((OpenEndedQuestion)question);
                    break;

                case QuestionType.TrueFalse:
                    DisplayTrueFalseOptions((TrueFalseQuestion)question);
                    break;

                default:
                    throw new NotSupportedException("Unsupported question type.");
            }

            // Pre-fill the user's previous answer (if available)
            if (_userAnswers.TryGetValue(question.Id, out var savedAnswer))
            {
                switch (question.Type)
                {
                    case QuestionType.MultipleChoice:
                        // Check the RadioButton with the saved answer
                        var radioButton = groupBoxAnswers.Controls
                            .OfType<RadioButton>()
                            .FirstOrDefault(rb => rb.Text == savedAnswer);
                        if (radioButton != null)
                        {
                            radioButton.Checked = true;
                        }
                        break;

                    case QuestionType.OpenEnded:
                        // Set the TextBox value
                        var textBox = groupBoxAnswers.Controls.OfType<TextBox>().FirstOrDefault();
                        if (textBox != null)
                        {
                            textBox.Text = savedAnswer;
                        }
                        break;

                    case QuestionType.TrueFalse:
                        // Check the RadioButton with the saved answer
                        var trueFalseButton = groupBoxAnswers.Controls
                            .OfType<RadioButton>()
                            .FirstOrDefault(rb => rb.Text == savedAnswer);
                        if (trueFalseButton != null)
                        {
                            trueFalseButton.Checked = true;
                        }
                        break;
                }
            }
        }

        private void DisplayMultipleChoiceOptions(MultipleChoiceQuestion question)
        {
            int yOffset = 20; // Vertical spacing
            for (int i = 0; i < question.Options.Count; i++)
            {
                var radioButton = new RadioButton
                {
                    Text = question.Options[i],
                    Location = new Point(10, yOffset),
                    AutoSize = true,
                    Tag = i // Store the index in the Tag for identification
                };

                // Add to GroupBox
                groupBoxAnswers.Controls.Add(radioButton);
                yOffset += 30; // Adjust spacing for next option
            }
        }

        private void DisplayOpenEndedInput(OpenEndedQuestion question)
        {
            var textBoxAnswer = new TextBox
            {
                Location = new Point(10, 20),
                Width = groupBoxAnswers.Width - 20 // Adjust to fit within the GroupBox
            };

            // Add to GroupBox
            groupBoxAnswers.Controls.Add(textBoxAnswer);
        }

        private void DisplayTrueFalseOptions(TrueFalseQuestion question)
        {
            var radioButtonTrue = new RadioButton
            {
                Text = "True",
                Location = new Point(10, 20),
                AutoSize = true,
                Tag = true // Store a boolean value in the Tag for identification
            };

            var radioButtonFalse = new RadioButton
            {
                Text = "False",
                Location = new Point(10, 50),
                AutoSize = true,
                Tag = false // Store a boolean value in the Tag for identification
            };

            // Add to GroupBox
            groupBoxAnswers.Controls.Add(radioButtonTrue);
            groupBoxAnswers.Controls.Add(radioButtonFalse);
        }

        // Method for saving user answer to Dictionary
        private void SaveUserAnswer(int questionId)
        {
            // Check if a question is displayed
            if (!_questions.Any(q => q.Id == questionId)) return;

            var question = _questions.First(q => q.Id == questionId);

            switch (question.Type)
            {
                case QuestionType.MultipleChoice:
                    // Get the selected RadioButton
                    var selectedOption = groupBoxAnswers.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);
                    if (selectedOption != null)
                    {
                        _userAnswers[questionId] = selectedOption.Text; // Save the selected option text
                    }
                    break;

                case QuestionType.OpenEnded:
                    // Get the TextBox input
                    var answerTextBox = groupBoxAnswers.Controls.OfType<TextBox>().FirstOrDefault();
                    if (answerTextBox != null)
                    {
                        _userAnswers[questionId] = answerTextBox.Text; // Save the input text
                    }
                    break;

                case QuestionType.TrueFalse:
                    // Get the selected True/False RadioButton
                    var selectedTrueFalse = groupBoxAnswers.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);
                    if (selectedTrueFalse != null)
                    {
                        _userAnswers[questionId] = selectedTrueFalse.Text; // Save "True" or "False"
                    }
                    break;

                default:
                    throw new NotSupportedException("Unsupported question type.");
            }
        }


        private void FinishBtn_Click(object sender, EventArgs e)
        {
            // Confirmation dialog to ensure the user wants to finish
            var confirmation = MessageBox.Show("Are you sure you want to finish the quiz?",
                                               "Finish Quiz",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question);

            // If the user selects 'No', simply return
            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            try
            {
                // Save current cell user answer as save method only called when user click on a question
                if (groupBoxAnswers.Tag is int currentQuestionId)
                {
                    // Save the user's answer for the current question
                    SaveUserAnswer(currentQuestionId);
                }

                // Retrieve quiz data (assume these are available in the current session)
                List<Question> quizQuestions = _questions; // Replace with the actual quiz questions list
                List<string> playerAnswers = _userAnswers.Values.ToList(); // Convert user answers dictionary to a list of answers

                // Calculate the number of correct answers
                int correctAnswers = GamePlayer.CountCorrectAnswers(quizQuestions, playerAnswers);

                // Total number of questions
                int totalQuestions = quizQuestions.Count;

                // Stop the quiz timer
                _gamePlayer.StopQuizTimer();
                _quizTimer.Stop();

                // Elapsed time for the quiz (assume this is tracked during the quiz)
                TimeSpan timeTaken = _gamePlayer.GetQuizTime(); // Assume GetQuizTime() returns the time taken

                // Save the quiz results
                _gamePlayer.SaveQuizResults(_gamePlayer._playerId, correctAnswers, totalQuestions, timeTaken);

                // Notify the user of successful save
                MessageBox.Show($"Quiz finished! You answered {correctAnswers} out of {totalQuestions} correctly.",
                                "Quiz Results",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                // Mark the quiz as completed
                _quizCompleted = true;

                // Close the current form or navigate to another form (if needed)
                this.Close();
            }
            catch (ArgumentNullException ex)
            {
                // Catches situations where a required argument was null
                // For example, if _questions or _userAnswers are unintentionally null
                MessageBox.Show($"Error: {ex.Message}",   // Display the error message
                                "Input Error",           // Title of the message box
                                MessageBoxButtons.OK,    // Single OK button for acknowledgment
                                MessageBoxIcon.Warning); // Icon indicating a warning or user-related issue
                this.Close(); // Ensure the form closes even after encountering this error
            }
            catch (ArgumentException ex)
            {
                // Catches cases where an invalid argument is passed
                // For example, incorrect types or values in user answers or quiz data
                MessageBox.Show($"Error: {ex.Message}",   // Display the error message
                                "Input Error",           // Title of the message box
                                MessageBoxButtons.OK,    // Single OK button for acknowledgment
                                MessageBoxIcon.Warning); // Icon indicating a warning or user-related issue
                this.Close(); // Ensure the form closes even after encountering this error
            }
            catch (Exception ex)
            {
                // Handles any unexpected or unhandled exceptions
                // This is a safety net for errors not covered by the specific exception types above
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", // Generic error message
                                "Error",                                       // Title of the message box
                                MessageBoxButtons.OK,                          // Single OK button for acknowledgment
                                MessageBoxIcon.Error);                         // Icon indicating a critical error
                this.Close(); // Ensure the form closes gracefully
            }
        }

    }
}
