using TextAdventureMaui.Models;

namespace TextAdventureMaui.Services;

public class PlayerService
{
    public Player? CurrentPlayer { get; private set; }

    /// <summary>
    /// Set the current player (e.g. after character creation).
    /// </summary>
    public void SetPlayer(Player player)
    {
        CurrentPlayer = player;
    }

    /// <summary>
    /// Clear the current player (e.g. on quit or new game).
    /// </summary>
    public void ClearPlayer()
    {
        CurrentPlayer = null;
    }

    /// <summary>
    /// Save the current player to local storage (placeholder).
    /// </summary>
    public Task SaveAsync()
    {
        // TODO: Serialize CurrentPlayer to JSON and write to file
        throw new NotImplementedException("Save functionality not yet implemented.");
    }

    /// <summary>
    /// Load a saved player from local storage (placeholder).
    /// </summary>
    public Task<Player?> LoadAsync()
    {
        // TODO: Read from JSON and recreate Player
        throw new NotImplementedException("Load functionality not yet implemented.");
    }

    /// <summary>
    /// Check if a player save exists (placeholder).
    /// </summary>
    public bool HasSave()
    {
        // TODO: Return true if save file exists
        return false;
    }
}