
namespace TextAdventureMaui.Models.Missions;

public class Room
{
    public int Id { get; }
    public string Name { get; }
    public string Description { get; }
    public string BackgroundImage { get; }
    public string MusicTrack { get; }
    public bool IsExplored { get; private set; }
    public Mission Mission { get; }
    public int? NextRoomId { get; }

    public Room(int id, string name, string description, string backgroundImage, string musicTrack, Mission mission, int? nextRoomId)
    {
        Id = id;
        Name = name;
        Description = description;
        BackgroundImage = backgroundImage;
        MusicTrack = musicTrack;
        Mission = mission;
        NextRoomId = nextRoomId;
    }

    public void Enter(Player player)
    {
        Console.WriteLine($"You enter {Name}.");
        Console.WriteLine($"Background: {BackgroundImage}, Music: {MusicTrack}");

        if (!IsExplored)
            Explore(player);
        else
            Console.WriteLine($"{Name} has already been explored.");
    }

    private void Explore(Player player)
    {
        Console.WriteLine(Description);
        Mission.Start(player);
        if (Mission.IsCompleted)
            IsExplored = true;
    }
}