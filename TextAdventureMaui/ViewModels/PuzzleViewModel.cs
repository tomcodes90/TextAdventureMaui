using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TextAdventureMaui.Models;

namespace TextAdventureMaui.ViewModels;

public partial class PuzzleViewModel : ObservableObject
{
    private int currentQuestionIndex;
    private int wrongAnswers;

    public ObservableCollection<PuzzleQuestion> Questions { get; } = new();

    [ObservableProperty]
    private PuzzleQuestion currentQuestion;

    [ObservableProperty]
    private string feedbackMessage;

    public PuzzleViewModel()
    {
        // Example questions (replace with your own)
        Questions.Add(new PuzzleQuestion
        {
            Question = "What color is the castle door?",
            Answers = new() { "Red", "Blue", "Green", "Yellow" },
            CorrectIndex = 1
        });

        Questions.Add(new PuzzleQuestion
        {
            Question = "Who cooks in the kitchen?",
            Answers = new() { "Knight", "Old Cook", "Maid", "Cat" },
            CorrectIndex = 1
        });

        Questions.Add(new PuzzleQuestion
        {
            Question = "What shines at night?",
            Answers = new() { "Moon", "Sword", "Cat", "Fireplace" },
            CorrectIndex = 0
        });

        Questions.Add(new PuzzleQuestion
        {
            Question = "Which is the key to the library?",
            Answers = new() { "Golden", "Silver", "Rusty", "Diamond" },
            CorrectIndex = 2
        });

        Questions.Add(new PuzzleQuestion
        {
            Question = "Who guards the tower?",
            Answers = new() { "Dragon", "Knight", "Cook", "Karen" },
            CorrectIndex = 0
        });

        currentQuestionIndex = 0;
        wrongAnswers = 0;
        CurrentQuestion = Questions[currentQuestionIndex];
    }

    [RelayCommand]
    private void SelectAnswer(int selectedIndex)
    {
        if (selectedIndex == CurrentQuestion.CorrectIndex)
        {
            FeedbackMessage = "✅ Correct!";
        }
        else
        {
            FeedbackMessage = "❌ Wrong!";
            wrongAnswers++;
        }

        // Lose condition
        if (wrongAnswers > 1)
        {
            FeedbackMessage = "💀 You lost the mission!";
            return;
        }

        // Win condition
        if (currentQuestionIndex >= 4) // 5 questions total
        {
            FeedbackMessage = "🎉 You passed the mission!";
            return;
        }

        // Next question
        currentQuestionIndex++;
        CurrentQuestion = Questions[currentQuestionIndex];
    }
}
