using TextAdventureMaui.Models.Dialogues;

namespace TextAdventureMaui.Models.Missions;

public class PuzzleMission : Mission
{
    public string Question { get; }
    public string Answer { get; }

    public PuzzleMission(
        string name,
        string description,
        string backgroundImage,
        string musicTrack,
        string question,
        string answer,
        List<DialogueLine>? introDialogue = null,
        List<DialogueLine>? outroDialogue = null)
        : base(name, description, backgroundImage, musicTrack, introDialogue, outroDialogue)
    {
        Question = question;
        Answer = answer;
    }
    
}