using System.Globalization;

namespace TextAdventureMaui.Converters;

public class PercentageToStarConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double percent)
        {
            // Clamp between 0 and 1
            percent = Math.Max(0, Math.Min(1, percent));
            return new GridLength(percent, GridUnitType.Star);
        }
        return new GridLength(0, GridUnitType.Star);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}