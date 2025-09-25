namespace TextAdventureMaui.Models.Missions;


public class MissionStep
{
    public MissionStepType Type { get; set; }
    public string? DialogueId { get; set; }
    public ChallengeType? ChallengeType { get; set; }
    public Dictionary<string, object>? Settings { get; set; }
}