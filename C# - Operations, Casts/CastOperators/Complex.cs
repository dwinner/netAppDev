using System;

namespace _03_CastOperators
{
   public struct Complex
   {
      private readonly double _real;
      private readonly double _imaginary;

      public Complex(double real, double imaginary)
      {
         _real = real;
         _imaginary = imaginary;
      }

      private double Magnitude
      {
         get { return Math.Sqrt(Math.Pow(_real, 2) + Math.Pow(_imaginary, 2)); }
      }

      public static implicit operator Complex(double d)
      {
         return new Complex(d, 0);
      }

      public static explicit operator double(Complex complex)
      {
         return complex.Magnitude;
      }

      public override string ToString()
      {
         return string.Format("({0}, {1})", _real, _imaginary);
      }
   }
}