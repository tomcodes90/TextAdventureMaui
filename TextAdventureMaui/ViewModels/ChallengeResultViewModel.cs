using System.Collections.ObjectModel;
using TextAdventureMaui.Models;
using TextAdventureMaui.Services;

namespace TextAdventureMaui.ViewModels;

public class ChallengeResultViewModel
{
    public bool Success { get; }
    public string ResultMessage => Success ? "You Won!" : "You Lost!";
    public ObservableCollection<string> EarnedItems { get; }
    public string Bonus { get; }
    public string AbilityName { get; }

    public ChallengeResultViewModel(ChallengeResult result, AbilityFactory abilityFactory)
    {
        Success = result.Success;

        EarnedItems = new ObservableCollection<string>(result.EarnedItems);

        Bonus = result.Bonus ?? "";

        if (result.UnlockedAbilityId.HasValue)
        {
            var ability = abilityFactory.CreateAbilityById(result.UnlockedAbilityId.Value);
            AbilityName = ability.Name;
        }
        else
        {
            AbilityName = "";
        }
    }
}