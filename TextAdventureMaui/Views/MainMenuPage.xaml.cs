using TextAdventureMaui.ViewModels;

namespace TextAdventureMaui.Views;

public partial class MainMenuPage : ContentPage
{
	public MainMenuPage()
	{
        InitializeComponent();
		BindingContext = new MainMenuViewModel();
    }
}