namespace TextAdventureMaui.Models.Missions
{
    public class PuzzleMission : Mission
    {
        public List<Question> Questions { get; }

        public PuzzleMission(
            string name,
            string description,
            string backgroundImage,
            string musicTrack,
            List<Question> questions
        ) : base(name, description, backgroundImage, musicTrack)
        {
            Questions = questions;
        }

        public override void Start(Player player)
        {
            Console.WriteLine($"Mission: {Name}");
            bool allCorrect = true;

            foreach (var q in Questions)
            {
                Console.WriteLine(q.Text);
                for (int i = 0; i < q.Answers.Count; i++)
                    Console.WriteLine($"{i + 1}. {q.Answers[i]}");

                string input = Console.ReadLine() ?? string.Empty;
                if (!q.IsCorrect(input))
                {
                    Console.WriteLine("Wrong answer!");
                    allCorrect = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Correct!");
                }
            }

            if (allCorrect)
                IsCompleted = true;
        }
    }
}