using TextAdventureMaui.Models;
using TextAdventureMaui.Models.Items.Weapons;

namespace TextAdventureMaui.Services;

public class CharacterCreationService
{
    public Player CreatePlayer(string name, string weaponChoice)
    {
        // Fallback if name is empty
        if (string.IsNullOrWhiteSpace(name))
            name = "Nameless Hero";

        Weapon starterWeapon = weaponChoice switch
        {
            "Sword" => new Sword(),
            "Bow" => new Bow(),
            "Hammer" => new Hammer(),
            _ => new Sword()
        };

        var player = new Player(name, maxHp: 30, attack: 5)
        {
            EquippedWeapon = starterWeapon
        };

        // You can give default abilities here if needed
        return player;
    }
}