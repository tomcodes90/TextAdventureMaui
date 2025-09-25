using TextAdventureMaui.Models;

namespace TextAdventureMaui.Services;

public interface IChallengeExecutor
{
    /// <summary>
    /// Execute the challenge described by settings and return the ChallengeResult.
    /// </summary>
    Task<ChallengeResult> ExecuteAsync(Dictionary<string, object>? settings);
}


