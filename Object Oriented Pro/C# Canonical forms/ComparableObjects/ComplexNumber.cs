using System;
using System.Collections.Generic;

namespace ComparableObjects
{
   public class ComplexNumber : IComparable<ComplexNumber>, IEquatable<ComplexNumber>
   {
      #region IComparable<ComplexNumber>

      public int CompareTo(ComplexNumber other) => DefaultComparison(this, other);

      #endregion

      #region IEquatable<ComplexNumber>

      public bool Equals(ComplexNumber other)
         =>
            !ReferenceEquals(null, other) &&
            (ReferenceEquals(this, other) || Real.Equals(other.Real) && Imaginary.Equals(other.Imaginary));

      #endregion

      #region Private fields

      private const double DefaultReal = default(double);
      private const double DefaultImaginary = default(double);

      #endregion

      #region Ctor and public instance fields

      public ComplexNumber(double real = DefaultReal, double imaginary = DefaultImaginary)
      {
         Real = real;
         Imaginary = imaginary;
      }

      public double Real { get; }

      public double Imaginary { get; }

      #endregion

      #region Default single instances

      public static IEqualityComparer<ComplexNumber> DefaultEqualityComparer { get; }
         = new RealImaginaryEqualityComparer();

      public static IComparer<ComplexNumber> DefaultComparer { get; }
         = new ComplexNumberComparer();

      public static Comparison<ComplexNumber> DefaultComparison { get; }
         = (first, second)
            => first.Equals(second) ? 0 : first.ComputeMagnitude() > second.ComputeMagnitude() ? 1 : -1;

      #endregion

      #region System.Object

      public override bool Equals(object obj)
         =>
            !ReferenceEquals(null, obj) &&
            (ReferenceEquals(this, obj) || obj is ComplexNumber && Equals((ComplexNumber) obj));

      public override int GetHashCode()
      {
         unchecked
         {
            return (Real.GetHashCode()*397) ^ Imaginary.GetHashCode();
         }
      }

      public static bool operator ==(ComplexNumber firstNumber, ComplexNumber secondNumber)
         => Equals(firstNumber, secondNumber);

      public static bool operator !=(ComplexNumber firstNumber, ComplexNumber secondNumber)
         => !(firstNumber == secondNumber);

      public override string ToString()
         => string.Format("Real: {0}, Imaginary: {1}", Real, Imaginary);

      #endregion

      #region Private classes

      private sealed class ComplexNumberComparer : IComparer<ComplexNumber>
      {
         public int Compare(ComplexNumber x, ComplexNumber y) => DefaultComparison(x, y);
      }

      private sealed class RealImaginaryEqualityComparer : IEqualityComparer<ComplexNumber>
      {
         public bool Equals(ComplexNumber x, ComplexNumber y) => x.Equals(y);

         public int GetHashCode(ComplexNumber obj) => obj.GetHashCode();
      }

      #endregion
   }
}