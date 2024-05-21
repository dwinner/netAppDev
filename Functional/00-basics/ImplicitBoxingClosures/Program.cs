/*
 * Неявная упаковка захваченных переменных
 */

using System;

namespace _08_ImplicitBoxingClosures
{
   internal static class Program
   {
      public delegate void PrintAndIncrement();

      private static void Main()
      {
         var delegates = CreateDelegates();
         for (var i = 0; i < 3; i++)
         {
            delegates[i]();
         }

         /* 0
            1
            2 */
         Console.ReadKey();
      }

      private static PrintAndIncrement[] CreateDelegates()
      {
         var delegates = new PrintAndIncrement[3];
         var someVariable = 0;
         var anotherVariable = 1;
         for (var i = 0; i < 3; i++)
         {
            delegates[i] = delegate { Console.WriteLine(someVariable++); };
         }

         return delegates;
      }
   }
}