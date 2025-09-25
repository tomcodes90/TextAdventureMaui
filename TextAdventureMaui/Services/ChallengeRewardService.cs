using TextAdventureMaui.Models;
using TextAdventureMaui.Services;

namespace TextAdventureMaui.Services;

public class ChallengeRewardService
{
    private readonly AbilityFactory _abilityFactory = new();

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

    public void UnlockAbility(Player player, int abilityId)
    {
        var ability = _abilityFactory.CreateAbilityById(abilityId);

        if (!player.Abilities.Any(a => a.Name == ability.Name))
        {
            player.Abilities.Add(ability);
            Console.WriteLine($"Nuova abilità sbloccata: {ability.Name}");
        }
    }
}