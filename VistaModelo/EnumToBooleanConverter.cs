using System;
using System.Globalization;
using System.Windows.Data;

namespace SistemaGestion.VistaModelo
{
    public class EnumToBooleanConverter : IValueConverter
    {
        // Convierte el valor del enum a booleano para enlazar con IsChecked.
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;

            string parameterString = parameter.ToString();
            if (!Enum.IsDefined(value.GetType(), value))
                return false;

            object parameterValue = Enum.Parse(value.GetType(), parameterString);
            return parameterValue.Equals(value);

        }

        // Convierte de vuelta el booleano al valor del enum.
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return Binding.DoNothing;
            if ((bool)value)
            {
                return Enum.Parse(targetType, parameter.ToString());
            }
            return Binding.DoNothing;
        }


    }
}
