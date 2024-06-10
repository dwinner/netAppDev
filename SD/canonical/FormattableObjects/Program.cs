/**
 * Форматирование объектов
 */

using System;
using System.Globalization;

namespace FormattableObjects
{
   internal class Program
   {
      private static void Main()
      {
         var num1 = new ComplexNumber(1.12345678, 2.12345678);
         Console.WriteLine("Формат US: {0}", num1.ToString("F5", new CultureInfo("en-US")));
         Console.WriteLine("Формат DE: {0}", num1.ToString("F5", new CultureInfo("de-DE")));
         Console.WriteLine("По-умолчанию: {0}", num1);
         Console.ReadKey();
      }
   }

   internal sealed class ComplexNumber : IFormattable
   {
      private readonly double _imaginary;
      private readonly double _real;

      public ComplexNumber(double real, double imaginary)
      {
         _real = real;
         _imaginary = imaginary;
      }

      public string ToString(string format, IFormatProvider formatProvider) =>
         string.Format("({0} {1})",
            _real.ToString(format, formatProvider),
            _imaginary.ToString(format, formatProvider));

      public override string ToString() => ToString("G", null);
   }
}