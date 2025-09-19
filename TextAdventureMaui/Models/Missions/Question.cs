
namespace TextAdventureMaui.Models.Missions
{
    public class Question
    {
        public string Text { get; }
        public List<string> Answers { get; }
        public string CorrectAnswer { get; }

        public Question(string text, List<string> answers, string correctAnswer)
        {
            Text = text;
            Answers = answers;
            CorrectAnswer = correctAnswer;
        }

        public bool IsCorrect(string answer)
        {
            return answer.Equals(CorrectAnswer, StringComparison.OrdinalIgnoreCase);
        }
    }
}
