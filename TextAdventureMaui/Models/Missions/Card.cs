namespace TextAdventureMaui.Models.Missions;

public class Card
{
    public List<string> Symbols { get; }

    public Card(List<string> symbols)
    {
        Symbols = symbols;
    }
}