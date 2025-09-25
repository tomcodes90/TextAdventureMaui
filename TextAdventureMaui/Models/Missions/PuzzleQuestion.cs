namespace TextAdventureMaui.Models;

public class PuzzleQuestion
{
    public string Id { get; set; } = "";
    public string Question { get; set; } = "";
    public List<string> Options { get; set; } = new();
    public string Answer { get; set; } = "";
}
