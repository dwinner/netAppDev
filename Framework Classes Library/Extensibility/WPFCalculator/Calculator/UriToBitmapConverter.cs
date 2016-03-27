using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Calculator.Properties;

namespace Calculator
{
   public class UriToBitmapConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         string uri = value.ToString();
         if (string.IsNullOrEmpty(uri))
            return null;

         FileStream stream = File.OpenRead(Path.Combine(Settings.Default.AddInDirectory, uri));
         var image = new BitmapImage();
         image.BeginInit();
         image.StreamSource = stream;
         image.EndInit();

         return image;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         throw new NotImplementedException();
      }
   }
}