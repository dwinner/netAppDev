using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ValueConverters
{
   public sealed class ValueMinMaxToPointConverter : IMultiValueConverter
   {
      public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
      {
         var value = (double) values[0];
         var minimum = (double) values[1];
         var maximum = (double) values[2];

         // Convert the value to one between 0 and 360
         var current = value / (maximum - minimum) * 360;

         // Adjust the finished state so the ArcSegment gets drawn as a whole circle
         if (Math.Abs(current - 360) < double.Epsilon)
            current = 359.999;

         // Shift by 90 degrees so 0 starts at the top of the circle
         current = current - 90;

         // Convert the angle to radians
         current = current * 0.017453292519943295;

         // Calculate the circle's point
         var x = 10 + 10 * Math.Cos(current);
         var y = 10 + 10 * Math.Sin(current);

         return new Point(x, y);         
      }

      public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
      {
         throw new NotImplementedException();
      }
   }
}