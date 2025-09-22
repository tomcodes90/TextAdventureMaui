using TextAdventureMaui.Models;
using TextAdventureMaui.Models.Missions;

namespace TextAdventureMaui.Services;

public class NavigationService
{
    private readonly MissionService _missionService;

    public List<Room> Rooms { get; }
    public Room CurrentRoom { get; private set; }
    public Player Player { get; }

    public NavigationService(Player player, List<Room> rooms, Room mainHall, MissionService missionService)
    {
        Player = player;
        Rooms = rooms;
        CurrentRoom = mainHall;
        _missionService = missionService;
    }

    /// <summary>
    /// Prosegue con la prossima stanza non ancora esplorata.
    /// </summary>
    public async Task ContinueStory()
    {
        var next = Rooms.FirstOrDefault(r => !r.IsExplored && r.Id != CurrentRoom.Id);
        if (next != null)
        {
            CurrentRoom = next;
            await _missionService.StartMission(CurrentRoom.Mission, Player);

            if (CurrentRoom.Mission.IsCompleted)
                CurrentRoom.MarkAsExplored();
        }
        else
        {
            Console.WriteLine("Hai completato tutte le stanze disponibili!");
        }
    }

    /// <summary>
    /// Permette di rigiocare una stanza già esplorata (grinding).
    /// </summary>
    public async Task ExploreRoom(int roomId)
    {
        var room = Rooms.FirstOrDefault(r => r.Id == roomId && r.IsExplored);
        if (room != null)
        {
            CurrentRoom = room;
            await _missionService.StartMission(CurrentRoom.Mission, Player);
        }
        else
        {
            Console.WriteLine("Questa stanza non è ancora disponibile per il grinding!");
        }
    }
}