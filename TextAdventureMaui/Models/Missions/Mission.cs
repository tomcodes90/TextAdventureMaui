using TextAdventureMaui.Models.Dialogues;

namespace TextAdventureMaui.Models.Missions;

public abstract class Mission
{
    public string Name { get; }
    public string Description { get; }
    public string BackgroundImage { get; }
    public string MusicTrack { get; }

    public bool IsCompleted { get; set; }

    public List<DialogueLine>? IntroDialogue { get; }
    public List<DialogueLine>? OutroDialogue { get; }
    public Dictionary<int, DialogueNode>? BranchingDialogue { get; }

    protected Mission(
        string name,
        string description,
        string backgroundImage,
        string musicTrack,
        List<DialogueLine>? introDialogue = null,
        List<DialogueLine>? outroDialogue = null,
        Dictionary<int, DialogueNode>? branchingDialogue = null)
    {
        Name = name;
        Description = description;
        BackgroundImage = backgroundImage;
        MusicTrack = musicTrack;
        IntroDialogue = introDialogue;
        OutroDialogue = outroDialogue;
        BranchingDialogue = branchingDialogue;
    }
    
}