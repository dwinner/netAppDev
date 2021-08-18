using System;
using System.Text;

namespace _05_CustomFormatter
{
   public struct Complex : IFormattable
   {
      private readonly double _real;
      private readonly double _imaginary;

      public double Real
      {
         get { return _real; }
      }

      public double Imaginary
      {
         get { return _imaginary; }
      }

      public Complex(double real, double imaginary)
      {
         _real = real;
         _imaginary = imaginary;
      }

      public string ToString(string format, IFormatProvider formatProvider)
      {
         var stringBuilder = new StringBuilder();
         stringBuilder.Append("( ")
            .Append(_real.ToString(format, formatProvider))
            .Append(" : ")
            .Append(_imaginary.ToString(format, formatProvider))
            .Append(" )");
         return stringBuilder.ToString();
      }
   }
}