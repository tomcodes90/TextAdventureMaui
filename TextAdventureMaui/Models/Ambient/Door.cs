namespace TextAdventureMaui.Models.Ambient
{
    public class Door
    {
        public int RoomId { get; }
        public string Label { get; }
        public bool IsLocked { get; private set; }
        public string Image { get; }

        public Door(int roomId, string label, bool locked, string image = "")
        {
            RoomId = roomId;
            Label = label;
            IsLocked = locked;
            Image = image;
        }

        public void Unlock() => IsLocked = false;
    }
}
