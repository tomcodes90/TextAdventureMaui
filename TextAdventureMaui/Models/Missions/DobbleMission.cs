using TextAdventureMaui.Models.Dialogues;

namespace TextAdventureMaui.Models.Missions;

public class DobbleMission : Mission
{
    public int CardCount { get; }

    public DobbleMission(
        string name,
        string description,
        string backgroundImage,
        string musicTrack,
        int cardCount,
        List<DialogueLine>? introDialogue = null,
        List<DialogueLine>? outroDialogue = null)
        : base(name, description, backgroundImage, musicTrack, introDialogue, outroDialogue)
    {
        CardCount = cardCount;
    }
    
}