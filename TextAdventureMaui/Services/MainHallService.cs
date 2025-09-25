using System.Collections.ObjectModel;
using TextAdventureMaui.Models.Ambient;
using TextAdventureMaui.Models.Dialogues;
using TextAdventureMaui.Models.Entities;

namespace TextAdventureMaui.Services;

public class MainHallService
{
    public ObservableCollection<Door> Doors { get; }
    public ObservableCollection<Npc> Npcs { get; }

    public MainHallService()
    {
        Doors = new ObservableCollection<Door>
        {
            new Door(1, "Hell's Kitchen",  locked: false, image: "hells_kitchen_door.png"),
            new Door(2, "Screaming Tower", locked: true,  image: "screaming_tower_door.png"),
            new Door(3, "Pain Chambers",   locked: true,  image: "pain_chambers_door.png"),
            new Door(4, "The Catacombs",   locked: true,  image: "catacombs_door.png"),
        };

        Npcs = new ObservableCollection<Npc>
        {
            new Npc("Maidy", "npc_maid.png", new List<DialogueLine>
            {
                new DialogueLine("Maidy", "Welcome to the castle! Do you like it here?"),
                new DialogueLine("Maidy", "Mind the doors — some lead to cursed places...")
            }),
            new Npc("Chex", "npc_chef.png", new List<DialogueLine>
            {
                new DialogueLine("Chex", "Hungry? I can whip up something… questionable."),
                new DialogueLine("Chex", "The kitchen has… a personality.")
            })
        };
    }

    public void UnlockDoor(int roomId)
    {
        var door = Doors.FirstOrDefault(d => d.RoomId == roomId);
        door?.Unlock();
    }
}
