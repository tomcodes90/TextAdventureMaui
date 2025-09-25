namespace TextAdventureMaui.Models.Missions;

public class MissionData
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string BackgroundImage { get; set; } = "";
    public string MusicTrack { get; set; } = "";

    public List<MissionStep> Steps { get; set; } = new();
    public Reward? Reward { get; set; }
}
