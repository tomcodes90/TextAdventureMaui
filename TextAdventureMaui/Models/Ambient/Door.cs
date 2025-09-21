using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureMaui.Models.Ambient
{
    public class Door
    {
        public int RoomId { get; }
        public string Label { get; }
        public bool IsLocked { get; private set; }

        public Door(int roomId, string label, bool locked)
        {
            RoomId = roomId;
            Label = label;
            IsLocked = locked;
        }

        public void Unlock() => IsLocked = false;
    }
}
