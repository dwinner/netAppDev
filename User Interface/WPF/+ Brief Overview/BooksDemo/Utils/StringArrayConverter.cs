using System;
using System.Globalization;
using System.Windows.Data;

namespace BooksDemo.Utils
{
   [ValueConversion(typeof(string[]), typeof(string))]
   public sealed class StringArrayConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         var values = value as string[];
         if (values == null || values.Length == 0)
            return null;

         var separator = parameter?.ToString() ?? string.Empty;

         return string.Join(separator, values);
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         throw new NotImplementedException();
      }
   }
}