using TextAdventureMaui.Models.Dialogues;

namespace TextAdventureMaui.Models.Missions
{

    public abstract class Mission
    {
        public string Name { get; }
        public string Description { get; }
        public string BackgroundImage { get; }
        public string MusicTrack { get; }
        public List<DialogueLine> IntroDialogue { get; }
        public List<DialogueLine> OutroDialogue { get; }
        public bool IsCompleted { get; protected set; }

        protected Mission(
            string name,
            string description,
            string backgroundImage,
            string musicTrack,
            List<DialogueLine>? introDialogue = null,
            List<DialogueLine>? outroDialogue = null)
        {
            Name = name;
            Description = description;
            BackgroundImage = backgroundImage;
            MusicTrack = musicTrack;
            IntroDialogue = introDialogue ?? new List<DialogueLine>();
            OutroDialogue = outroDialogue ?? new List<DialogueLine>();
            IsCompleted = false;
        }

        public abstract void Start(Player player);

        protected void PlayDialogue(List<DialogueLine> dialogue)
        {
            foreach (var line in dialogue)
            {
                Console.WriteLine($"{line.Speaker}: {line.Text}");
            }
        }
    }
}