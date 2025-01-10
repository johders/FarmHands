using System.Globalization;

namespace Mde.Project.Mobile.Converters
{
	public class NoImageToVisibilityConverter : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
            if (value is string stringValue)
            {
                return string.IsNullOrEmpty(stringValue);
            }
            else if (value is ImageSource imageSource)
            {
                return false;
            }

            return true;
        }

		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
