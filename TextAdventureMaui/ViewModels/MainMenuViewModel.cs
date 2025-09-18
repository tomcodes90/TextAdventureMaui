using System.Windows.Input;
using Plugin.Maui.Audio;
using TextAdventureMaui.Models;
using TextAdventureMaui.Views;

namespace TextAdventureMaui.ViewModels;

public class MainMenuViewModel
{
    private readonly IAudioManager _audioManager;
    private IAudioPlayer? _player;

    public ICommand StartCommand { get; }
    public ICommand SettingsCommand { get; }
    public ICommand ExitCommand { get; }

    public MainMenuViewModel(IAudioManager audioManager)
    {
        _audioManager = audioManager;

        StartCommand = new Command(OnStart);
        SettingsCommand = new Command(OnSettings);
        ExitCommand = new Command(OnExit);

        PlayBackgroundMusic();
    }

    private async void PlayBackgroundMusic()
    {
        try
        {
            var fileStream = await FileSystem.OpenAppPackageFileAsync("backgroundmusic.mp3");
            _player = _audioManager.CreatePlayer(fileStream);
            _player.Loop = true;
            _player.Play();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to play music: {ex.Message}");
        }
    }

    private async Task StartTestFightAsync()
    {
        var player = new Player("Hero", 100, 2);
        var enemy = new Enemy("Dishwasher", 80, 2);

        var vm = new BattleViewModel(player, enemy);
        var page = new BattlePage(vm);                 // uses the ctor above

        // From a ViewModel use Shell.Current
        await Shell.Current.Navigation.PushAsync(page);
    }

    private async void OnStart()
    {
        _player?.Stop();
        await StartTestFightAsync();
    }

    private void OnSettings()
    {
        Shell.Current.GoToAsync(nameof(SettingsPage));
    }

    private void OnExit()
    {
        _player?.Stop();
        System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
    }
}
