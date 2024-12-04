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
    public partial class Gamecreation : Form
    {
        private GameCreator _gameCreator;
        private MainForm _mainForm;
        private readonly List<Question> _tempQuestions;
        public Gamecreation(GameCreator gameCreator, MainForm mainForm)
        {
            InitializeComponent();
            ThemeManager.SetTheme(this);
            _gameCreator = gameCreator;
            _mainForm = mainForm;
            _tempQuestions = new List<Question>();
            LoadQuestionsIntoDataGridView();

            if (_tempQuestions.Count > 0)
            {
                DisplayQuestion(_tempQuestions.First());
            }


        }
        private void LoadQuestionsIntoDataGridView()
        {
            // Retrieve all questions from the database
            _tempQuestions.Clear();
            _tempQuestions.AddRange(_gameCreator.GetAllQuestions());

            // Bind questions to the DataGridView
            dgvQuestion.DataSource = _tempQuestions.Select(q => new
            {
                q.Id,
                q.Title,
                Type = q.Type.ToString()
            }).ToList();
        }


        private void DgvQuestion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row is clicked
            {
                DataGridView dgvQuestion1 = dgvQuestion;
                DataGridViewRow selectedRow = dgvQuestion1.Rows[e.RowIndex];
                int selectedId = (int)dgvQuestion1.Rows[e.RowIndex].Cells["Id"].Value;
                var selectedQuestion = _tempQuestions.FirstOrDefault(q => q.Id == selectedId);
                if (selectedQuestion != null)
                {
                    DisplayQuestion(selectedQuestion);
                }

            }
        }


        private void CreateBtn_Click(object sender, EventArgs e)
        {
            using (var questionCreationForm = new QuestionCreationForm(_gameCreator))
            {
                questionCreationForm.ShowDialog();
            }
            LoadQuestionsIntoDataGridView();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (dgvQuestion.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a question to delete", "No selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Get the selected question's ID
            var selectedRow = dgvQuestion.SelectedRows[0];
            int questionId = (int)selectedRow.Cells["Id"].Value;

            // Confirm deletion
            var confirmResult = MessageBox.Show("Are you sure you want to delete this question?",
                                                "Confirm Delete",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes) return;

            try
            {
                // Call GameCreator's method to delete the question
                _gameCreator.DeleteQuestion(questionId);

                // Remove the question from the local list and update the DataGridView
                _tempQuestions.RemoveAll(q => q.Id == questionId);
                dgvQuestion.DataSource = null;
                dgvQuestion.DataSource = _tempQuestions;

                MessageBox.Show("Question deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to delete question: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            _mainForm?.Show();
        }

        private void AddUserBtn_Click(object sender, EventArgs e)
        {
            AddPlayerForm addPlayerForm = new AddPlayerForm(_gameCreator);
            addPlayerForm.ShowDialog();
        }


        private void DisplayQuestion(Question question)
        {
            // Clear existing controls in the GroupBox
            this.groupBoxAnswers.Controls.Clear();
            tboxTitle.Text = question.Title;

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

                // Highlight the correct answer
                if (i == question.CorrectAnswerIndex)
                {
                    radioButton.Font = new Font(radioButton.Font, FontStyle.Bold);
                    radioButton.ForeColor = Color.Green;
                }


                // Add to GroupBox
                this.groupBoxAnswers.Controls.Add(radioButton);
                yOffset += 30; // Adjust spacing for next option
            }
        }

        private void DisplayOpenEndedInput(OpenEndedQuestion question)
        {
            var textBoxAnswer = new TextBox
            {
                Location = new Point(10, 20),
                Width = this.groupBoxAnswers.Width - 20, // Adjust to fit within the GroupBox
                Text = question.AcceptableAnswers.FirstOrDefault()
            };

            // Add to GroupBox
            this.groupBoxAnswers.Controls.Add(textBoxAnswer);
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

            // Highlight the correct answer
            if (question.IsTrue)
            {
                radioButtonTrue.Font = new Font(radioButtonTrue.Font, FontStyle.Bold);
                radioButtonTrue.ForeColor = Color.Green;
                radioButtonFalse.ForeColor = Color.Red;
            }
            else
            {
                radioButtonFalse.Font = new Font(radioButtonFalse.Font, FontStyle.Bold);
                radioButtonFalse.ForeColor = Color.Green;
                radioButtonTrue.ForeColor = Color.Red;
            }

            // Add to GroupBox
            this.groupBoxAnswers.Controls.Add(radioButtonTrue);
            this.groupBoxAnswers.Controls.Add(radioButtonFalse);
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (dgvQuestion.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a question to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the selected question ID
            var selectedRow = dgvQuestion.SelectedRows[0];
            int questionId = (int)selectedRow.Cells["Id"].Value;

            // Find the corresponding question in the local list
            var selectedQuestion = _tempQuestions.FirstOrDefault(q => q.Id == questionId);

            if (selectedQuestion == null)
            {
                MessageBox.Show("Question not found in the local list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Update the question title
            string newTitle = tboxTitle.Text.Trim();
            if (string.IsNullOrWhiteSpace(newTitle))
            {
                MessageBox.Show("The question title cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            selectedQuestion.Title = newTitle;

            // Update answers based on the question type
            try
            {
                switch (selectedQuestion.Type)
                {
                    case QuestionType.MultipleChoice:
                        UpdateMultipleChoiceOptions((MultipleChoiceQuestion)selectedQuestion);
                        break;

                    case QuestionType.OpenEnded:
                        UpdateOpenEndedAnswer((OpenEndedQuestion)selectedQuestion);
                        break;

                    case QuestionType.TrueFalse:
                        UpdateTrueFalseAnswer((TrueFalseQuestion)selectedQuestion);
                        break;

                    default:
                        MessageBox.Show("Unsupported question type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update answers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Save the updated question to the database
            try
            {
                _gameCreator.UpdateQuestion(selectedQuestion);
                MessageBox.Show("Question updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh the DataGridView
                dgvQuestion.DataSource = null;
                dgvQuestion.DataSource = _tempQuestions;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the question: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateMultipleChoiceOptions(MultipleChoiceQuestion question)
        {
            question.Options.Clear();
            foreach (Control control in groupBoxAnswers.Controls)
            {
                if (control is RadioButton radioButton)
                {
                    question.Options.Add(radioButton.Text.Trim());
                }
            }
            question.CorrectAnswerIndex = groupBoxAnswers.Controls.OfType<RadioButton>()
                .ToList()
                .FindIndex(rb => rb.Checked);
        }

        private void UpdateOpenEndedAnswer(OpenEndedQuestion question)
        {
            var textBox = groupBoxAnswers.Controls.OfType<TextBox>().FirstOrDefault();
            if (textBox != null)
            {
                question.AcceptableAnswers.Add(textBox.Text.Trim());
            }
        }

        private void UpdateTrueFalseAnswer(TrueFalseQuestion question)
        {
            var selectedRadioButton = groupBoxAnswers.Controls.OfType<RadioButton>()
                .FirstOrDefault(rb => rb.Checked);

            if (selectedRadioButton != null)
            {
                question.IsTrue = (bool)selectedRadioButton.Tag;
            }
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            // Get the search text
            string searchText = SearchTxtBox.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("Please enter a search term.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Filter questions based on the search term
            var filteredQuestions = _tempQuestions
                .Where(q => q.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (filteredQuestions.Count == 0)
            {
                MessageBox.Show("No questions found matching the search term.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Update the DataGridView with the filtered questions
            dgvQuestion.DataSource = null; // Clear the existing data source
            dgvQuestion.DataSource = filteredQuestions; // Bind the filtered list

            // Optionally select the first question from the filtered results
            if (filteredQuestions.Any())
            {
                DisplayQuestion(filteredQuestions[0]);
            }
        }

        private void ViewResultBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var players = _gameCreator.GetAllPlayersWithResults();

                if (players.Count == 0)
                {
                    MessageBox.Show("No players found.", "Players", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Format the list into a single string
                string playerList = string.Join(Environment.NewLine, players);

                // Display the players in a MessageBox
                MessageBox.Show(playerList, "Players & Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching players and results: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

