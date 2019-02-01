using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace Animation
{
   public class ArithmeticConverter : IValueConverter
   {
      private const string ArithmeticParseExpression = "([+\\-*/]{1,1})\\s{0,}(\\-?[\\d\\.]+)";
      private readonly Regex _arithmeticRegex = new Regex(ArithmeticParseExpression);

      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         if (!(value is double) || parameter == null)
            return null;

         var param = parameter.ToString();
         if (param.Length <= 0)
            return null;

         var match = _arithmeticRegex.Match(param);
         if (match.Groups.Count != 3)
            return null;

         var operation = match.Groups[1].Value.Trim();
         var numericValue = match.Groups[2].Value;

         double number;
         if (!double.TryParse(numericValue, out number))
            return null;

         var valueAsDouble = (double) value;
         double returnValue = 0;

         switch (operation)
         {
            case "+":
               returnValue = valueAsDouble + number;
               break;

            case "-":
               returnValue = valueAsDouble - number;
               break;

            case "*":
               returnValue = valueAsDouble*number;
               break;

            case "/":
               returnValue = valueAsDouble/number;
               break;
         }

         return returnValue;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         throw new NotImplementedException("The method or operation is not implemented.");
      }
   }
}