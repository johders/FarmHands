using System.Globalization;

namespace Mde.Project.Mobile.Converters
{
	public class OfferCountToStringConverter : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			int offerCount = (int)value;
			string singular = "offer";
			string plural = "offers";

			return offerCount == 1 ? $"{offerCount} {singular}" : $"{offerCount} {plural}";

		}

		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
