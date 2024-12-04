classDiagram
    class Question {
        - string Text
        - QuestionType Type
        + Question(string text, QuestionType type)
        + abstract bool CheckAnswer(string answer)
    }

    class MultipleChoiceQuestion {
        - List~string~ Options
        - int CorrectAnswerIndex
        + MultipleChoiceQuestion(string text, List~string~ options, int correctAnswerIndex)
        + bool CheckAnswer(string answer)
    }

    class OpenEndedQuestion {
        - List~string~ AcceptableAnswers
        + OpenEndedQuestion(string text, List~string~ acceptableAnswers)
        + OpenEndedQuestion(string text, string acceptableAnswer)
        + bool CheckAnswer(string answer)
    }

    class TrueFalseQuestion {
        - bool CorrectAnswer
        + TrueFalseQuestion(string text, bool correctAnswer)
        + bool CheckAnswer(string answer)
    }

    class QuestionType {
        <<Enumeration>>
        MultipleChoice=1
        OpenEnded=2
        TrueFalse=3
    }

    Question <|-- MultipleChoiceQuestion
    Question <|-- OpenEndedQuestion
    TrueFalseQuestion --|> Question
    QuestionType -- Question

    note for Question "Represents a generic question with attributes and an abstract method to check answers."
    note for MultipleChoiceQuestion "Represents a question with multiple choice options."
    note for OpenEndedQuestion "Represents a question with acceptable open-ended answers."
    note for TrueFalseQuestion "Represents a question with a true/false answer."
    note for QuestionType "Enumeration defining possible question types."
    note "'+' Indicates a public attribute or method.<br> '-' Indicates a private attribute. <br><|-- Indicates inheritance, where one class derives from another."
