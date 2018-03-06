using System;
using System.Globalization;
using System.Windows.Data;

namespace ValueConverters
{
   public sealed class ValueMinMaxToIsLargeArcConverter : IMultiValueConverter
   {
      public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
      {
         var value = (double) values[0];
         var minimum = (double) values[1];
         var maximum = (double) values[2];

         // Only return true if the value is 50% of the range or greater
         return value * 2 >= maximum - minimum;
      }

      public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
      {
         throw new NotImplementedException();
      }
   }
}