using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureMaui.Models.Missions
{
    public class Puzzle
    {
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public List<Clue> RequiredClues { get; set; } = new();

        public bool Solve(string attempt)
        {
            return attempt.Equals(Answer, StringComparison.OrdinalIgnoreCase);
        }
    }

}
