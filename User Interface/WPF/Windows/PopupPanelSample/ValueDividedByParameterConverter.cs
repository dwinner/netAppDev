using System;
using System.Globalization;
using System.Windows.Data;

namespace PopupPanelSample
{
   public class ValueDividedByParameterConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         double n, d;
         return double.TryParse(value.ToString(), out n) && double.TryParse(parameter.ToString(), out d) &&
                Math.Abs(d) > double.Epsilon
            ? n / d
            : 0;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         return default(object);
      }
   }
}