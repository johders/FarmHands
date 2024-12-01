using System.Globalization;

namespace Mde.Project.Mobile.Converters
{
	public class OfferCountToStringConverter : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
            if (value == null)
            {
                return "No offers";
            }

			if (value is int offerCount)
			{
                string singular = "offer";
                string plural = "offers";

                return offerCount == 1 ? $"{offerCount} {singular}" : $"{offerCount} {plural}";
            }

            throw new InvalidOperationException("Value must be an integer");
        }

		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
