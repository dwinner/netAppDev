/*
 * Каррирование
 */

using System;

namespace _09_Carrying
{
   internal static class Program
   {
      private static void Main()
      {
         var binder = new Bind2dn(Add, 4);
         Console.WriteLine(binder.Binder(2));

         Console.ReadKey();
      }

      private static int Add(int x, int y) => x + y;
   }
}