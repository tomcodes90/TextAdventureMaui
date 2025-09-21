namespace TextAdventureMaui.Views;

public partial class RoomPage : ContentPage, IQueryAttributable
{
    public RoomPage()
    {
        InitializeComponent();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("RoomId"))
        {
            int roomId = (int)query["RoomId"];
            Title = $"Room {roomId}";
            // TODO: Load room data here
        }
    }
}
