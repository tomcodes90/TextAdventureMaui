using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureMaui.Models.Missions
{
    public class DobbleDeck
    {
        public List<string> Icons { get; set; } = new();
        public List<List<string>> Cards { get; set; } = new();
    }

}
