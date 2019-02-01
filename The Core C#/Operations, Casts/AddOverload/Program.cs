/**
 * Перегрузка операторов сложения
 */

using System;

namespace _01_AddOverload
{
   class Program
   {
      static void Main()
      {
         var complex1 = new Complex(1.0, 3.0);
         var complex2 = new Complex(1.0, 2.0);
         Complex complex3 = complex1 + complex2;
         Complex complex4 = 20.0 + complex1;
         Complex complex5 = complex1 + 25.0;
         Console.WriteLine("cpx1 == {0}", complex1);
         Console.WriteLine("cpx2 == {0}", complex2);
         Console.WriteLine("cpx3 == {0}", complex3);
         Console.WriteLine("cpx4 == {0}", complex4);
         Console.WriteLine("cpx5 == {0}", complex5);
         Console.ReadKey();
      }
   }

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

      public static Complex Add(Complex firstComplex, Complex secondComplex)
      {
         return new Complex(firstComplex._real + secondComplex._real,
                            firstComplex._imaginary + secondComplex._imaginary);
      }

      public static Complex Add(Complex aComplex, double aDouble)
      {
         return new Complex(aDouble + aComplex._real, aComplex._imaginary);
      }

      public static Complex operator +(Complex firstComplex, Complex secondComplex)
      {
         return Add(firstComplex, secondComplex);
      }

      public static Complex operator +(double number, Complex complex)
      {
         return Add(complex, number);
      }

      public static Complex operator +(Complex complex, double number)
      {
         return Add(complex, number);
      }
   }
}
