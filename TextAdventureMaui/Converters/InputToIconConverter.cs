using System.Globalization;
using Microsoft.Maui.Controls;

namespace TextAdventureMaui.Converters;

public class InputToIconConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string input) return null;

        return input switch
        {
            "Up" => "uparrow.png",
            "Down" => "downarrow.png",
            "Left" => "leftarrow.png",
            "Right" => "rightarrow.png",
            "Action" => "lettera.png",
            _ => "placeholder.png"
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}