
using TextAdventureMaui.Models.Dialogues;

namespace TextAdventureMaui.Models.Missions
{
    public class Npc
    {
        public string Name { get; }
        public string Portrait { get; }
        public List<DialogueLine> Dialogue { get; }

        public Npc(string name, string portrait, List<DialogueLine> dialogue)
        {
            Name = name;
            Portrait = portrait;
            Dialogue = dialogue;
        }
    }
}
