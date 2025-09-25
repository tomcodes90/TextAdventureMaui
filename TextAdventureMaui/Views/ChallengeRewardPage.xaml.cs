using TextAdventureMaui.Models;
using TextAdventureMaui.Models.Missions;
using TextAdventureMaui.ViewModels;

namespace TextAdventureMaui.Views;

public partial class ChallengeRewardPage : ContentPage
{
    public ChallengeRewardPage(Player player, ChallengeReward reward)
    {
        InitializeComponent();
        BindingContext = new ChallengeRewardViewModel(player, reward);
    }
}