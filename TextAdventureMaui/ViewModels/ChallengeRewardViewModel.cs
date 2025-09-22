using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TextAdventureMaui.Models;
using TextAdventureMaui.Models.Missions;
using TextAdventureMaui.Services;

namespace TextAdventureMaui.ViewModels;

public partial class ChallengeRewardViewModel : ObservableObject
{
    private readonly Player _player;
    private readonly ChallengeReward _reward;
    private readonly ChallengeRewardService _rewardService = new();

    [ObservableProperty] private string rewardText;

    public ChallengeRewardViewModel(Player player, ChallengeReward reward)
    {
        _player = player;
        _reward = reward;

        RewardText = reward.NewAbilityUnlocked != null
            ? $"Hai sbloccato una nuova abilità: {reward.NewAbilityUnlocked}!"
            : "Scegli un potenziamento:";
    }

    [RelayCommand]
    private async Task ChooseUpgrade(string choice)
    {
        _rewardService.ApplyUpgrade(_player, choice);

        if (_reward.NewAbilityUnlocked != null)
            _rewardService.UnlockAbility(_player, _reward.NewAbilityUnlocked);

        await Shell.Current.Navigation.PopAsync(); // chiude la RewardPage e torna indietro
    }
}