using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventureMaui.Models;
using TextAdventureMaui.Models.Missions;

namespace TextAdventureMaui.Services
{
    class NavigationService
    {
        public List<Room> Rooms { get; }
        public Room CurrentRoom { get; private set; }
        public Player Player { get; }

        public NavigationService(Player player, List<Room> rooms, Room mainHall)
        {
            Player = player;
            Rooms = rooms;
            CurrentRoom = mainHall;
        }

        public void ContinueStory()
        {
            var next = Rooms.FirstOrDefault(r => !r.IsExplored && r.Id != CurrentRoom.Id);
            if (next != null)
            {
                CurrentRoom = next;
                CurrentRoom.Enter(Player);
            }
            else
            {
                Console.WriteLine("You’ve completed all available rooms!");
            }
        }

        public void ExploreRoom(int roomId)
        {
            var room = Rooms.FirstOrDefault(r => r.Id == roomId && r.IsExplored);
            if (room != null)
            {
                CurrentRoom = room;
                CurrentRoom.Enter(Player);
            }
            else
            {
                Console.WriteLine("That room is not available for grinding yet!");
            }
        }

        public void ShowInventory()
        {
            Player.Inventory.PrintInventory();
        }

        public void ShowStats()
        {
            Console.WriteLine($"{Player.Name} - HP: {Player.CurrentHp}/{Player.MaxHp}, Attack: {Player.BaseAttack}");
            if (Player.EquippedWeapon != null)
                Console.WriteLine($"Equipped: {Player.EquippedWeapon.Name}");
        }
    }
}
}
