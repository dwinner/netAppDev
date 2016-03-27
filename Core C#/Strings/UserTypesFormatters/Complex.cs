using System;
using System.Text;

namespace _04_UserTypesFormatters
{
   public struct Complex : IFormattable
   {
      private readonly double _real;
      private readonly double _imaginary;

      public Complex(double real, double imaginary)
      {
         _real = real;
         _imaginary = imaginary;
      }

      public string ToString(string format, IFormatProvider formatProvider)
      {
         var stringBuilder = new StringBuilder();
         if (format == "DBG")
         {
            // Генерация отладочного вывода для данного объекта
            stringBuilder.Append("( ")
               .Append(GetType())
               .Append(Environment.NewLine)
               .AppendFormat("\tдействительная:\t{0}{1}", _real, Environment.NewLine)
               .AppendFormat("\tмнимая:\t{0}{1}", _imaginary, Environment.NewLine);
         }
         else
         {
            stringBuilder.Append("( ")
               .Append(_real.ToString(format, formatProvider))
               .Append(" : ")
               .Append(_imaginary.ToString(format, formatProvider))
               .Append(" )");
         }
         return stringBuilder.ToString();
      }
   }
}