using System;

namespace ComparableObjects
{
   public sealed class ComplexNumber : IComparable
   {
      private const double DefaultReal = 0.0;
      private const double DefaultImaginary = 0.0;

      private readonly double _real;
      private readonly double _imaginary;

      public double Real
      {
         get
         {
            return _real;
         }
      }

      public double Imaginary
      {
         get
         {
            return _imaginary;
         }
      }

      public double Magnitude
      {
         get
         {
            return Math.Sqrt(Math.Pow(_real, 2) + Math.Pow(_imaginary, 2));
         }
      }

      public ComplexNumber(double real, double imaginary)
      {
         _real = real;
         _imaginary = imaginary;
      }

      public ComplexNumber() : this(DefaultReal, DefaultImaginary) { }

      private bool Equals(ComplexNumber other)
      {
         return _real.Equals(other._real) && _imaginary.Equals(other._imaginary);
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj))
            return false;
         if (ReferenceEquals(this, obj))
            return true;
         return obj is ComplexNumber && Equals((ComplexNumber)obj);
      }

      public override int GetHashCode()
      {
         unchecked
         {
            return (int)Magnitude;
         }
      }

      public int CompareTo(object obj)
      {
         var that = obj as ComplexNumber;
         if (that == null)
            throw new ArgumentException("obj");
         return Equals(that) ? 0 : Magnitude > that.Magnitude ? 1 : -1;
      }

      public static bool operator ==(ComplexNumber firstNumber, ComplexNumber secondNumber)
      {
         return Equals(firstNumber, secondNumber);
      }

      public static bool operator !=(ComplexNumber firstNumber, ComplexNumber secondNumber)
      {
         return !(firstNumber == secondNumber);
      }

      public override string ToString()
      {
         return string.Format("Real: {0}, Imaginary: {1}", _real, _imaginary);
      }
   }
}