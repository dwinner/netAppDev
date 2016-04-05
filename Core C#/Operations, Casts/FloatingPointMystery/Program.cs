/**
 * Загадка плавающей запятой
 */

using System;

namespace FloatingPointMystery
{
   class Program
   {
      static void Main()
      {
         const long l1 = long.MaxValue - 5;
         const long l2 = long.MaxValue - 4;
         const double d1 = l1;
         const double d2 = l2;
         Console.WriteLine(d1);
         Console.WriteLine(d2);
         Console.WriteLine(l1 == l2);
         Console.WriteLine(d1 == d2);
         Console.WriteLine(Math.Abs(d1 - d2) < double.Epsilon);   // Не поможет

         Console.ReadKey();
      }
   }
}
