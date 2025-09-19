using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureMaui.Models.Dialogues
{
    public class DialogueLine
    {
        public string Speaker { get; }
        public string Text { get; }

        public DialogueLine(string speaker, string text)
        {
            Speaker = speaker;
            Text = text;
        }
    }
}
