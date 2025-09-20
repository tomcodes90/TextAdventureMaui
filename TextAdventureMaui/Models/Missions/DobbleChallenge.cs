namespace TextAdventureMaui.Models.Missions
{
    public class DobbleChallenge
    {
        public Card Card1 { get; }
        public Card Card2 { get; }
        public string CommonSymbol { get; }

        public DobbleChallenge(Card card1, Card card2)
        {
            Card1 = card1;
            Card2 = card2;

            CommonSymbol = card1.Symbols.Intersect(card2.Symbols).First();
        }
    }
}