
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
}