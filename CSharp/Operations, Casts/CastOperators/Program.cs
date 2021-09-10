/**
 * Операции преобразования
 */

using System;

namespace _03_CastOperators
{
   class Program
   {
      static void Main()
      {
         var complex1 = new Complex(1.0, 3.0);
         Complex complex2 = 2.0; // Использовать неявную операцию
         var d = (double)complex1; // Использовать явную операцию
         Console.WriteLine("cpx1 = {0}", complex1);
         Console.WriteLine("cpx2 = {0}", complex2);
         Console.WriteLine("d = {0}", d);

         Console.ReadKey();
      }
   }
}
