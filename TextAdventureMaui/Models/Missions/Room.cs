

namespace TextAdventureMaui.Models.Missions;


public class Room
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }

    // Challenge in this room (battle, puzzle, mini-game, etc.)
    public required IMission Mission { get; set; }

    // Clues found in the room
    public List<Clue> Clues { get; set; } = new();

    // Puzzle to unlock the next room (optional)
    public Puzzle? Puzzle { get; set; }

    // State
    public bool MissionCompleted { get; set; } = false;
    public bool PuzzleSolved { get; set; } = false;

    // Navigation
    public int NextRoomId { get; set; }
}

