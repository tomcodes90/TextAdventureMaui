using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TextAdventureMaui.Models.Dialogues;

namespace TextAdventureMaui.ViewModels;

public partial class DialoguePanelViewModel : ObservableObject
{
    private readonly Dictionary<int, DialogueNode> _nodes;
    private int _currentId;

    [ObservableProperty] private DialogueNode currentNode;

    public IRelayCommand NextCommand { get; }
    public IRelayCommand<DialogueChoice> ChooseCommand { get; }

    public event Action? DialogueFinished;

    public DialoguePanelViewModel(Dictionary<int, DialogueNode> nodes, int startNodeId)
    {
        _nodes = nodes;
        _currentId = startNodeId;
        CurrentNode = _nodes[_currentId];

        NextCommand = new RelayCommand(Next);
        ChooseCommand = new RelayCommand<DialogueChoice>(Choose);
    }

    private void Next()
    {
        // If node has no choices, try go to next id (sequential)
        int nextId = _currentId + 1;
        if (_nodes.ContainsKey(nextId))
        {
            _currentId = nextId;
            CurrentNode = _nodes[_currentId];
        }
        else
        {
            DialogueFinished?.Invoke();
        }
    }

    private void Choose(DialogueChoice choice)
    {
        if (_nodes.ContainsKey(choice.NextNodeId))
        {
            _currentId = choice.NextNodeId;
            CurrentNode = _nodes[_currentId];
        }
        else
        {
            DialogueFinished?.Invoke();
        }
    }
}