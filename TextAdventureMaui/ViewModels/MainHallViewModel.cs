using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input; // <- make sure this is present
using System.Collections.ObjectModel;
using TextAdventureMaui.Models;
using TextAdventureMaui.Models.Ambient;
using TextAdventureMaui.Models.Dialogues;
using TextAdventureMaui.Models.Entities;
using TextAdventureMaui.Services;

namespace TextAdventureMaui.ViewModels;

public partial class MainHallViewModel : ObservableObject
{
    private readonly PlayerService _playerService;
    private readonly MainHallService _hallService;

    [ObservableProperty] private Player? currentPlayer;
    [ObservableProperty] private CharacterViewModel playerCharacter;
    [ObservableProperty] private List<CharacterViewModel> npcCharacters;

    [ObservableProperty] private ConversationPanelViewModel? conversationVm;
    [ObservableProperty] private bool showPlayerPanel;

    public bool ShowOverlay => ShowPlayerPanel || (ConversationVm?.ShowConversation ?? false);

    public ObservableCollection<Door> Doors => _hallService.Doors;
    public ObservableCollection<Npc> Npcs => _hallService.Npcs;

    public IAsyncRelayCommand<Door> EnterRoomCommand { get; }      // async
    public IAsyncRelayCommand<string> TalkToNpcCommand { get; }    // async
    public IRelayCommand OverlayTappedCommand { get; }
    public IAsyncRelayCommand TogglePlayerPanelCommand { get; }    // async

    public MainHallViewModel(PlayerService playerService,
                             MainHallService hallService,
                             ConversationService conversationService)
    {
        _playerService = playerService;
        _hallService = hallService;

        CurrentPlayer = _playerService.CurrentPlayer!;
        PlayerCharacter = new CharacterViewModel(
            CurrentPlayer.Name!,
            CurrentPlayer.Sprite,
            TogglePlayerPanelAsync // Func<Task>
        );

        npcCharacters = hallService.Npcs
            .Select(n => new CharacterViewModel(
                n.Name,
                n.Portrait,
                () => TalkToNpc(n.Name) // Func<Task>
            ))
            .ToList();

        ConversationVm = new ConversationPanelViewModel(conversationService);

        EnterRoomCommand = new AsyncRelayCommand<Door>(EnterRoom);
        TalkToNpcCommand = new AsyncRelayCommand<string>(TalkToNpc);
        OverlayTappedCommand = new RelayCommand(OnOverlayTapped);
        TogglePlayerPanelCommand = new AsyncRelayCommand(TogglePlayerPanelAsync);
    }

    private async Task EnterRoom(Door door)
    {
        if (door.IsLocked)
        {
            await App.Current.MainPage.DisplayAlert("Locked",
                $"The door to {door.Label} is locked.", "OK");
            return;
        }
        await Shell.Current.GoToAsync($"room?RoomId={door.RoomId}");
    }

    private Task TogglePlayerPanelAsync()
    {
        if (!ShowPlayerPanel && ConversationVm != null)
            ConversationVm.ShowConversation = false;

        ShowPlayerPanel = !ShowPlayerPanel;
        OnPropertyChanged(nameof(ShowOverlay));
        return Task.CompletedTask;
    }

    // NOTE: Keep this returning Task (no async needed since we don't await)
    private Task TalkToNpc(string npcName)
    {
        ShowPlayerPanel = false;
        OnPropertyChanged(nameof(ShowOverlay));

        var npc = _hallService.Npcs.FirstOrDefault(n => n.Name == npcName);
        if (npc == null) return Task.CompletedTask;

        var nodes = npc.Dialogue
            .Select((line, index) => new DialogueNode(index, line))
            .ToDictionary(n => n.Id, n => n);

        var convo = new Conversation(nodes, 0);
        ConversationVm?.StartConversation(convo);

        OnPropertyChanged(nameof(ShowOverlay));
        return Task.CompletedTask;
    }

    private void OnOverlayTapped()
    {
        var changed = false;

        if (ShowPlayerPanel)
        {
            ShowPlayerPanel = false;
            changed = true;
        }
        if (ConversationVm != null && ConversationVm.ShowConversation)
        {
            ConversationVm.ShowConversation = false;
            changed = true;
        }
        if (changed) OnPropertyChanged(nameof(ShowOverlay));
    }
}
