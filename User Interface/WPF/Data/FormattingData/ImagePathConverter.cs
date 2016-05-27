using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace DataBinding
{
   public class ImagePathConverter : IValueConverter
   {
      readonly string imageDirectory = Directory.GetCurrentDirectory();

      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         var imagePath = Path.Combine(imageDirectory, (string) value);
         return new BitmapImage(new Uri(imagePath));
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         throw new NotSupportedException("The method or operation is not implemented.");
      }
   }
}