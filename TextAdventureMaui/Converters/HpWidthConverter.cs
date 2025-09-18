using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureMaui.Converters
{
    public class HpWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int hp && parameter is string widthStr && int.TryParse(widthStr, out int fullWidth))
            {
                // Return width proportional to HP
                return fullWidth * hp / 100.0; // assuming maxHp = 100
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
