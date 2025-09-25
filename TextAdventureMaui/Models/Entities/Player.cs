
using TextAdventureMaui.Models.Entities;
using TextAdventureMaui.Models.Items;

namespace TextAdventureMaui.Models;

public class Player(string? name, int maxHp, int attack) : Entity(name, maxHp, attack)
{
    public Inventory Inventory { get; } = new Inventory();
    public List<Ability> Abilities { get; } = new();

    public Ability? MatchAbility(List<string> inputSequence)
    {
        return Abilities.FirstOrDefault(ability =>
            ability.InputSequence.SequenceEqual(inputSequence));
    }
    
    public string Sprite
    {
        get
        {
            if (EquippedWeapon is null) return "knight_sword.png"; // fallback

            return EquippedWeapon.DisplayName switch
            {
                "Sword" => "knight_sword.png",
                "Hammer" => "knight_hammer.png",
                "Bow" => "knight_bow.png",
                _ => "knight_sword.png"
            };
        }
    }

}