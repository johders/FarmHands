using System.Globalization;

namespace Mde.Project.Mobile.Converters
{
	public class NoImageToVisibilityConverter : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			bool doesNotHaveImage = String.IsNullOrEmpty((string)value);
			return doesNotHaveImage;
		}

		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
