
using TextAdventureMaui.Views;

namespace TextAdventureMaui.Models.Missions
{
    public class DobbleMission : IMission
    {
        public string Title => "Dobble Match!";
        public string Description => "Find the matching symbol before time runs out.";

        public bool IsCompleted { get; private set; }
        public bool IsFailed { get; private set; }

        public async Task StartAsync()
        {
            // Navigate to Dobble mini-game page
            await Shell.Current.GoToAsync(nameof(DobblePage));
        }

        public void Complete() => IsCompleted = true;
        public void Fail() => IsFailed = true;
    }

}
