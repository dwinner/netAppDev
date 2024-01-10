/**
 * Обобщенные структуры
 */

using System;

namespace GenericPoint
{
   class Program
   {
      static void Main()
      {
         Console.WriteLine("***** Fun with Generic Structures *****\n");

         // Целые точки
         Point<int> p = new Point<int>(10, 10);
         Console.WriteLine("p.ToString()={0}", p);
         p.ResetPoint();
         Console.WriteLine("p.ToString()={0}", p);
         Console.WriteLine();

         // Точки двойной точности
         Point<double> p2 = new Point<double>(5.4, 3.3);
         Console.WriteLine("p2.ToString()={0}", p2);
         p2.ResetPoint();
         Console.WriteLine("p2.ToString()={0}", p2);

         Console.ReadLine();
      }
   }
}
