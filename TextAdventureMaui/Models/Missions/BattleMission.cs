using TextAdventureMaui.Models.Dialogues;
using TextAdventureMaui.Models.Entities;

namespace TextAdventureMaui.Models.Missions;

public class BattleMission : Mission
{
    public Enemy Enemy { get; }

    public BattleMission(
        string name,
        string description,
        string backgroundImage,
        string musicTrack,
        Enemy enemy,
        List<DialogueLine>? introDialogue = null,
        List<DialogueLine>? outroDialogue = null)
        : base(name, description, backgroundImage, musicTrack, introDialogue, outroDialogue)
    {
        Enemy = enemy;
    }
    
}