using System.Diagnostics;
using TextAdventureMaui.Models.Missions;

namespace TextAdventureMaui.Services;

public class DobbleService
{
    private readonly DobbleDeck _deck;
    private readonly Random _random = new();

    public DobbleService()
    {
        Debug.WriteLine("DobbleService constructed.");
        _deck = DobbleDeckGenerator.Generate(5, shuffle: true);
        if (!DobbleDeckGenerator.Validate(_deck, out var errors))
        {
            foreach (var e in errors) Debug.WriteLine(e);
            throw new Exception("Dobble deck generation failed!");
        }
        Debug.WriteLine($"✅ Deck generated with {_deck.Icons.Count} icons and {_deck.Cards.Count} cards.");
    }

    public Task<(List<string> Card1, List<string> Card2)> GetCardPairAsync()
    {
        var idx1 = _random.Next(_deck.Cards.Count);
        int idx2;
        do { idx2 = _random.Next(_deck.Cards.Count); } while (idx2 == idx1);

        return Task.FromResult((_deck.Cards[idx1], _deck.Cards[idx2]));
    }

    public Task<string> FindMatchAsync(List<string> card1, List<string> card2)
    {
        var match = card1.Intersect(card2).ToList();
        if (match.Count != 1)
            throw new Exception($"Deck error: cards don’t have exactly 1 match (found {match.Count})");

        return Task.FromResult(match[0]);
    }
}