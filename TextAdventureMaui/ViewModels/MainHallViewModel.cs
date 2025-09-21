using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TextAdventureMaui.Models.Ambient;
using TextAdventureMaui.Models.Dialogues;
using TextAdventureMaui.Models.Entities;
using TextAdventureMaui.Services;

namespace TextAdventureMaui.ViewModels;

public partial class MainHallViewModel : ObservableObject
{
    private readonly DialogueService dialogueService;

    [ObservableProperty] private List<Door> doors;
    [ObservableProperty] private List<Npc> npcs;

    public MainHallViewModel(DialogueService dialogueService)
    {
        this.dialogueService = dialogueService;

        // inizializza 4 porte
        Doors = new List<Door>
        {
            new Door(1, "Hell's Kitchen", locked: false),
            new Door(2, "Screaming Tower", locked: true),
            new Door(3, "Pain Chambers", locked: true),
            new Door(4, "The Catacombs", locked: true)
        };

        // inizializza NPCs
        Npcs = new List<Npc>
        {
            new Npc("Nonna", "nonna.png", new List<DialogueLine>
            {
                new DialogueLine("Nonna", "Benvenuto nel castello!"),
                new DialogueLine("Nonna", "Stai attento alle stanze maledette...")
            }),
            new Npc("Mercante", "merchant.png", new List<DialogueLine>
            {
                new DialogueLine("Mercante", "Vuoi comprare qualcosa?"),
                new DialogueLine("Mercante", "Forse più tardi...")
            })
        };
    }

    [RelayCommand]
    private async Task EnterRoom(Door door)
    {
        if (door.IsLocked)
        {
            await App.Current.MainPage.DisplayAlert("Locked", $"La porta {door.Label} è chiusa.", "OK");
            return;
        }

        await Shell.Current.GoToAsync($"room?RoomId={door.RoomId}");
    }

    [RelayCommand]
    private void TalkToNpc(Npc npc)
    {
        dialogueService.Play(
            npc.Dialogue.ToDictionary(
                d => npc.Dialogue.IndexOf(d),
                d => new DialogueNode(npc.Dialogue.IndexOf(d), d)
            ),
            0
        );
    }
}
