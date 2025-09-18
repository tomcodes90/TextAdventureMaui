
using TextAdventureMaui.Views;

namespace TextAdventureMaui.Models.Missions
{
    public class BattleMission : IMission
    {
        public string Title => "Battle!";
        public string Description => "Defeat the angry dishwasher.";

        public bool IsCompleted { get; private set; }
        public bool IsFailed { get; private set; }

        public async Task StartAsync()
        {
            // Launch battle screen
            await Shell.Current.GoToAsync(nameof(BattlePage));
        }

        public void Complete() => IsCompleted = true;
        public void Fail() => IsFailed = true;
    }

}
