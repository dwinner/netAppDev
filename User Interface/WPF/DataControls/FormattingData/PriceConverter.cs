using System;
using System.Globalization;
using System.Windows.Data;

namespace DataBinding
{
   [ValueConversion(typeof (decimal), typeof (string))]
   public class PriceConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         var price = (decimal) value;
         return price.ToString("c", culture);
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         var price = value.ToString();
         decimal result;
         return decimal.TryParse(price, NumberStyles.Any, culture, out result) ? result : value;
      }
   }
}