using System;

namespace _05_BoolOperations
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

      public double Magnitude
      {
         get { return Math.Sqrt(Math.Pow(_real, 2) + Math.Pow(_imaginary, 2)); }
      }

      public static bool operator true(Complex complex)
      {
         return (Math.Abs(complex._real) > double.Epsilon) || (Math.Abs(complex._imaginary) > double.Epsilon);
      }

      public static bool operator false(Complex complex)
      {
         return (Math.Abs(complex._real) < double.Epsilon) && (Math.Abs(complex._imaginary) < double.Epsilon);
      }
   }
}