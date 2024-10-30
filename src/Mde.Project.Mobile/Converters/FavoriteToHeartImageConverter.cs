using System.Globalization;

namespace Mde.Project.Mobile.Converters
{
	public class FavoriteToHeartImageConverter : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			string result = (bool)value ? "hearted.png" : "heart.png";
			return result;
		}

		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
