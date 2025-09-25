using System.Text.Json;
using TextAdventureMaui.Models;

namespace TextAdventureMaui.Services;

using System.Diagnostics;

public class PuzzleService
{
    private readonly Dictionary<string, PuzzleQuestion> _puzzles;

    public PuzzleService(IEnumerable<PuzzleQuestion> puzzles)
    {
        _puzzles = puzzles.ToDictionary(p => p.Id, p => p);
    }

    public Task<bool> StartPuzzleAsync(Dictionary<string, object> settings)
    {
        if (!settings.TryGetValue("questionId", out var idObj) || idObj is not JsonElement jsonElement)
            throw new ArgumentException("Puzzle step missing questionId");

        string questionId = jsonElement.GetString()!;

        if (!_puzzles.TryGetValue(questionId, out var puzzle))
            throw new Exception($"Puzzle with id {questionId} not found");

        // 👉 TODO: Replace this with a proper UI page
        Debug.WriteLine($"Puzzle: {puzzle.Question}");
        foreach (var opt in puzzle.Options)
            Debug.WriteLine($" - {opt}");

        // For now, always return true (as if solved correctly)
        return Task.FromResult(true);
    }
}
