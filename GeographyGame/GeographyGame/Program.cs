using Geography;
using System;
using System.Diagnostics;

namespace Geography;

# nullable disable
// Console.ReadLine() will only return null if the end of input has been reached for redirected input,
// and if it has, reading it again will simply get you null again, so this becomes an infinite loop.
// In an interactive setup
// , if the user enters nothing (simply presses Enter), it will return an empty string, not null
// https://stackoverflow.com/questions/70291276/converting-null-literal-for-console-readline-for-string-input
public enum QuestionType
{
    MultipleChoice = 1,
    OpenEnded = 2,
    TrueFalse = 3,
}

public abstract class Question
{

    public string Title { get; set; }

    public QuestionType Type { get; set; }

    public Question(string title, QuestionType questionType)
    {
        Title = title;
        Type = questionType;
    }

    public abstract bool CheckAnswer(string answer);
}

public class MultipleChoiceQuestion : Question
{
    public List<string> Options { get; set; }
    public int CorrectAnswerIndex { get; set; }

    public MultipleChoiceQuestion(string text, List<string> options, int correctAnswerIndex)
        : base(text, QuestionType.MultipleChoice)
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
    public OpenEndedQuestion(string text, List<string> acceptableAnswers)
        : base(text, QuestionType.OpenEnded)
    {
        AcceptableAnswers = acceptableAnswers;
    }

    // Constructor accepting a single correct answer and internally creating the list
    public OpenEndedQuestion(string text, string acceptableAnswer)
        : base(text, QuestionType.OpenEnded)
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

    public TrueFalseQuestion(string text, bool isTrue)
        : base(text, QuestionType.TrueFalse)
    {
        IsTrue = isTrue;
    }

    public override bool CheckAnswer(string answer)
    {
        return (IsTrue && answer.Equals("true", StringComparison.OrdinalIgnoreCase)) ||
               (!IsTrue && answer.Equals("false", StringComparison.OrdinalIgnoreCase));
    }

}

// The class that handles creating, editing, and deleting questions.
public class GameCreator
{
    private List<Question> questions;

    public GameCreator()
    {
        questions = new List<Question>();
    }

    public void AddQuestion(Question question)
    {
        questions.Add(question);
    }

    public void EditQuestion(int index, Question updatedQuestion)
    {
        if (index >= 0 && index < questions.Count)
        {
            questions[index] = updatedQuestion;
        }
        else
        {
            Console.WriteLine("Invalid question index.");
        }
    }

    public void DeleteQuestion(int index)
    {
        if (index >= 0 && index < questions.Count)
        {
            questions.RemoveAt(index);
        }
        else
        {
            Console.WriteLine("Invalid question index.");
        }
    }

    public List<Question> GetQuestions()
    {
        return questions;
    }

    // Methods to create different types of questions

    public Question CreateMultipleChoiceQuestion()
    {
        Console.WriteLine("\nEnter the question text for the Multiple Choice question:");
        string questionText = Console.ReadLine();

        Console.WriteLine("Enter number of choices");
        int answer_cnt = int.Parse(Console.ReadLine());
        
        Console.WriteLine($"Enter the possible answers ({answer_cnt} options):");


        List<string> options = new List<string>();
        for (int i = 0; i < answer_cnt; i++)
        {
            Console.WriteLine($"Option {i + 1}:");
            options.Add(Console.ReadLine());
        }


        Console.WriteLine("Enter the index of the correct answer (0-3):");
        int correctAnswerIndex = int.Parse(Console.ReadLine());

        return new MultipleChoiceQuestion(questionText, options, correctAnswerIndex);
    }

    public Question CreateOpenEndedQuestion()
    {
        Console.WriteLine("\nEnter the question text for the Open-Ended question:");
        string questionText = Console.ReadLine();

        Console.WriteLine("Enter the correct answer (1-4 words):");
        string correctAnswer = Console.ReadLine();

        return new OpenEndedQuestion(questionText, correctAnswer);
    }

    public Question CreateTrueFalseQuestion()
    {
        Console.WriteLine("\nEnter the statement for the True/False question:");
        string questionText = Console.ReadLine();

        Console.WriteLine("Enter the correct answer (true/false):");
        bool correctAnswer = bool.Parse(Console.ReadLine());

        return new TrueFalseQuestion(questionText, correctAnswer);
    }
}



public class GamePlayer
{
    private List<Question> questions;
    private int correctAnswers;
    private Stopwatch stopwatch;

    public GamePlayer(List<Question> questions)
    {
        this.questions = questions;
        this.correctAnswers = 0;
        this.stopwatch = new Stopwatch();
    }

    public void StartGame()
    {
        stopwatch.Start();
        correctAnswers = 0;

        for (int i = 0; i < questions.Count; i++)
        {
            Console.WriteLine($"Question {i + 1}: {questions[i].Title}");

            // For multiple-choice questions, show the options
            if (questions[i] is MultipleChoiceQuestion mcq)
            {
                for (int j = 0; j < mcq.Options.Count; j++)
                {
                    Console.WriteLine($"{j}: {mcq.Options[j]}");
                }
            }

            string answer = Console.ReadLine();

            // Check the answer
            if (questions[i].CheckAnswer(answer))
            {
                correctAnswers++;
            }
        }

        stopwatch.Stop();
        DisplayResults();
    }

    private void DisplayResults()
    {
        // Display the score
        Console.WriteLine($"\nYou answered {correctAnswers} out of {questions.Count} questions correctly.");

        // Display the time spent
        double minutesSpent = stopwatch.Elapsed.TotalMinutes;
        Console.WriteLine($"Time spent: {minutesSpent:F2} minutes.");

        // Ask if the user wants to see the correct answers
        Console.WriteLine("Would you like to see the correct answers? (yes/no)");
        string showAnswers = Console.ReadLine();

        if (showAnswers.ToLower() == "yes")
        {
            ShowCorrectAnswers();
        }

        // Ask if the user wants to play again or exit
        Console.WriteLine("Would you like to start again? (yes/no)");
        string playAgain = Console.ReadLine();

        if (playAgain.ToLower() == "yes")
        {
            // Optionally restart the game (you could delegate this to the main class)
            StartGame();
        }
        else
        {
            Console.WriteLine("Thank you for playing!");
        }
    }

    private void ShowCorrectAnswers()
    {
        Console.WriteLine("\nCorrect Answers:");
        for (int i = 0; i < questions.Count; i++)
        {
            Console.WriteLine($"Question {i + 1}: {questions[i].Title}");

            if (questions[i] is MultipleChoiceQuestion mcq)
            {
                Console.WriteLine($"Correct Answer: {mcq.Options[mcq.CorrectAnswerIndex]}");
            }
            else if (questions[i] is OpenEndedQuestion oe)
            {
                Console.WriteLine($"Correct Answer: {string.Join(", ", oe.AcceptableAnswers)}");
            }
            else if (questions[i] is TrueFalseQuestion tf)
            {
                Console.WriteLine($"Correct Answer: {(tf.IsTrue ? "True" : "False")}");
            }
        }
    }
}


// Main class responsible for overall game flow.
public class Game
{
    private GameCreator gameCreator;
    private GamePlayer gamePlayer;

    public Game()
    {
        gameCreator = new GameCreator();
    }

    public void Start()
    {
        bool isRunning = true;

        // Main game loop
        while (isRunning)
        {
            Console.WriteLine("\nWelcome to the Quiz Game!");
            Console.WriteLine("Select Mode:");
            Console.WriteLine("1. Create a Game");
            Console.WriteLine("2. Play the Game");
            Console.WriteLine("3. Quit");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    RunGameCreationMode();
                    break;
                case "2":
                    RunGamePlayMode();
                    break;
                case "3":
                    isRunning = false;
                    Console.WriteLine("Thank you for playing! Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please select 1, 2, or 3.");
                    break;
            }
        }
    }

    private void RunGameCreationMode()
    {
        Console.WriteLine("\nGame Creation Mode");

        int maxQuestions = 10;
        int currentQuestionCount = 0;

        while (true)
        {
            Console.WriteLine("\nCurrent number of questions: " + currentQuestionCount);
            Console.WriteLine("Would you like to add a new question? (yes/no or stop)");
            string userInput = Console.ReadLine().ToLower();

            if (userInput == "no" || userInput == "stop")
            {
                Console.WriteLine("Exiting Game Creation Mode.");
                break;
            }

            if (currentQuestionCount >= maxQuestions)
            {
                Console.WriteLine($"You have reached the maximum limit of {maxQuestions} questions.");
                break;
            }

            Console.WriteLine("\nSelect the type of question to add:");
            Console.WriteLine("1. Multiple Choice");
            Console.WriteLine("2. Open-Ended");
            Console.WriteLine("3. True/False");

            if (Enum.TryParse(Console.ReadLine(), out QuestionType questionType))
            {
                Question newQuestion = null;

                switch (questionType)
                {
                    case QuestionType.MultipleChoice:
                        newQuestion = gameCreator.CreateMultipleChoiceQuestion();
                        break;

                    case QuestionType.OpenEnded:
                        newQuestion = gameCreator.CreateOpenEndedQuestion();
                        break;

                    case QuestionType.TrueFalse:
                        newQuestion = gameCreator.CreateTrueFalseQuestion();
                        break;

                    default:
                        Console.WriteLine("Invalid option selected. Try again.");
                        continue;
                }

                if (newQuestion != null)
                {
                    gameCreator.AddQuestion(newQuestion);
                    currentQuestionCount++;
                    Console.WriteLine("Question added successfully.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please select a valid question type.");
            }
        }
    }

    private void RunGamePlayMode()
    {
        // Check if there are any questions to play with
        if (gameCreator.GetQuestions().Count == 0)
        {
            Console.WriteLine("\nNo questions available. Please create questions first.");
            return;
        }

        // Start the game with the available questions
        gamePlayer = new GamePlayer(gameCreator.GetQuestions());
        gamePlayer.StartGame();
    }
}


// Example usage:
public class Program
{
    public static void Main(string[] args)
    {
        Game game = new Game();
        game.Start();
    }
}

