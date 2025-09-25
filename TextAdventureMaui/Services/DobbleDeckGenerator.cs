using System.Diagnostics;
using TextAdventureMaui.Models.Missions;

namespace TextAdventureMaui.Services;

public static class DobbleDeckGenerator
{
    private static readonly Random _rng = new();

    public static DobbleDeck Generate(int order = 5, bool shuffle = true)
    {
        int symbolsCount = order * order + order + 1;
        int cardSize = order + 1;

        var icons = Enumerable.Range(1, symbolsCount)
                              .Select(i => $"dobble_{i}.png")
                              .ToList();

        var cards = new List<List<string>>();

        // First card
        var firstCard = new List<string>();
        for (int k = 0; k < cardSize; k++)
            firstCard.Add(icons[k]);
        cards.Add(firstCard);

        // n cards: each starts with symbol 0
        for (int i = 0; i < order; i++)
        {
            var card = new List<string> { icons[0] };
            for (int j = 0; j < order; j++)
                card.Add(icons[order + 1 + order * i + j]);
            cards.Add(card);
        }

        // n*n cards
        for (int i = 0; i < order; i++)
        {
            for (int j = 0; j < order; j++)
            {
                var card = new List<string> { icons[i + 1] };
                for (int k = 0; k < order; k++)
                {
                    int idx = order + 1 + order * k + ((i * k + j) % order);
                    card.Add(icons[idx]);
                }
                cards.Add(card);
            }
        }

        if (shuffle)
        {
            // Shuffle symbols within each card
            foreach (var card in cards)
                Shuffle(card);

            // Shuffle the deck itself
            Shuffle(cards);
        }

        return new DobbleDeck { Icons = icons, Cards = cards };
    }

    public static bool Validate(DobbleDeck deck, out List<string> errors)
    {
        errors = new List<string>();

        for (int i = 0; i < deck.Cards.Count; i++)
        {
            for (int j = i + 1; j < deck.Cards.Count; j++)
            {
                var common = deck.Cards[i].Intersect(deck.Cards[j]).ToList();
                if (common.Count != 1)
                {
                    errors.Add($"Card {i + 1} & {j + 1} share {common.Count} symbols: {string.Join(", ", common)}");
                }
            }
        }

        return errors.Count == 0;
    }

    private static void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = _rng.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}
