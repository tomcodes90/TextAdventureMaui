using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureMaui.Models.Missions
{
    public class Door
    {
        public int RoomId { get; }
        public bool IsLocked { get; private set; }

        public Door(int roomId, bool locked = true)
        {
            RoomId = roomId;
            IsLocked = locked;
        }

        public void Unlock()
        {
            IsLocked = false;
        }
    }
}
