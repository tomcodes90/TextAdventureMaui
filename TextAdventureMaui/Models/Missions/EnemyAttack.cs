using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureMaui.Models.Missions
{
    public enum DodgeInput
    {
        Up,
        Down,
        Left,
        Right
    }

    public class EnemyAttack
    {
        public string Name { get; set; } = string.Empty;
        public List<DodgeInput> Combo { get; set; } = new();
        public int Damage { get; set; }
    }
}
