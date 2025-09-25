using TextAdventureMaui.Models.Dialogues;

namespace TextAdventureMaui.Services;

public class ConversationService
{
    private Conversation? _conversation;
    private int _currentNodeId;

    public event Action<DialogueLine>? LineSpoken;
    public event Action<List<DialogueChoice>>? ChoicesPresented;
    public event Action? ConversationEnded;

    public void StartConversation(Conversation conversation)
    {
        _conversation = conversation;
        _currentNodeId = conversation.StartNodeId;
        PlayCurrentNode();
    }

    private void PlayCurrentNode()
    {
        if (_conversation == null) return;

        if (_conversation.Nodes.TryGetValue(_currentNodeId, out var node))
        {
            LineSpoken?.Invoke(node.Line);

            if (node.HasChoices)
                ChoicesPresented?.Invoke(node.Choices);
            else
                _currentNodeId++; // default to next node id
        }
        else
        {
            EndConversation();
        }
    }

    public void Choose(int choiceIndex)
    {
        if (_conversation == null) return;
        if (!_conversation.Nodes.TryGetValue(_currentNodeId, out var node)) return;
        if (choiceIndex < 0 || choiceIndex >= node.Choices.Count) return;

        _currentNodeId = node.Choices[choiceIndex].NextNodeId;
        PlayCurrentNode();
    }

    public void Continue()
    {
        if (_conversation == null) return;
        if (!_conversation.Nodes.TryGetValue(_currentNodeId, out var _)) return;

        PlayCurrentNode();
    }

    private void EndConversation()
    {
        _conversation = null;
        ConversationEnded?.Invoke();
    }
}
