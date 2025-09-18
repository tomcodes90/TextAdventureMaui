using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureMaui.Converters
{
    public class HpPercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int hp && parameter is string maxStr && int.TryParse(maxStr, out int maxHp))
            {
                double percent = Math.Max(0, (double)hp / maxHp);
                return new GridLength(percent, GridUnitType.Star);
            }
            return new GridLength(0, GridUnitType.Star);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

}
