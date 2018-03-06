/**
 * Перегрузка операторов сравнения
 */

using System;

namespace _02_CompareOverload
{
   class Program
   {
      static void Main()
      {
         var complex1 = new Complex(1.0, 3.0);
         var complex2 = new Complex(1.0, 2.0);
         Console.WriteLine("cpx1 = {0}, cpx1.Magnitude = {1}", complex1, complex1.Magnitude);
         Console.WriteLine("cpx2 = {0}, cpx2.Magnitude = {1}", complex2, complex2.Magnitude);
         Console.WriteLine("cpx1 == cpx2 ? {0}", complex1 == complex2);
         Console.WriteLine("cpx1 != cpx2 ? {0}", complex1 != complex2);
         Console.WriteLine("cpx1 < cpx2 ? {0}", complex1 < complex2);
         Console.WriteLine("cpx1 > cpx2 ? {0}", complex1 > complex2);
         Console.WriteLine("cpx1 <= cpx2 ? {0}", complex1 <= complex2);
         Console.WriteLine("cpx1 >= cpx2 ? {0}", complex1 >= complex2);

         Console.ReadKey();
      }
   }

   public struct Complex : IEquatable<Complex>,
                           IComparable,
                           IComparable<Complex>
   {
      private readonly double _real;
      private readonly double _img;

      public Complex(double real, double img)
      {
         _real = real;
         _img = img;
      }

      public bool Equals(Complex other)   // Версия, безопасная к типам
      {
         return (Math.Abs(_real - other._real) < double.Epsilon && Math.Abs(_img - other._img) < double.Epsilon);
      }

      public int CompareTo(Complex other) // Версия, безопасная к типам
      {
         return Equals(other) ? 0 : Magnitude > other.Magnitude ? 1 : -1;
      }

      int IComparable.CompareTo(object obj)  // Реализация IComparable
      {
         if (!(obj is Complex))
            throw new ArgumentException("Неверное сравнение");
         return CompareTo((Complex)obj);
      }

      public double Magnitude { get { return Math.Sqrt(Math.Pow(_real, 2) + Math.Pow(_img, 2)); } }

      #region Перегрузка System.Object

      public override bool Equals(object other)
      {
         bool result = false;
         if (other is Complex)
         {
            result = Equals((Complex)other);
         }
         return result;
      }

      public override int GetHashCode()
      {
         return (int)Magnitude;
      }

      public override string ToString()
      {
         return string.Format("({0}, {1})", _real, _img);
      }

      #endregion

      #region Перегруженные операторы

      public static bool operator ==(Complex lhsComplex, Complex rhsComplex)
      {
         return lhsComplex.Equals(rhsComplex);
      }

      public static bool operator !=(Complex lhsComplex, Complex rhsComplex)
      {
         return !lhsComplex.Equals(rhsComplex);
      }

      public static bool operator <(Complex lhsComplex, Complex rhsComplex)
      {
         return lhsComplex.CompareTo(rhsComplex) < 0;
      }

      public static bool operator >(Complex lhsComplex, Complex rhsComplex)
      {
         return lhsComplex.CompareTo(rhsComplex) > 0;
      }

      public static bool operator <=(Complex lhsComplex, Complex rhsComplex)
      {
         return lhsComplex.CompareTo(rhsComplex) <= 0;
      }

      public static bool operator >=(Complex lhsComplex, Complex rhsComplex)
      {
         return lhsComplex.CompareTo(rhsComplex) >= 0;
      }

      #endregion

   }
}
