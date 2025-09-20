namespace TextAdventureMaui.Models.Dialogues
{
    public class DialogueLine
    {
        public string Speaker { get; }
        public string Text { get; }
        public string? Portrait { get; }

        public DialogueLine(string speaker, string text, string? portrait = null)
        {
            Speaker = speaker;
            Text = text;
            Portrait = portrait;
        }
    }
}