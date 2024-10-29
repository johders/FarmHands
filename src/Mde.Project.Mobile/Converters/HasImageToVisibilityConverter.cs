using System.Globalization;

namespace Mde.Project.Mobile.Converters
{
	public class HasImageToVisibilityConverter : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value is not string)
				throw new ArgumentException("Value provided is not a string, Cannot convert.");

			bool hasImage = !String.IsNullOrEmpty((string)value);

			return hasImage;
		}

		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
