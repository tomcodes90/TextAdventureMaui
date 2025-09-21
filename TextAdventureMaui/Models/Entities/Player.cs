using TextAdventureMaui.Models.Entities;
using TextAdventureMaui.Models.Items;
using TextAdventureMaui.Models.Missions;


namespace TextAdventureMaui.Models;

public class Player(string? name, int maxHp, int attack) : Entity(name, maxHp, attack)
{
    public Inventory Inventory { get; } = new Inventory();
    public MissionType MissionFlag { get; set; }

    // Lista di abilità in stile Tekken
    public List<Ability> Abilities { get; } = new();

    /// <summary>
    /// Controlla se una sequenza di input corrisponde ad una abilità nota
    /// </summary>
    public Ability? MatchAbility(List<string> inputSequence)
    {
        foreach (var ability in Abilities)
        {
            if (ability.InputSequence.SequenceEqual(inputSequence))
                return ability;
        }
        return null; // Nessuna abilità trovata
    }
}