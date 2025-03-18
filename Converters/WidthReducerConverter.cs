using System;
using System.Globalization;
using System.Windows.Data;

namespace SistemaGestion.Converters
{
    public class WidthReducerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double width && parameter is string percentStr && double.TryParse(percentStr, out double percent))
            {
                return width * (1 - (percent / 100));
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}