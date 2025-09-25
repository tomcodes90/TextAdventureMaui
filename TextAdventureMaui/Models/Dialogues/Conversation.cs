namespace TextAdventureMaui.Models.Dialogues;

public class Conversation
{
    public Dictionary<int, DialogueNode> Nodes { get; }
    public int StartNodeId { get; }

    public Conversation(Dictionary<int, DialogueNode> nodes, int startNodeId = 0)
    {
        Nodes = nodes;
        StartNodeId = startNodeId;
    }
}
