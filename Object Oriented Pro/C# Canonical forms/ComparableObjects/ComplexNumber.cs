using System;

namespace ComparableObjects
{
   public sealed class ComplexNumber : IComparable<ComplexNumber>
   {
      private const double DefaultReal = default(double);
      private const double DefaultImaginary = default(double);

      public ComplexNumber(double real = DefaultReal, double imaginary = DefaultImaginary)
      {
         Real = real;
         Imaginary = imaginary;
      }

      private double Real { get; }

      private double Imaginary { get; }

      private double Magnitude
         => Math.Sqrt(Math.Pow(Real, 2) + Math.Pow(Imaginary, 2));      

      private bool Equals(ComplexNumber other)
         => Real.Equals(other.Real) && Imaginary.Equals(other.Imaginary);

      public override bool Equals(object obj)
         =>
            !ReferenceEquals(null, obj) &&
            (ReferenceEquals(this, obj) || obj is ComplexNumber && Equals((ComplexNumber) obj));

      public override int GetHashCode()
      {
         unchecked
         {
            return (int) Magnitude;
         }
      }

      public static bool operator ==(ComplexNumber firstNumber, ComplexNumber secondNumber)
         => Equals(firstNumber, secondNumber);

      public static bool operator !=(ComplexNumber firstNumber, ComplexNumber secondNumber)
         => !(firstNumber == secondNumber);

      public override string ToString()
         => string.Format("Real: {0}, Imaginary: {1}", Real, Imaginary);

      public int CompareTo(ComplexNumber other)
         => Equals(other) ? 0 : Magnitude > other.Magnitude ? 1 : -1;
   }
}