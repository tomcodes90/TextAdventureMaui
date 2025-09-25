using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TextAdventureMaui.Models;
using TextAdventureMaui.Models.Missions;
using TextAdventureMaui.Services;

namespace TextAdventureMaui.ViewModels;

public partial class ChallengeRewardViewModel : ObservableObject
{
    private readonly Player _player;
    private readonly AbilityFactory _abilityFactory;
    private readonly ChallengeRewardService _rewardService = new();

    public ChallengeReward Reward { get; }

    [ObservableProperty] private string rewardText;
    public ObservableCollection<string> Loot { get; } = new();

    public bool HasLoot => Loot.Any();

    public ChallengeRewardViewModel(Player player, ChallengeReward reward)
    {
        _player = player;
        Reward = reward;

        // ✅ niente path, la factory legge da Resources/Data/abilities.json
        _abilityFactory = new AbilityFactory();

        if (reward.NewAbilityUnlockedId.HasValue)
        {
            var ability = _abilityFactory.CreateAbilityById(reward.NewAbilityUnlockedId.Value);
            RewardText = $"Hai sbloccato una nuova abilità: {ability.Name}!";
        }
        else
        {
            RewardText = reward.PlayerWon
                ? "Scegli un potenziamento:"
                : "Hai perso la sfida.";
        }

        foreach (var item in reward.Loot)
        {
            Loot.Add(item);
        }
    }


    [RelayCommand]
    private async Task ChooseUpgrade(string choice)
    {
        if (!Reward.CanChooseUpgrade) return;

        _rewardService.ApplyUpgrade(_player, choice);

        if (Reward.NewAbilityUnlockedId.HasValue)
            _rewardService.UnlockAbility(_player, Reward.NewAbilityUnlockedId.Value);

        await Shell.Current.Navigation.PopAsync();
    }

}
            