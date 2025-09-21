using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventureMaui.Models.Ambient;
using TextAdventureMaui.Models.Entities;

namespace TextAdventureMaui.Models.Missions
{
    public class MainHall
    {
        public List<Door> Doors { get; } = new();
        public List<Npc> Npcs { get; } = new();

        public MainHall()
        {
            // Initialize 4 doors
            Doors.Add(new Door(1, "Room 1", false)); // Room 1 unlocked by default
            Doors.Add(new Door(2, "Room 2", true));
            Doors.Add(new Door(3, "Room 3", true));
            Doors.Add(new Door(4, "Room 4", true));
        }

        public void UnlockDoor(int roomId)
        {
            var door = Doors.FirstOrDefault(d => d.RoomId == roomId);
            if (door != null) door.Unlock();
        }
    }
}
