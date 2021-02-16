/**
 * Форматирование объектов
 */

using System;
using System.Globalization;

namespace FormattableObjects
{
   class Program
   {
      static void Main()
      {
         var num1 = new ComplexNumber(1.12345678, 2.12345678);
         Console.WriteLine("Формат US: {0}", num1.ToString("F5", new CultureInfo("en-US")));
         Console.WriteLine("Формат DE: {0}", num1.ToString("F5", new CultureInfo("de-DE")));
         Console.WriteLine("По-умолчанию: {0}", num1);
         Console.ReadKey();
      }
   }

   sealed class ComplexNumber : IFormattable
   {
      private readonly double _real;
      private readonly double _imaginary;

      public ComplexNumber(double real, double imaginary)
      {
         _real = real;
         _imaginary = imaginary;
      }

      public override string ToString()
      {
         return ToString("G", null);
      }

      public string ToString(string format, IFormatProvider formatProvider)
      {
         return string.Format("({0} {1})", _real.ToString(format, formatProvider),
            _imaginary.ToString(format, formatProvider));
      }
   }
}
