using TextAdventureMaui.Models;
using TextAdventureMaui.Factories;

namespace TextAdventureMaui.Services;

public class ChallengeRewardService
{
    private readonly AbilityFactory _abilityFactory;
    private readonly ItemFactory _itemFactory;

    public ChallengeRewardService(AbilityFactory abilityFactory, ItemFactory itemFactory)
    {
        _abilityFactory = abilityFactory;
        _itemFactory = itemFactory;
    }

    public void ApplyResult(Player player, ChallengeResult result)
    {
        if (!result.Success)
            return;

        // 🔹 Apply stat bonus
        if (!string.IsNullOrEmpty(result.Bonus))
        {
            switch (result.Bonus)
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

        // 🔹 Unlock ability
        if (result.UnlockedAbilityId.HasValue)
        {
            var ability = _abilityFactory.CreateAbilityById(result.UnlockedAbilityId.Value);
            if (!player.Abilities.Any(a => a.Name == ability.Name))
            {
                player.Abilities.Add(ability);
                Console.WriteLine($"Nuova abilità sbloccata: {ability.Name}");
            }
        }

        // 🔹 Add earned items
        foreach (var itemName in result.EarnedItems)
        {
            var item = _itemFactory.CreateItem(itemName);
            player.Inventory.AddItem(item);
            Console.WriteLine($"Item ottenuto: {item.Name}");
        }
    }
}