namespace TextAdventureMaui.Models.Dialogues;


    public class DialogueChoice
    {
        public string Text { get; }
        public int NextNodeId { get; }

        public DialogueChoice(string text, int nextNodeId)
        {
            Text = text;
            NextNodeId = nextNodeId;
        }
    }

