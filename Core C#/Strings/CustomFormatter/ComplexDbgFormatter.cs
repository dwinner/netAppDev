using System;
using System.Globalization;
using System.Text;

namespace _05_CustomFormatter
{
   /// <summary>
   /// Собственный поставщик форматирования
   /// </summary>
   public class ComplexDbgFormatter : ICustomFormatter, IFormatProvider
   {
      public string Format(string format, object arg, IFormatProvider formatProvider)
      {
         if (arg is Complex && format == "DBG")
         {
            var complex = (Complex)arg;
            // Сгенерировать отладочный вывод для данного объекта
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("( ")
               .Append(GetType())
               .Append(Environment.NewLine)
               .AppendFormat("\tдействительная:\t{0}{1}", complex.Real, Environment.NewLine)
               .AppendFormat("\tмнимая:\t{0}{1}", complex.Imaginary, Environment.NewLine);
            return stringBuilder.ToString();
         }
         var formattable = arg as IFormattable;
         return formattable != null ? formattable.ToString(format, formatProvider) : arg.ToString();
      }

      public object GetFormat(Type formatType)
      {
         return formatType == typeof(ICustomFormatter) ? this : CultureInfo.CurrentCulture.GetFormat(formatType);
      }
   }
}