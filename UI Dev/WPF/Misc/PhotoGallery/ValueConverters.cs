using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PhotoGallery
{
   public class CountToBackgroundConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter,
         CultureInfo culture)
      {
         if (targetType != typeof(Brush))
            throw new InvalidOperationException("The target must be a Brush!");
         
         var defaultBrush = Brushes.Transparent;
         return value != null ? (int.Parse(value.ToString()) == 0 ? parameter : defaultBrush) : defaultBrush;
      }

      public object ConvertBack(object value, Type targetType, object parameter,
         CultureInfo culture)
      {
         throw new NotSupportedException();
      }
   }

   public class RawCountToDescriptionConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter,
         CultureInfo culture)
      {         
         if (value != null)
         {
            var num = int.Parse(value.ToString());
            return num + (num == 1 ? " item" : " items");
         }

         return string.Empty;
      }

      public object ConvertBack(object value, Type targetType, object parameter,
         CultureInfo culture)
      {
         throw new NotSupportedException();
      }
   }

   public class DateTimeToDateConverter : IValueConverter
   {
      private const string DefaultDateTimeFormat = "MM/dd/yyyy";

      public object Convert(object value, Type targetType, object parameter,
         CultureInfo culture)
      {
         return value != null
            ? ((DateTime) value).ToString(DefaultDateTimeFormat)
            : DateTime.Now.ToString(DefaultDateTimeFormat);
      }

      public object ConvertBack(object value, Type targetType, object parameter,
         CultureInfo culture)
      {
         throw new NotSupportedException();
      }
   }
}