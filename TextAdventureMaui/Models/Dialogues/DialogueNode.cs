namespace TextAdventureMaui.Models.Dialogues;

public class DialogueNode
{
    public int Id { get; }
    public DialogueLine Line { get; }
    public List<DialogueChoice> Choices { get; }

    public DialogueNode(int id, DialogueLine line)
    {
        Id = id;
        Line = line;
        Choices = new List<DialogueChoice>();
    }

    public bool HasChoices => Choices.Any();
}
