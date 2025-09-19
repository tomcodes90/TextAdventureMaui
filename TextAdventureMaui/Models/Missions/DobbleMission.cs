namespace TextAdventureMaui.Models.Missions
{
    public class DobbleMission : Mission
    {
        public List<DobbleChallenge> Challenges { get; }

        public DobbleMission(
            string name,
            string description,
            string backgroundImage,
            string musicTrack,
            List<DobbleChallenge> challenges
        ) : base(name, description, backgroundImage, musicTrack)
        {
            Challenges = challenges;
        }

        public override void Start(Player player)
        {
            Console.WriteLine($"Mission: {Name}");
            bool allCorrect = true;

            foreach (var challenge in Challenges)
            {
                Console.WriteLine("Find the common symbol!");
                Console.WriteLine($"Card1: {string.Join(", ", challenge.Card1.Symbols)}");
                Console.WriteLine($"Card2: {string.Join(", ", challenge.Card2.Symbols)}");

                string input = Console.ReadLine() ?? string.Empty;
                if (!input.Equals(challenge.CommonSymbol, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Wrong! The right answer was " + challenge.CommonSymbol);
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