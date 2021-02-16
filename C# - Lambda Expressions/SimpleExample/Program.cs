/**
 * Простой пример лямбда-выражения с парметром
 */

using System;

namespace _01_SimpleExample
{
   class Program
   {
      static void Main()
      {
         Func<double, double> expr = x => x / 2;
         const int someNumber = 9;
         Console.WriteLine("Результат: {0}", expr(someNumber));
         Console.ReadKey();
      }
   }
}
