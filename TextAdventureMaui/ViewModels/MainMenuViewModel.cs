using System.Windows.Input;
using Plugin.Maui.Audio;
using TextAdventureMaui.Models;
using TextAdventureMaui.Models.Entities;
using TextAdventureMaui.Models.Items.Weapons;
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
            // 🔹 Usa questa riga se vuoi navigare al MainHall
            // await Shell.Current.GoToAsync("///mainhall");

            // 🔹 Per ora testiamo direttamente la battle
            await StartTestBattleAsync();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Navigation error: {ex}");
        }
    }

    private async Task StartTestBattleAsync()
    {
        var player = new Player("Hero", 10, 1);
        player.Abilities.Add(new Ability(
            "Slash",
            new List<string> { "Left", "Right", "Action" },
            3,
            "Colpo rapido"
        ));
        player.Abilities.Add(new Ability(
            "Whirlwind",
            new List<string> { "Up", "Down", "Up", "Action" },
            5,
            "Attacco rotante"
        ));

        // 🔹 Assegna arma direttamente con la setter
        player.EquippedWeapon = new Sword();

        var enemy = new Enemy("Goblin", 6, 2);
        enemy.EquippedWeapon = new Sword();

        var vm = new BattleViewModel(player, enemy);
        var page = new BattlePage(vm);

        await Shell.Current.Navigation.PushAsync(page);
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
