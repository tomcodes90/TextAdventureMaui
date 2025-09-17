using System.Windows.Input;

namespace TextAdventureMaui.ViewModels;

public class MainMenuViewModel
{
    public ICommand StartCommand { get; }
    public ICommand SettingsCommand { get; }
    public ICommand ExitCommand { get; }

    public MainMenuViewModel()
    {
        StartCommand = new Command(OnStart);
        SettingsCommand = new Command(OnSettings);
        ExitCommand = new Command(OnExit);
    }

    private void OnStart()
    {
        App.Current.MainPage.DisplayAlert("Main Menu", "Starting the game...", "OK");
    }

    private void OnSettings()
    {
        App.Current.MainPage.DisplayAlert("Main Menu", "Opening settings...", "OK");
    }

    private void OnExit()
    {
        System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
    }
}