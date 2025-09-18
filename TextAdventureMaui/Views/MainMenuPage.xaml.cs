using TextAdventureMaui.ViewModels;
using Microsoft.Maui.Controls;

namespace TextAdventureMaui.Views;

public partial class MainMenuPage : ContentPage
{
    public MainMenuPage(MainMenuViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm; // DI injects the ViewModel with audio service
    }

    protected override bool OnBackButtonPressed()
    {
        return true; // disables Android back button
    }
}
