using TextAdventureMaui.ViewModels;
using Microsoft.Maui.Controls;

namespace TextAdventureMaui.Views;

public partial class MainMenuPage : ContentPage
{
    private MainMenuViewModel ViewModel => BindingContext as MainMenuViewModel;

    public MainMenuPage()
    {
        InitializeComponent();
        BindingContext = new MainMenuViewModel();
    }

    protected override bool OnBackButtonPressed()
    {
        return true; // disable Android back button if desired
    }
}