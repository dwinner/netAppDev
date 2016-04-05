/**
 * Обработка комплекных чисел
 */

using System;
using System.Numerics;
using static System.Numerics.Complex;

namespace ComplexNumberDemo
{
   internal static class Program
   {
      private static void Main()
      {
         var a = new Complex(2, 1);
         var b = new Complex(3, 2);
         Console.WriteLine("a = {0}", a);
         Console.WriteLine("b = {0}", b);
         Console.WriteLine("a + b = {0}", a + b);
         Console.WriteLine("pow(a,2) = {0}", Pow(a, 2));
         Console.WriteLine("a / 0 = {0}", a/Zero);

         // -1 - вещественная часть, а 0 - мнимая
         Complex c = -1;
         Console.WriteLine("c = {0}", c);
         Console.WriteLine("Sqrt(c) = {0}", Sqrt(c));
         Console.WriteLine("Sqrt(c) = {0}",
            string.Format(new ComplexFormatter(), "{0:i}", Sqrt(c)));

         Console.ReadKey();
      }
   }
}