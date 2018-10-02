using System;
using System.Globalization;
using System.Windows.Data;

namespace MultiBindingDemo
{
   public sealed class PersonNameConverter : IMultiValueConverter
   {
      public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
      {
         //Contract.Requires<ArgumentException>(values != null && values.Length == 2);
         //Contract.Requires<ArgumentNullException>(parameter != null);
         //Contract.Requires<InvalidCastException>(parameter is string);

         switch ((string) parameter)
         {
            case "FirstLast":
               return $"{values[0]} {values[1]}";
            case "LastFirst":
               return $"{values[1]} {values[0]}";
            default:
               throw new ArgumentException($"Invalid argument {parameter}");
         }
      }

      public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
      {
         throw new NotImplementedException();
      }
   }
}