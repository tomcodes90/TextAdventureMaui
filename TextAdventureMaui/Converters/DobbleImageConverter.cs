using System.Globalization;

namespace TextAdventureMaui.Converters;

public class DobbleImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string filename)
            return filename; // just the filename, e.g. "dobble_1.png"

        return null!;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}

