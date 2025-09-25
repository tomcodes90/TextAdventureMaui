using System.Text.Json;
using TextAdventureMaui.Models;
using TextAdventureMaui.Models.Dialogues;
using TextAdventureMaui.Models.Missions;
using TextAdventureMaui.Services;
using TextAdventureMaui.Views;

public class MissionRunner
{
    private readonly ConversationService _conversationService;    // event-driven conversation. :contentReference[oaicite:15]{index=15}
    private readonly Dictionary<ChallengeType, IChallengeExecutor> _executors;
    private readonly ChallengeRewardService _rewardService;       // apply items / bonuses. :contentReference[oaicite:16]{index=16}
    private readonly PlayerService _playerService;
    private readonly AbilityFactory _abilityFactory;              // for ResultPage UI
    private readonly IServiceProvider _services;

    public MissionRunner(
        ConversationService conversationService,
        Dictionary<ChallengeType, IChallengeExecutor> executors,
        ChallengeRewardService rewardService,
        PlayerService playerService,
        AbilityFactory abilityFactory,
        IServiceProvider services)
    {
        _conversationService = conversationService;
        _executors = executors;
        _rewardService = rewardService;
        _playerService = playerService;
        _abilityFactory = abilityFactory;
        _services = services;
    }

    public async Task RunMissionAsync(MissionData mission)
    {
        foreach (var step in mission.Steps)
        {
            if (step.Type == MissionStepType.Conversation)
            {
                // Load conversation data (you should have a Dialogue/Conversation loader)
                var conversation = await LoadConversationByIdAsync(step.DialogueId!);
                var tcs = new TaskCompletionSource<bool>();

                void OnEnded() => tcs.TrySetResult(true);

                _conversationService.ConversationEnded += OnEnded;
                _conversationService.StartConversation(conversation); // event-driven play. :contentReference[oaicite:17]{index=17}

                await tcs.Task; // wait until conversation ended
                _conversationService.ConversationEnded -= OnEnded;
            }
            else if (step.Type == MissionStepType.Challenge)
            {
                if (step.ChallengeType == null)
                    throw new Exception("Challenge step missing challengeType.");

                if (!_executors.TryGetValue(step.ChallengeType.Value, out var executor))
                    throw new Exception($"No executor registered for {step.ChallengeType.Value}");

                // execute challenge and get result
                var result = await executor.ExecuteAsync(step.Settings);

                // show result page and wait for player to press Continue
                var tcs = new TaskCompletionSource<bool>();
                void OnContinue()
                {
                    tcs.TrySetResult(true);
                }

                // create and push result page — page must call the action when Continue clicked
                await Shell.Current.Navigation.PushAsync(
                    new ChallengeResultPage(result, _abilityFactory, () => OnContinue())
                );

                // wait for continue
                await tcs.Task;

                // apply result to player (only after player pressed continue)
                var player = _playerService.CurrentPlayer;
                _rewardService.ApplyResult(player, result);
                // close result page
                await Shell.Current.Navigation.PopAsync();
            }
        }

        // mission-level reward applied after steps
        if (mission.Reward != null)
        {
            var player = _playerService.CurrentPlayer;
            var rewardResult = new ChallengeResult(true, mission.Reward.Loot, bonus: null, unlockedAbilityId: null);
            _rewardService.ApplyResult(player, rewardResult);
        }
    }

    // Example loader - implement as you have it
    private Task<Conversation> LoadConversationByIdAsync(string id)
    {
        // TODO: implement lookup from dialogues JSON/Service and return Conversation model expected by ConversationService.
        throw new NotImplementedException();
    }
}
