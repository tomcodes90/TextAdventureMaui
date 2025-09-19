using TextAdventureMaui.Models;
using TextAdventureMaui.Models.Dialogues;
using TextAdventureMaui.Models.Entities;
using TextAdventureMaui.Models.Missions;

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

    public override void Start(Player player)
    {
        Console.WriteLine($"Mission: {Name}");

        PlayDialogue(IntroDialogue);

        Console.WriteLine($"Enemy {Enemy.Name} appears!");
        // TODO: Hook into BattleService here

        // Simulate win for now
        IsCompleted = true;

        if (IsCompleted)
        {
            PlayDialogue(OutroDialogue);
        }
    }
}