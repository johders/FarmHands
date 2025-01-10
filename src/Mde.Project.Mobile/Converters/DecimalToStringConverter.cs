using System.Globalization;

namespace Mde.Project.Mobile.Converters
{
    public class DecimalToStringConverter : IValueConverter
    {        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal decimalValue)
            {
                return decimalValue.ToString("N2", culture);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue && decimal.TryParse(stringValue, NumberStyles.Any, culture, out var decimalValue))
            {
                return decimalValue;
            }
            return 0m;
        }
    }
}
