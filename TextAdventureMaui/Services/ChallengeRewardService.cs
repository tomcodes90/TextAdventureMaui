using TextAdventureMaui.Models;
using TextAdventureMaui.Models.Entities;

namespace TextAdventureMaui.Services;

public class ChallengeRewardService
{
    public void ApplyUpgrade(Player player, string choice)
    {
        switch (choice)
        {
            case "Damage":
                player.BaseAttack += 1;
                Console.WriteLine("Upgrade: +Damage");
                break;

            case "Health":
                player.MaxHp += 2;
                player.CurrentHp += 2;
                Console.WriteLine("Upgrade: +Health");
                break;
        }
    }

    public void UnlockAbility(Player player, string abilityName)
    {
        // Qui puoi avere un "catalogo" di abilità predefinite
        if (abilityName == "Whirlwind")
        {
            player.Abilities.Add(new Ability(
                "Whirlwind",
                new List<string> { "Up", "Down", "Up", "Action" },
                5,
                "Un attacco rotante devastante"
            ));
        }
    }
}