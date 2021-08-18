using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ImageViewer
{
   internal class FilenameToColorConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         return string.IsNullOrEmpty(value as string) ? Brushes.Red : Brushes.Green;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         throw new NotImplementedException();
      }
   }
}