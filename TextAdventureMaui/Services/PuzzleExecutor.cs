using TextAdventureMaui.Models;

namespace TextAdventureMaui.Services;

public class PuzzleExecutor : IChallengeExecutor
{
    private readonly PuzzleService _puzzleService;

    public PuzzleExecutor(PuzzleService puzzleService)
    {
        _puzzleService = puzzleService; // your PuzzleService StartPuzzleAsync expects settings. :contentReference[oaicite:10]{index=10}
    }

    public async Task<ChallengeResult> ExecuteAsync(Dictionary<string, object>? settings)
    {
        if (settings == null) throw new ArgumentException("Puzzle step missing settings.");
        // PuzzleService.StartPuzzleAsync returns Task<bool> (true=solved)
        var ok = await _puzzleService.StartPuzzleAsync(settings);
        return new ChallengeResult(ok, ok ? new List<string> { "puzzle_token" } : null);
    }
}