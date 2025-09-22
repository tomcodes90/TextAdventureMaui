using TextAdventureMaui.Models;
using TextAdventureMaui.Models.Dialogues;
using TextAdventureMaui.Models.Missions;
using TextAdventureMaui.ViewModels;
using TextAdventureMaui.Views;

namespace TextAdventureMaui.Services;

public class MissionService
{
    private readonly DialogueService _dialogueService;

    public MissionService(DialogueService dialogueService)
    {
        _dialogueService = dialogueService;
    }

    public async Task StartMission(Mission mission, Player player)
    {
        Console.WriteLine($"[MissionService] Starting mission: {mission.Name}");

        // --- Intro dialogues ---
        PlayDialogue(mission.IntroDialogue, mission.BranchingDialogue);

        switch (mission)
        {
            case BattleMission battleMission:
                await StartBattleMission(battleMission, player);
                break;

            case PuzzleMission puzzleMission:
                await StartPuzzleMission(puzzleMission, player);
                break;

            case DobbleMission dobbleMission:
                await StartDobbleMission(dobbleMission, player);
                break;

            default:
                Console.WriteLine("[MissionService] Unknown mission type.");
                break;
        }
    }

    private async Task StartBattleMission(BattleMission mission, Player player)
    {
        var vm = new BattleViewModel(player, mission.Enemy);

        vm.BattleEnded += async (sender, reward) =>
        {
            if (reward.PlayerWon)
            {
                mission.IsCompleted = true;

                PlayDialogue(mission.OutroDialogue, mission.BranchingDialogue);

                await ShowReward(player, reward);
            }
            else
            {
                Console.WriteLine("[MissionService] Player lost the battle.");
            }
        };

        var page = new BattlePage(vm);
        await Shell.Current.Navigation.PushAsync(page);
    }

    private async Task StartPuzzleMission(PuzzleMission mission, Player player)
    {
        Console.WriteLine("[MissionService] Starting puzzle mission…");

        // ⚠️ qui la tua PuzzlePage dovrebbe restituire un ChallengeReward al termine
        var reward = new ChallengeReward(true, canChooseUpgrade: true);

        mission.IsCompleted = true;
        PlayDialogue(mission.OutroDialogue, mission.BranchingDialogue);

        await ShowReward(player, reward);
    }

    private async Task StartDobbleMission(DobbleMission mission, Player player)
    {
        Console.WriteLine("[MissionService] Starting dobble mission…");

        // ⚠️ qui la tua DobblePage dovrebbe restituire un ChallengeReward al termine
        var reward = new ChallengeReward(true, canChooseUpgrade: true);

        mission.IsCompleted = true;
        PlayDialogue(mission.OutroDialogue, mission.BranchingDialogue);

        await ShowReward(player, reward);
    }

    /// <summary>
    /// Mostra la ChallengeRewardPage con upgrade e abilità sbloccate.
    /// </summary>
    private async Task ShowReward(Player player, ChallengeReward reward)
    {
        var rewardPage = new ChallengeRewardPage(player, reward);
        await Shell.Current.Navigation.PushAsync(rewardPage);
    }

    /// <summary>
    /// Adatta automaticamente i dialoghi (lineari o ramificati).
    /// </summary>
    private void PlayDialogue(List<DialogueLine>? lines, Dictionary<int, DialogueNode>? branching)
    {
        if (branching != null && branching.Any())
        {
            _dialogueService.Play(branching, 0);
        }
        else if (lines != null && lines.Any())
        {
            var dict = lines
                .Select((line, index) => new DialogueNode(index, line))
                .ToDictionary(n => n.Id, n => n);

            _dialogueService.Play(dict, 0);
        }
    }
}
