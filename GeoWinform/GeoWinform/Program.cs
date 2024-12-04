using static System.Diagnostics.Debug;
using SQLitePCL;
using System;
using Microsoft.Data.Sqlite;
using System.Diagnostics;
using System.Xml.Linq;

namespace GeoWinform
{
    public enum QuestionType
    {
        MultipleChoice = 1,
        OpenEnded = 2,
        TrueFalse = 3,
    }


    public abstract class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public QuestionType Type { get; set; }

        protected Question(int id, string title, QuestionType type)
        {
            Id = id;
            Title = title;
            Type = type;
        }

        public abstract bool CheckAnswer(string answer);

        /// <summary>
        /// Factory method to create a Question object from database data.
        /// </summary>
        public static Question CreateQuestion(
            int id,
            string title,
            QuestionType type,
            List<(string AnswerText, bool IsTrue)> answers)
        {
            return type switch
            {
                QuestionType.MultipleChoice => new MultipleChoiceQuestion(
                    id,
                    title,
                    answers.Select(a => a.AnswerText).ToList(),
                    answers.FindIndex(a => a.IsTrue)),

                QuestionType.OpenEnded => new OpenEndedQuestion(
                    id,
                    title,
                    answers.Select(a => a.AnswerText).ToList()),

                QuestionType.TrueFalse => new TrueFalseQuestion(
                    id,
                    title,
                    answers.Any(a => a.IsTrue)),

                _ => throw new ArgumentException("Unknown question type")
            };
        }
    }

    public class MultipleChoiceQuestion : Question
    {
        public List<string> Options { get; set; }
        public int CorrectAnswerIndex { get; set; }

        public MultipleChoiceQuestion(int id, string text, List<string> options, int correctAnswerIndex)
            : base(id, text, QuestionType.MultipleChoice)
        {
            Options = options;
            CorrectAnswerIndex = correctAnswerIndex;
        }

        public override bool CheckAnswer(string answer)
        {
            if (int.TryParse(answer, out int selectedOption))
            {
                return selectedOption == CorrectAnswerIndex;
            }
            return false;
        }
    }

    public class OpenEndedQuestion : Question
    {
        // List to store multiple acceptable answers
        public List<string> AcceptableAnswers { get; set; }

        // Constructor accepting multiple acceptable answers
        public OpenEndedQuestion(int id, string text, List<string> acceptableAnswers)
            : base(id, text, QuestionType.OpenEnded)
        {
            AcceptableAnswers = acceptableAnswers;
        }

        // Constructor accepting a single correct answer and internally creating the list
        public OpenEndedQuestion(int id, string text, string acceptableAnswer)
            : base(id, text, QuestionType.OpenEnded)
        {
            AcceptableAnswers = new List<string> { acceptableAnswer };
        }

        // Overridden method to check if the answer is correct
        public override bool CheckAnswer(string answer)
        {
            // Ensure that both the user's answer and acceptable answers are trimmed and case-insensitive
            return AcceptableAnswers.Any(a => a.Equals(answer.Trim(), StringComparison.OrdinalIgnoreCase));
        }
    }


    public class TrueFalseQuestion : Question
    {
        public bool IsTrue { get; set; }

        public TrueFalseQuestion(int id, string text, bool isTrue)
            : base(id, text, QuestionType.TrueFalse)
        {
            IsTrue = isTrue;
        }

        public override bool CheckAnswer(string answer)
        {
            return (IsTrue && answer.Equals("true", StringComparison.OrdinalIgnoreCase)) ||
                   (!IsTrue && answer.Equals("false", StringComparison.OrdinalIgnoreCase));
        }

    }

    public class ThemeManager
    {
        public static string CurrentTheme = "Light";
    // Define a struct to hold theme details
    public struct Theme
        {
            public Color FormBackColor;
            public Color ControlBackColor;
            public Color ControlForeColor;
            public Color PanelBackColor;
            public Color LabelForeColor;
            public Color ButtonBackColor;
            public Color ButtonForeColor;
            public FlatStyle ButtonFlatStyle;
            public int ButtonBorderSize;
        }

        // Predefined themes
        public static readonly Dictionary<string, Theme> Themes = new()
        {
            ["Light"] = new Theme
            {
                FormBackColor = Color.LightSkyBlue,
                ControlBackColor = Color.WhiteSmoke,
                ControlForeColor = Color.DarkBlue,
                PanelBackColor = Color.LightCyan,
                LabelForeColor = Color.DarkSlateGray,
                ButtonBackColor = Color.MediumSeaGreen,
                ButtonForeColor = Color.White,
                ButtonFlatStyle = FlatStyle.Flat,
                ButtonBorderSize = 0
            },
            ["Dark"] = new Theme
            {
                FormBackColor = Color.FromArgb(30, 30, 30),
                ControlBackColor = Color.FromArgb(45, 45, 45),
                ControlForeColor = Color.White,
                PanelBackColor = Color.FromArgb(60, 60, 60),
                LabelForeColor = Color.LightGray,
                ButtonBackColor = Color.DimGray,
                ButtonForeColor = Color.White,
                ButtonFlatStyle = FlatStyle.Flat,
                ButtonBorderSize = 0
            },
            ["HighContrast"] = new Theme
            {
                FormBackColor = Color.Black,
                ControlBackColor = Color.Yellow,
                ControlForeColor = Color.Black,
                PanelBackColor = Color.Gray,
                LabelForeColor = Color.White,
                ButtonBackColor = Color.Red,
                ButtonForeColor = Color.White,
                ButtonFlatStyle = FlatStyle.Flat,
                ButtonBorderSize = 1
            }
        };
        // Public method to set the default theme for the controls
        public static void SetTheme(Form form, string themeName = "Light")
        {
            if (!Themes.TryGetValue(themeName, out var theme))
            {
                throw new ArgumentException($"Theme '{themeName}' is not defined.");
            }

            // Apply theme to form and controls
            form.BackColor = theme.FormBackColor;
            foreach (Control control in form.Controls)
            {
                switch (control)
                {
                    case Button button:
                        SetButtonStyle(button, theme);
                        break;

                    case TextBox textBox:
                        textBox.BackColor = theme.ControlBackColor;
                        textBox.ForeColor = theme.ControlForeColor;
                        break;

                    case ComboBox comboBox:
                        comboBox.BackColor = theme.ControlBackColor;
                        comboBox.ForeColor = theme.ControlForeColor;
                        break;

                    case Label label:
                        label.ForeColor = theme.LabelForeColor;
                        break;

                    case Panel panel:
                        panel.BackColor = theme.PanelBackColor;
                        break;
                }
            }
        }

        // Helper method to set button styles
        private static void SetButtonStyle(Button button, Theme theme)
        {
            button.BackColor = theme.ButtonBackColor;
            button.ForeColor = theme.ButtonForeColor;
            button.FlatStyle = theme.ButtonFlatStyle;
            button.FlatAppearance.BorderSize = theme.ButtonBorderSize;
        }

        public static void ApplyThemeToAllForms()
        {
            foreach (Form form in Application.OpenForms)
            {
                ThemeManager.SetTheme(form, CurrentTheme);
            }
        }

        private void ChangeTheme(string newTheme)
        {
            ThemeManager.CurrentTheme = newTheme; // Updates global theme
            ThemeManager.ApplyThemeToAllForms(); // Apply theme to all currently open forms
        }


    }

    public class GameCreator
    {
        private readonly string _connectionString;

        public GameCreator(string connectionString)
        {
            _connectionString = connectionString;
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var createQuestionsTableQuery = @"
                CREATE TABLE IF NOT EXISTS Questions (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    QuestionType TEXT NOT NULL
                )";

                var createAnswersTableQuery = @"
                CREATE TABLE IF NOT EXISTS Answers (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    AnswerText TEXT NOT NULL,
                    QuestionId INTEGER NOT NULL,
                    IsTrue BOOLEAN,
                    FOREIGN KEY (QuestionId) REFERENCES Questions(Id)
                )";

                var createPlayersTableQuery = @"
                CREATE TABLE IF NOT EXISTS Players (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL UNIQUE,
                    Password TEXT NOT NULL,
                    IsGameMaster BOOLEAN NOT NULL DEFAULT 0
                )";

                var createResultsTableQuery = @"
                CREATE TABLE IF NOT EXISTS Results (
                    ResultId INTEGER PRIMARY KEY AUTOINCREMENT,  
                    PlayerId INTEGER NOT NULL,                   
                    CorrectAnswers INTEGER NOT NULL,            
                    TotalQuestions INTEGER NOT NULL,            
                    TimeTakenSeconds INTEGER NOT NULL,          
                    QuizDate DATETIME DEFAULT CURRENT_TIMESTAMP, 
                    FOREIGN KEY (PlayerId) REFERENCES Players(Id) ON DELETE CASCADE
                );";

                using (var command = new SqliteCommand(createQuestionsTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (var command = new SqliteCommand(createAnswersTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (var command = new SqliteCommand(createPlayersTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (var command = new SqliteCommand(createResultsTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RegisterUser(string name, string password, bool isGameMaster)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            const string query = @"
                INSERT INTO Players (Name, Password, IsGameMaster)
                VALUES (@Name, @Password, @IsGameMaster)";

            using var command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@IsGameMaster", isGameMaster ? 1 : 0); // 1 for GameMaster, 0 for Player

            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqliteException ex) when (ex.SqliteErrorCode == 19) // UNIQUE constraint violation
            {
                throw new InvalidOperationException("A user with this name already exists.", ex);
            }
        }


        public void AddQuestion(Question question)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();
            // Insert the question
            var insertQuestionQuery = @"
                    INSERT INTO Questions (Title, QuestionType) 
                    VALUES (@Title, @QuestionType);
                    SELECT last_insert_rowid();";

            long questionId;
            using (var command = new SqliteCommand(insertQuestionQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@Title", question.Title);
                command.Parameters.AddWithValue("@QuestionType", question.Type.ToString());

                questionId = (long)command.ExecuteScalar();
            }

            // Insert answers based on question type
            if (question is MultipleChoiceQuestion mcQuestion)
            {
                for (int i = 0; i < mcQuestion.Options.Count; i++)
                {
                    AddAnswer(connection, transaction, mcQuestion.Options[i], questionId, i == mcQuestion.CorrectAnswerIndex);
                }
            }
            else if (question is OpenEndedQuestion oeQuestion)
            {
                foreach (var answer in oeQuestion.AcceptableAnswers)
                {
                    AddAnswer(connection, transaction, answer, questionId, true);
                }
            }
            else if (question is TrueFalseQuestion tfQuestion)
            {
                AddAnswer(connection, transaction, "True", questionId, tfQuestion.IsTrue);
                AddAnswer(connection, transaction, "False", questionId, !tfQuestion.IsTrue);
            }

            transaction.Commit();
        }

        private void AddAnswer(SqliteConnection connection, SqliteTransaction transaction, string answerText, long questionId, bool isTrue)
        {
            var insertAnswerQuery = @"
            INSERT INTO Answers (AnswerText, QuestionId, IsTrue) 
            VALUES (@AnswerText, @QuestionId, @IsTrue)";

            using (var command = new SqliteCommand(insertAnswerQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@AnswerText", answerText);
                command.Parameters.AddWithValue("@QuestionId", questionId);
                command.Parameters.AddWithValue("@IsTrue", isTrue);
                command.ExecuteNonQuery();
            }
        }

        // Additional methods for fetching, updating, and deleting questions
        public List<Question> GetAllQuestions()
        {
            var questions = new List<Question>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var queryQuestions = "SELECT * FROM Questions";

                using var command = new SqliteCommand(queryQuestions, connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string title = reader.GetString(1);
                    string type = reader.GetString(2);

                    QuestionType questionType = Enum.Parse<QuestionType>(type);

                    // Retrieve and create each question based on its type
                    questions.Add(questionType switch
                    {
                        QuestionType.MultipleChoice => GetMultipleChoiceQuestion(id, title),
                        QuestionType.OpenEnded => GetOpenEndedQuestion(id, title),
                        QuestionType.TrueFalse => GetTrueFalseQuestion(id, title),
                        _ => throw new Exception("Unknown question type.")
                    });
                }
            }
            return questions;
        }

        private MultipleChoiceQuestion GetMultipleChoiceQuestion(int questionId, string title)
        {
            var options = new List<string>();
            int correctAnswerIndex = -1;

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var queryOptions = "SELECT AnswerText, IsTrue FROM Answers WHERE QuestionId = @QuestionId";
                using var command = new SqliteCommand(queryOptions, connection);
                command.Parameters.AddWithValue("@QuestionId", questionId);
                using var reader = command.ExecuteReader();
                int index = 0;
                while (reader.Read())
                {
                    string optionText = reader.GetString(0);
                    bool isTrue = reader.GetBoolean(1);

                    options.Add(optionText);
                    if (isTrue) correctAnswerIndex = index;

                    index++;
                }
            }

            return new MultipleChoiceQuestion(questionId, title, options, correctAnswerIndex);
        }

        private OpenEndedQuestion GetOpenEndedQuestion(int questionId, string title)
        {
            var acceptableAnswers = new List<string>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var queryAnswers = "SELECT AnswerText FROM Answers WHERE QuestionId = @QuestionId";
                using var command = new SqliteCommand(queryAnswers, connection);
                command.Parameters.AddWithValue("@QuestionId", questionId);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    acceptableAnswers.Add(reader.GetString(0));
                }
            }

            return new OpenEndedQuestion(questionId, title, acceptableAnswers);
        }

        private TrueFalseQuestion GetTrueFalseQuestion(int questionId, string title)
        {
            bool isTrue = false;

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var queryAnswer = "SELECT AnswerText, IsTrue FROM Answers WHERE QuestionId = @QuestionId";
                using var command = new SqliteCommand(queryAnswer, connection);
                command.Parameters.AddWithValue("@QuestionId", questionId);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string answerText = reader.GetString(0);
                    isTrue = answerText.Equals("True", StringComparison.OrdinalIgnoreCase) && reader.GetBoolean(1);
                }
            }

            return new TrueFalseQuestion(questionId, title, isTrue);
        }


        /// <summary>
        /// Updates an existing question in the database.
        /// </summary>
        /// <param name="questionId">The ID of the question to update.</param>
        /// <param name="newTitle">The new question text.</param>
        /// 
        public void UpdateQuestion(Question updatedQuestion)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var transaction = connection.BeginTransaction();
            try
            {
                // Update the question title and type
                const string updateQuestionQuery = @"
            UPDATE Questions
            SET Title = @Title, QuestionType = @QuestionType
            WHERE Id = @QuestionId";
                using (var updateQuestionCommand = new SqliteCommand(updateQuestionQuery, connection, transaction))
                {
                    updateQuestionCommand.Parameters.AddWithValue("@Title", updatedQuestion.Title);
                    updateQuestionCommand.Parameters.AddWithValue("@QuestionType", updatedQuestion.Type.ToString());
                    updateQuestionCommand.Parameters.AddWithValue("@QuestionId", updatedQuestion.Id);
                    updateQuestionCommand.ExecuteNonQuery();
                }

                // Update the answers based on the question type
                switch (updatedQuestion)
                {
                    case MultipleChoiceQuestion multipleChoice:
                        UpdateMultipleChoiceAnswers(multipleChoice, connection, transaction);
                        break;

                    case OpenEndedQuestion openEnded:
                        UpdateOpenEndedAnswer(openEnded, connection, transaction);
                        break;

                    case TrueFalseQuestion trueFalse:
                        UpdateTrueFalseAnswer(trueFalse, connection, transaction);
                        break;

                    default:
                        throw new NotSupportedException("Unsupported question type for update.");
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Debug.WriteLine($"Error updating question: {ex.Message}");
                throw new Exception("An error occurred while updating the question.", ex);
            }
        }

        private void UpdateMultipleChoiceAnswers(MultipleChoiceQuestion question, SqliteConnection connection, SqliteTransaction transaction)
        {
            // Delete existing answers for the question
            const string deleteAnswersQuery = "DELETE FROM Answers WHERE QuestionId = @QuestionId";
            using (var deleteAnswersCommand = new SqliteCommand(deleteAnswersQuery, connection, transaction))
            {
                deleteAnswersCommand.Parameters.AddWithValue("@QuestionId", question.Id);
                deleteAnswersCommand.ExecuteNonQuery();
            }

            // Insert updated answers
            const string insertAnswerQuery = @"
        INSERT INTO Answers (QuestionId, AnswerText, IsTrue)
        VALUES (@QuestionId, @AnswerText, @IsTrue)";
            foreach (var (option, index) in question.Options.Select((opt, idx) => (opt, idx)))
            {
                using var insertAnswerCommand = new SqliteCommand(insertAnswerQuery, connection, transaction);
                insertAnswerCommand.Parameters.AddWithValue("@QuestionId", question.Id);
                insertAnswerCommand.Parameters.AddWithValue("@AnswerText", option);
                insertAnswerCommand.Parameters.AddWithValue("@IsTrue", index == question.CorrectAnswerIndex);
                insertAnswerCommand.ExecuteNonQuery();
            }
        }

        private void UpdateOpenEndedAnswer(OpenEndedQuestion question, SqliteConnection connection, SqliteTransaction transaction)
        {
            // Delete existing answer for the question
            const string deleteAnswersQuery = "DELETE FROM Answers WHERE QuestionId = @QuestionId";
            using (var deleteAnswersCommand = new SqliteCommand(deleteAnswersQuery, connection, transaction))
            {
                deleteAnswersCommand.Parameters.AddWithValue("@QuestionId", question.Id);
                deleteAnswersCommand.ExecuteNonQuery();
            }

            // Insert the updated answer
            const string insertAnswerQuery = @"
        INSERT INTO Answers (QuestionId, AnswerText, IsTrue)
        VALUES (@QuestionId, @AnswerText, @IsTrue)";
            using var insertAnswerCommand = new SqliteCommand(insertAnswerQuery, connection, transaction);
            insertAnswerCommand.Parameters.AddWithValue("@QuestionId", question.Id);
            insertAnswerCommand.Parameters.AddWithValue("@AnswerText", question.AcceptableAnswers);
            insertAnswerCommand.Parameters.AddWithValue("@IsTrue", true);
            insertAnswerCommand.ExecuteNonQuery();
        }

        private void UpdateTrueFalseAnswer(TrueFalseQuestion question, SqliteConnection connection, SqliteTransaction transaction)
        {
            // Delete existing answer for the question
            const string deleteAnswersQuery = "DELETE FROM Answers WHERE QuestionId = @QuestionId";
            using (var deleteAnswersCommand = new SqliteCommand(deleteAnswersQuery, connection, transaction))
            {
                deleteAnswersCommand.Parameters.AddWithValue("@QuestionId", question.Id);
                deleteAnswersCommand.ExecuteNonQuery();
            }

            // Insert True/False answers
            const string insertAnswerQuery = @"
        INSERT INTO Answers (QuestionId, AnswerText, IsTrue)
        VALUES (@QuestionId, @AnswerText, @IsTrue)";

            // True answer
            using (var trueAnswerCommand = new SqliteCommand(insertAnswerQuery, connection, transaction))
            {
                trueAnswerCommand.Parameters.AddWithValue("@QuestionId", question.Id);
                trueAnswerCommand.Parameters.AddWithValue("@AnswerText", "True");
                trueAnswerCommand.Parameters.AddWithValue("@IsTrue", question.IsTrue.ToString());
                trueAnswerCommand.ExecuteNonQuery();
            }

            //// False answer
            //using (var falseAnswerCommand = new SqliteCommand(insertAnswerQuery, connection, transaction))
            //{
            //    falseAnswerCommand.Parameters.AddWithValue("@QuestionId", question.Id);
            //    falseAnswerCommand.Parameters.AddWithValue("@AnswerText", "False");
            //    falseAnswerCommand.Parameters.AddWithValue("@IsCorrect", question.IsTrue.ToString());
            //    falseAnswerCommand.ExecuteNonQuery();
            //}
        }


        private void DeleteAnswersByQuestionId(SqliteConnection connection, SqliteTransaction transaction, int questionId)
        {
            var deleteAnswersQuery = "DELETE FROM Answers WHERE QuestionId = @QuestionId";
            using var command = new SqliteCommand(deleteAnswersQuery, connection, transaction);
            command.Parameters.AddWithValue("@QuestionId", questionId);
            command.ExecuteNonQuery();
        }


        /// <summary>
        /// Deletes a question and its associated answers from the database.
        /// </summary>
        /// <param name="questionId">The ID of the question to delete.</param>
        public void DeleteQuestion(int questionId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var transaction = connection.BeginTransaction();
            try
            {
                // Delete associated answers first
                var deleteAnswersQuery = "DELETE FROM Answers WHERE QuestionId = @QuestionId";
                using (var deleteAnswersCommand = new SqliteCommand(deleteAnswersQuery, connection, transaction))
                {
                    deleteAnswersCommand.Parameters.AddWithValue("@QuestionId", questionId);
                    deleteAnswersCommand.ExecuteNonQuery();
                }

                // Delete the question
                var deleteQuestionQuery = "DELETE FROM Questions WHERE Id = @QuestionId";
                using (var deleteQuestionCommand = new SqliteCommand(deleteQuestionQuery, connection, transaction))
                {
                    deleteQuestionCommand.Parameters.AddWithValue("@QuestionId", questionId);
                    deleteQuestionCommand.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                // Rollback transaction if there's an error
                transaction.Rollback();
                Debug.WriteLine($"Error deleting question: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes an answer from the database.
        /// </summary>
        /// <param name="answerId">The ID of the answer to delete.</param>
        public void DeleteAnswer(int answerId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var deleteAnswerQuery = "DELETE FROM Answers WHERE Id = @AnswerId";
            using var deleteAnswerCommand = new SqliteCommand(deleteAnswerQuery, connection);
            deleteAnswerCommand.Parameters.AddWithValue("@AnswerId", answerId);

            try
            {
                deleteAnswerCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting answer: {ex.Message}");
            }
        }

        public int GetLastInsertedId()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            const string query = "SELECT last_insert_rowid();";
            using var command = new SqliteCommand(query, connection);
            return Convert.ToInt32(command.ExecuteScalar());
        }

        public int GetNextQuestionId()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            const string query = "SELECT IFNULL(MAX(Id), 0) + 1 FROM Questions;";
            using var command = new SqliteCommand(query, connection);
            return Convert.ToInt32(command.ExecuteScalar());
        }

        public List<string> GetAllPlayersWithResults()
        {
            var playersWithResults = new List<string>();

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            const string query = @"
        SELECT 
            p.Id AS PlayerId, 
            p.Name AS PlayerName, 
            r.CorrectAnswers, 
            r.TotalQuestions, 
            r.TimeTakenSeconds, 
            r.QuizDate
        FROM Players p
        LEFT JOIN Results r ON p.Id = r.PlayerId
        ORDER BY p.Id, r.QuizDate";

            using var command = new SqliteCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                int playerId = reader.GetInt32(0);
                string playerName = reader.GetString(1);
                int? correctAnswers = reader.IsDBNull(2) ? null : reader.GetInt32(2);
                int? totalQuestions = reader.IsDBNull(3) ? null : reader.GetInt32(3);
                int? timeTaken = reader.IsDBNull(4) ? null : reader.GetInt32(4);
                string quizDate = reader.IsDBNull(5) ? "No results yet" : reader.GetDateTime(5).ToString("yyyy-MM-dd HH:mm");

                if (correctAnswers.HasValue)
                {
                    playersWithResults.Add(
                        $"Player: {playerName} (ID: {playerId}) | Correct: {correctAnswers}/{totalQuestions} | Time: {timeTaken}s | Date: {quizDate}");
                }
                else
                {
                    playersWithResults.Add($"Player: {playerName} (ID: {playerId}) | No results yet.");
                }
            }

            return playersWithResults;
        }



    }
    // Gameplayer class 
    public class GamePlayer
    {

        private readonly string _connectionString;
        private Stopwatch QuizTimer { get; set; }
        public int _playerId;

        public TimeSpan TimeSpentOnQuiz { get; private set; }

        // Constructor
        public GamePlayer(string connectionString)
        {
            QuizTimer = new Stopwatch();
            _connectionString = connectionString;
        }

        // Method to start quiz timer
        public void StartQuizTimer()
        {
            QuizTimer.Restart();
        }

        public TimeSpan ElaspedTime => QuizTimer.Elapsed;
        // Method to stop quiz timer and store the elapsed time
        public void StopQuizTimer()
        {
            if (QuizTimer.IsRunning)
            {
                QuizTimer.Stop();
                TimeSpentOnQuiz = QuizTimer.Elapsed;
            }
            else
            {
                throw new InvalidOperationException("Quiz timer was not started.");
            }
        }

        public static int CountCorrectAnswers(List<Question> quizQuestions, List<string> playerAnswers)
        {
            if (quizQuestions == null || playerAnswers == null)
            {
                throw new ArgumentNullException("Quiz questions or player answers cannot be null.");
            }

            if (quizQuestions.Count != playerAnswers.Count)
            {
                throw new ArgumentException("The number of player answers must match the number of quiz questions.");
            }

            int correctAnswerCount = 0;

            for (int i = 0; i < quizQuestions.Count; i++)
            {
                // Check if the player's answer is correct
                if (quizQuestions[i].CheckAnswer(playerAnswers[i]))
                {
                    correctAnswerCount++;
                }
            }

            return correctAnswerCount;
        }

        public void SaveQuizResults(int playerId, int correctAnswers, int totalQuestions, TimeSpan timeTaken)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var query = @"
                INSERT INTO Results (PlayerId, CorrectAnswers, TotalQuestions, TimeTakenSeconds)
                VALUES (@PlayerId, @CorrectAnswers, @TotalQuestions, @TimeTakenSeconds)";

            using var command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@PlayerId", playerId);
            command.Parameters.AddWithValue("@CorrectAnswers", correctAnswers);
            command.Parameters.AddWithValue("@TotalQuestions", totalQuestions);
            command.Parameters.AddWithValue("@TimeTakenSeconds", (int)timeTaken.TotalSeconds);

            command.ExecuteNonQuery();
        }

        public bool Login(string username, string password)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            const string query = @"
            SELECT Id
            FROM Players 
            WHERE Name = @Name AND Password = @Password";

            using var command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@Name", username);
            command.Parameters.AddWithValue("@Password", password);

            var result = command.ExecuteScalar(); // Use ExecuteScalar to retrieve a single value

            if (result != null) // Check if a result was returned
            {
                _playerId = Convert.ToInt32(result);
                return true;
            }
            return false;
        }

        public bool IsGameMaster(string username)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            const string query = @"
            SELECT IsGameMaster 
            FROM Players 
            WHERE Name = @Name";

            using var command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@Name", username);

            object result = command.ExecuteScalar();
            return result != null && Convert.ToBoolean(result);
        }

        // Method to get the total quiz time
        public TimeSpan GetQuizTime()
        {
            if (QuizTimer.IsRunning)
            {
                throw new InvalidOperationException("Quiz timer is still running. Please stop it before fetching the elapsed time.");
            }

            return TimeSpentOnQuiz;
        }
   

    }


    // 


    public class GlobalThemeMessageFilter : IMessageFilter
    {
        public bool PreFilterMessage(ref Message m)
        {
            // Check for WM_SHOWWINDOW message to detect when a form is being shown
            const int WM_SHOWWINDOW = 0x0018;

            if (m.Msg == WM_SHOWWINDOW && Control.FromHandle(m.HWnd) is Form form)
            {
                ThemeManager.SetTheme(form, ThemeManager.CurrentTheme); // Apply the global theme
            }

            return false; // Allow the message to proceed
        }
    }


    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            string databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "quiz.db");
            string connectionString = $"Data Source={databasePath}";


            ApplicationConfiguration.Initialize();

            // Try to load FOT-Yuruka Std UB font
            try
            {
                // Attempt to set the custom font
                Application.SetDefaultFont(new Font(new FontFamily("FOT-Yuruka Std UB"), 8f));
            }
            catch (ArgumentException)
            {
                // If the custom font is not found, set a fallback font
                Debug.WriteLine("Font 'FOT-Yuruka Std UB' not found. Using default fallback font.");
                Application.SetDefaultFont(new Font(FontFamily.GenericSansSerif, 8f)); // Use a generic fallback font
            }
            MainForm mainForm = new MainForm(connectionString);
            Application.Run(mainForm);
        }
    }
}