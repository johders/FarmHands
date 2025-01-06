using System.Globalization;

namespace Mde.Project.Mobile.Converters
{
	public class BoolToAvailabilityStringConverter : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value is not bool)
				throw new ArgumentException("Value provided is not a bool, Cannot convert.");
			
			bool isAvailable = (bool)value;

			string convertedString = isAvailable ? "Available" : "Unavailable";

			return convertedString;
		}

		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
