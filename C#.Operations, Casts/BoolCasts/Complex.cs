using System;

namespace _06_BoolCasts
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

      public override string ToString()
      {
         return string.Format("({0}, {1})", _real, _imaginary);
      }

      public double Magnitude
      {
         get { return Math.Sqrt(Math.Pow(_real, 2) + Math.Pow(_imaginary, 2)); }
      }

      public static implicit operator bool(Complex complex)
      {
         return (Math.Abs(complex._real) > double.Epsilon) || (Math.Abs(complex._imaginary) > double.Epsilon);
      }
   }
}