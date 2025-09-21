using System.Windows.Input;
using Plugin.Maui.Audio;
using TextAdventureMaui.Models;
using TextAdventureMaui.Models.Entities;
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

    private async void OnStart()
    {
        _player?.Stop();
        try
        {
            await Shell.Current.GoToAsync("///mainhall");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Navigation error: {ex}");
        }
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
