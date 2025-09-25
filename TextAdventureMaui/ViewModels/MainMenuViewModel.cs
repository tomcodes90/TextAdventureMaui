using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Plugin.Maui.Audio;
using TextAdventureMaui.Models;
using TextAdventureMaui.Services;
using TextAdventureMaui.Views;

namespace TextAdventureMaui.ViewModels;

public class MainMenuViewModel : INotifyPropertyChanged
{
    private readonly IAudioManager _audioManager;
    private readonly CharacterCreationService _characterService;
    private readonly AbilityLoader _abilityLoader;
    private readonly PlayerService _playerService;
    private IAudioPlayer? _musicPlayer;

    public event PropertyChangedEventHandler? PropertyChanged;

    // ------------------------------------------------------------
    // Properties (UI state & validation)
    // ------------------------------------------------------------
    private bool _showMainMenu = true;
    private bool _showCharacterCreation;
    private string _playerName = string.Empty;
    private string _chosenWeapon = string.Empty;
    private string _validationMessage = string.Empty;
    private bool _hasValidationError;
    private bool _swordSelected, _bowSelected, _hammerSelected;

    public bool ShowMainMenu
    {
        get => _showMainMenu;
        set => SetProperty(ref _showMainMenu, value);
    }

    public bool ShowCharacterCreation
    {
        get => _showCharacterCreation;
        set => SetProperty(ref _showCharacterCreation, value);
    }

    public string PlayerName
    {
        get => _playerName;
        set => SetProperty(ref _playerName, value);
    }

    public string ChosenWeapon
    {
        get => _chosenWeapon;
        set => SetProperty(ref _chosenWeapon, value);
    }

    public string ValidationMessage
    {
        get => _validationMessage;
        set => SetProperty(ref _validationMessage, value);
    }

    public bool HasValidationError
    {
        get => _hasValidationError;
        set => SetProperty(ref _hasValidationError, value);
    }

    public bool SwordSelected
    {
        get => _swordSelected;
        set => SetProperty(ref _swordSelected, value);
    }

    public bool BowSelected
    {
        get => _bowSelected;
        set => SetProperty(ref _bowSelected, value);
    }

    public bool HammerSelected
    {
        get => _hammerSelected;
        set => SetProperty(ref _hammerSelected, value);
    }

    public Player? Player { get; private set; }

    // ------------------------------------------------------------
    // Commands
    // ------------------------------------------------------------
    public ICommand StartCommand { get; }
    public ICommand SettingsCommand { get; }
    public ICommand ExitCommand { get; }
    public ICommand ChooseWeaponCommand { get; }
    public ICommand ConfirmCommand { get; }

    // ------------------------------------------------------------
    // Constructor
    // ------------------------------------------------------------
    public MainMenuViewModel(
        IAudioManager audioManager,
        CharacterCreationService characterService,
        AbilityLoader abilityLoader,
        PlayerService playerService)
    {
        _audioManager = audioManager;
        _characterService = characterService;
        _abilityLoader = abilityLoader;
        _playerService = playerService;

        StartCommand = new Command(OnStart);
        SettingsCommand = new Command(OnSettings);
        ExitCommand = new Command(OnExit);
        ChooseWeaponCommand = new Command<string>(OnChooseWeapon);
        ConfirmCommand = new Command(async () => await OnConfirmAsync());

        PlayBackgroundMusic();
    }

    // ------------------------------------------------------------
    // Methods
    // ------------------------------------------------------------
    private async void PlayBackgroundMusic()
    {
        try
        {
            var fileStream = await FileSystem.OpenAppPackageFileAsync("backgroundmusic.mp3");
            _musicPlayer = _audioManager.CreatePlayer(fileStream);
            _musicPlayer.Loop = true;
            _musicPlayer.Play();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to play music: {ex.Message}");
        }
    }

    private void OnStart()
    {
        _musicPlayer?.Stop();
        ShowMainMenu = false;
        ShowCharacterCreation = true;
    }

    private void OnChooseWeapon(string weapon)
    {
        ChosenWeapon = weapon;
        SwordSelected = weapon == "Sword";
        BowSelected = weapon == "Bow";
        HammerSelected = weapon == "Hammer";
    }

    private async Task OnConfirmAsync()
    {
        if (!ValidateInputs())
            return;

        // Create player
        Player = _characterService.CreatePlayer(PlayerName, ChosenWeapon);

        // Assign abilities
        var abilities = await _abilityLoader.LoadAbilitiesAsync();
        var starterAbilities = _abilityLoader.GetStarterAbilities(ChosenWeapon);
        foreach (var ability in starterAbilities)
            Player.Abilities.Add(ability);

        // Save player globally
        _playerService.SetPlayer(Player);

        // Navigate to Main Hall
        await Shell.Current.GoToAsync("/mainhall");
    }

    private bool ValidateInputs()
    {
        if (string.IsNullOrWhiteSpace(PlayerName))
        {
            ShowValidation("Name cannot be empty.");
            return false;
        }

        if (PlayerName.Length > 15 || !PlayerName.All(char.IsLetter))
        {
            ShowValidation("Name must be letters only and max 15 characters.");
            return false;
        }

        if (string.IsNullOrEmpty(ChosenWeapon))
        {
            ShowValidation("Please select a weapon.");
            return false;
        }

        HasValidationError = false;
        ValidationMessage = string.Empty;
        return true;
    }

    private void ShowValidation(string message)
    {
        ValidationMessage = message;
        HasValidationError = true;
    }

    private void OnSettings()
    {
        Shell.Current.GoToAsync(nameof(SettingsPage));
    }

    private void OnExit()
    {
        _musicPlayer?.Stop();
        System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
    }

    // ------------------------------------------------------------
    // PropertyChanged helper
    // ------------------------------------------------------------
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return false;

        backingStore = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
