
using TextAdventureMaui.Models.Items;
using TextAdventureMaui.Models.Missions;

namespace TextAdventureMaui.Models;

public class Player(string? name, int maxHp, int attack) : Entity(name, maxHp, attack)
{

    public Inventory Inventory { get; } = new Inventory();
    public MissionType MissionFlag { get; set; }
    public string V1 { get; }
    public int V2 { get; }
}


