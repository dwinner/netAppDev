using System;
using System.Globalization;
using System.Windows.Data;

namespace DataBinding
{
	public class PriceRangeProductGrouper : IValueConverter
	{
		public int GroupInterval { get; set; }

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var price = (decimal)value;
			if (price < GroupInterval)
			{
				return $"Less than {GroupInterval:C}";
			}

			var interval = (int)price / GroupInterval;
			var lowerLimit = interval * GroupInterval;
			var upperLimit = (interval + 1) * GroupInterval;

			return $"{lowerLimit:C} to {upperLimit:C}";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException("This converter is for grouping only.");
		}
	}
}