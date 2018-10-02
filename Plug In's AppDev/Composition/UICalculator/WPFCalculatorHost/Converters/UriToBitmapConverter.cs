using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WPFCalculatorHost.Converters
{
	public class UriToBitmapConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var uri = value?.ToString();
			if (uri != null)
			{
				var stream = File.OpenRead(uri);
				var image = new BitmapImage();
				image.BeginInit();
				image.StreamSource = stream;
				image.EndInit();

				return image;
			}

			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}