using TextAdventureMaui.Models;
using TextAdventureMaui.Models.Entities;

namespace TextAdventureMaui.Services;

public class RewardService
{
    public void ApplyUpgrade(Player player, string choice)
    {
        switch (choice)
        {
            case "Damage":
                player.BaseAttack += 2;
                break;
            case "Health":
                player.MaxHp += 10;
                player.CurrentHp += 10;
                break;
        }
    }

    public void UnlockAbility(Player player, string abilityName)
    {
        // qui potresti avere un catalogo di abilità definite
        if (abilityName == "Whirlwind")
        {
            player.Abilities.Add(new Ability(
                "Whirlwind",
                new List<string> { "Up", "Down", "Up", "Action" },
                25,
                "Un attacco rotante devastante"
            ));
        }
    }
}
