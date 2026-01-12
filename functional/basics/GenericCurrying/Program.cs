/*
 * Обобщенное каррирование
 */

using System;

namespace _10_GenericCarrying
{
   internal static class Program
   {
      private static void Main()
      {
         var binder = new Bind2Nd<int, int, int>(Add, 4);
         Console.WriteLine(binder.Binder(2));

         Console.ReadKey();
      }

      private static int Add(int x, int y) => x + y;
   }
}