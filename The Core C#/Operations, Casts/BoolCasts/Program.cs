/**
 * Булевы операции неявного преобразования
 */

using System;

namespace _06_BoolCasts
{
   class Program
   {
      static void Main()
      {
         var complex1 = new Complex(1.0, 3.0);
         Console.WriteLine(complex1 ? "cpx1 равно true" : "cpx1 равно false");
         var complex2 = new Complex(0, 0);
         Console.WriteLine("cpx2 равно {0}", complex2 ? "true" : "false");

         Console.ReadKey();
      }
   }
}
