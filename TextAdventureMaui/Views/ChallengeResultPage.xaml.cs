using TextAdventureMaui.Models;
using TextAdventureMaui.Services;
using TextAdventureMaui.ViewModels;

namespace TextAdventureMaui.Views;

public partial class ChallengeResultPage : ContentPage
{
    public ChallengeResultPage(ChallengeResult result, AbilityFactory abilityFactory, Action onContinue)
    {
        InitializeComponent();
        BindingContext = new ChallengeResultViewModel(result, abilityFactory)
        {
            ContinueCommand = new Command(() => onContinue())
        };
    }
}