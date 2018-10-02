using System;

namespace EqualsAndPerformance
{
   public struct ComplexNumber : IComparable,
                                 IComparable<ComplexNumber>,
                                 IEquatable<ComplexNumber>
   {
      private readonly double _real;
      private readonly double _imaginary;

      public ComplexNumber(double real, double imaginary)
      {
         _real = real;
         _imaginary = imaginary;
      }

      public bool Equals(ComplexNumber other)
      {
         return Math.Abs(_real - other._real) < double.Epsilon &&
                Math.Abs(_imaginary - other._imaginary) < double.Epsilon;
      }

      public override bool Equals(object other) // NOTE: Внешнему коду придется делать упаковку, а методу - распаковку
      {
         bool result = false;
         if (other is ComplexNumber)
         {
            var that = (ComplexNumber)other;
            result = Equals(that);
         }
         return result;
      }

      public override int GetHashCode()
      {
         return (int)Magnitude;
      }

      public static bool operator ==(ComplexNumber num1, ComplexNumber num2)
      {
         return num1.Equals(num2);
      }

      public static bool operator !=(ComplexNumber num1, ComplexNumber num2)
      {
         return !(num1 == num2);
      }

      public int CompareTo(ComplexNumber other)
      {
         return Equals(other) ? 0 : Magnitude > other.Magnitude ? 1 : -1;
      }

      int IComparable.CompareTo(object other)  // NOTE: Небезопасная версия делегирует вызовы безопасной
      {
         if (!(other is ComplexNumber))
         {
            throw new ArgumentException("other");
         }
         return CompareTo((ComplexNumber)other);
      }

      public double Magnitude { get { return Math.Sqrt(Math.Pow(_real, 2) + Math.Pow(_imaginary, 2)); } }
   }
}