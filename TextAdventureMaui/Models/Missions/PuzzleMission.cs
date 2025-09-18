using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventureMaui.Views;
namespace TextAdventureMaui.Models.Missions
{
    public class PuzzleMission : IMission
    {
        public string Title { get; set; } = "Solve the Puzzle!";
        public string Description { get; set; } = "Find the clues and solve the puzzle.";
        public bool IsCompleted { get; private set; } = false;
        public bool IsFailed { get; private set; } = false;

        public Puzzle Puzzle { get; set; } = new();

        public Task StartAsync()
        {
            // Here you would navigate to a PuzzlePage in MAUI
            return Shell.Current.GoToAsync(nameof(PuzzlePage));
        }

        public void Complete() => IsCompleted = true;
        public void Fail() => IsFailed = true;
    }
}
