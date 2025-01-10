using System.Globalization;

namespace Mde.Project.Mobile.Converters
{
    internal class BoolToHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 100 : 50; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value == 100;
        }
    }
}
