using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TextAdventureMaui.Models;
using TextAdventureMaui.Models.Ambient;
using TextAdventureMaui.Models.Dialogues;
using TextAdventureMaui.Models.Entities;
using TextAdventureMaui.Services;

namespace TextAdventureMaui.ViewModels;

public partial class MainHallViewModel : ObservableObject
{
    private readonly DialogueService _dialogueService;
    private readonly PlayerService _playerService;
    private List<Npc> _npcs;

    [ObservableProperty] private Player? currentPlayer;

    [ObservableProperty] private CharacterViewModel playerCharacter;
    [ObservableProperty] private List<CharacterViewModel> npcCharacters;

    [ObservableProperty] private DialoguePanelViewModel? maidyDialogueVm;
    [ObservableProperty] private DialoguePanelViewModel? chexDialogueVm;

    [ObservableProperty] private bool showPlayerPanel;
    [ObservableProperty] private bool showMaidyDialogue;
    [ObservableProperty] private bool showChexDialogue;
    [ObservableProperty] 
    private ObservableCollection<Door> doors;

    

    public bool ShowOverlay => ShowMaidyDialogue || ShowChexDialogue || ShowPlayerPanel;
    public event Action<string>? RequestClosePanel;

    partial void OnShowPlayerPanelChanged(bool value)
    {
        if (value)
        {
            if (ShowMaidyDialogue) { ShowMaidyDialogue = false; RequestClosePanel?.Invoke("Maidy"); }
            if (ShowChexDialogue)  { ShowChexDialogue  = false; RequestClosePanel?.Invoke("Chex"); }
        }
    }

    partial void OnShowMaidyDialogueChanged(bool value)
    {
        if (value)
        {
            if (ShowPlayerPanel)   { ShowPlayerPanel   = false; RequestClosePanel?.Invoke("Player"); }
            if (ShowChexDialogue)  { ShowChexDialogue  = false; RequestClosePanel?.Invoke("Chex"); }
        }
    }

    partial void OnShowChexDialogueChanged(bool value)
    {
        if (value)
        {
            if (ShowPlayerPanel)   { ShowPlayerPanel   = false; RequestClosePanel?.Invoke("Player"); }
            if (ShowMaidyDialogue) { ShowMaidyDialogue = false; RequestClosePanel?.Invoke("Maidy"); }
        }
    }


    public IRelayCommand OverlayTappedCommand { get; }

    public MainHallViewModel(DialogueService dialogueService, PlayerService playerService)
    {
        _dialogueService = dialogueService;
        _playerService = playerService;

        var player = _playerService.CurrentPlayer!;
        CurrentPlayer = player;

        PlayerCharacter = new CharacterViewModel(
            player.Name!,
            player.Sprite,
            TogglePlayerPanelAsync
        );

        _npcs = new List<Npc>
        {
            new Npc("Maidy", "npc_maid.png", new List<DialogueLine>
            {
                new DialogueLine("Maidy", "Welcome to the castle!"),
                new DialogueLine("Maidy", "Be careful with the cursed rooms...")
            }),
            new Npc("Chex", "npc_chef.png", new List<DialogueLine>
            {
                new DialogueLine("Chex", "Want to cook something?"),
                new DialogueLine("Chex", "Maybe later...")
            })
        };
        Doors = new ObservableCollection<Door>
        {
            new Door(1, "Hell's Kitchen", locked: false),
            new Door(2, "Screaming Tower", locked: true),
            new Door(3, "Pain Chambers", locked: true),
            new Door(4, "The Catacombs", locked: true)
        };


        NpcCharacters = new List<CharacterViewModel>
        {
            new CharacterViewModel("Maidy", "npc_maid.png", () => TalkToNpcAsync("Maidy")),
            new CharacterViewModel("Chex", "npc_chef.png", () => TalkToNpcAsync("Chex"))
        };

        OverlayTappedCommand = new RelayCommand(OnOverlayTapped);
    }

    private Task TogglePlayerPanelAsync()
    {
        ShowPlayerPanel = !ShowPlayerPanel;
        return Task.CompletedTask;
    }

    private void OnOverlayTapped()
    {
        if (ShowPlayerPanel)
            ShowPlayerPanel = false; // overlay closes only player panel
    }

    private Task TalkToNpcAsync(string npcName)
    {
        var npc = _npcs.FirstOrDefault(n => n.Name == npcName);
        if (npc == null) return Task.CompletedTask;

        var nodes = npc.Dialogue
            .Select((line, index) => new DialogueNode(index, line))
            .ToDictionary(n => n.Id, n => n);

        var vm = new DialoguePanelViewModel(nodes, 0);
        vm.DialogueFinished += () =>
        {
            if (npc.Name == "Maidy")
            {
                ShowMaidyDialogue = false;
                MaidyDialogueVm = null; // 🔹 clears
            }
            else if (npc.Name == "Chex")
            {
                ShowChexDialogue = false;
                ChexDialogueVm = null; // 🔹 clears
            }
        };

        if (npc.Name == "Maidy")
        {
            MaidyDialogueVm = vm; // 🔹 new instance every time
            ShowMaidyDialogue = true;
        }
        else if (npc.Name == "Chex")
        {
            ChexDialogueVm = vm;
            ShowChexDialogue = true;
        }

        return Task.CompletedTask;
    }
    
    [RelayCommand]
    private async Task EnterRoom(Door door)
    {
        if (door.IsLocked)
        {
            await App.Current.MainPage.DisplayAlert("Locked", $"The door to {door.Label} is locked.", "OK");
            return;
        }

        await Shell.Current.GoToAsync($"room?RoomId={door.RoomId}");
    }

}
