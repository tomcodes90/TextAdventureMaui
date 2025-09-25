using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TextAdventureMaui.Models.Dialogues;
using TextAdventureMaui.Services;

namespace TextAdventureMaui.ViewModels;

public partial class ConversationPanelViewModel : ObservableObject
{
    private readonly ConversationService _conversationService;

    [ObservableProperty] private string currentSpeaker = string.Empty;
    [ObservableProperty] private string currentLine = string.Empty;
    [ObservableProperty] private ObservableCollection<DialogueChoice> currentChoices = new();
    [ObservableProperty] private bool hasChoices;
    [ObservableProperty] private bool showContinue;
    [ObservableProperty] private bool showConversation;

    public IRelayCommand<DialogueChoice> ChooseCommand { get; }
    public IRelayCommand ContinueCommand { get; }

    public ConversationPanelViewModel(ConversationService service)
    {
        _conversationService = service;

        ChooseCommand = new RelayCommand<DialogueChoice>(OnChoiceSelected);
        ContinueCommand = new RelayCommand(OnContinue);

        _conversationService.LineSpoken += OnLineSpoken;
        _conversationService.ChoicesPresented += OnChoicesPresented;
        _conversationService.ConversationEnded += OnConversationEnded;
    }

    public void StartConversation(Conversation conversation)
    {
        Reset();
        ShowConversation = true;
        _conversationService.StartConversation(conversation);
    }

    private void OnLineSpoken(DialogueLine line)
    {
        CurrentSpeaker = line.Speaker;
        CurrentLine = line.Text;
        HasChoices = false;
        ShowContinue = true;
        CurrentChoices.Clear();
    }

    private void OnChoicesPresented(List<DialogueChoice> choices)
    {
        CurrentChoices = new ObservableCollection<DialogueChoice>(choices);
        HasChoices = true;
        ShowContinue = false;
    }

    private void OnChoiceSelected(DialogueChoice choice)
    {
        var idx = CurrentChoices.IndexOf(choice);
        if (idx >= 0) _conversationService.Choose(idx);
    }

    private void OnContinue() => _conversationService.Continue();

    private void OnConversationEnded() => Reset();

    private void Reset()
    {
        ShowConversation = false;
        CurrentLine = string.Empty;
        CurrentSpeaker = string.Empty;
        CurrentChoices.Clear();
        HasChoices = false;
        ShowContinue = false;
    }
}
