namespace TextAdventureMaui.Models.Missions;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string BackgroundImage { get; set; } = "";
    public string MusicTrack { get; set; } = "";
    public bool IsExplored { get; private set; }
    public MissionData Mission { get; set; } = default!;
    public int? NextRoomId { get; set; }

    public void MarkAsExplored() => IsExplored = true;
}