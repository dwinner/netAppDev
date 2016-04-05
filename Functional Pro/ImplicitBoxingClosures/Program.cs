/**
 * Неявная упаковка захваченных переменных
 */

using System;

namespace _08_ImplicitBoxingClosures
{
   class Program
   {
      static void Main(string[] args)
      {
         PrintAndIncrement[] delegates = CreateDelegates();
         for (int i = 0; i < 3; i++)
         {
            delegates[i]();
         }
         /* 0
            1
            2 */
         Console.ReadKey();
      }

      public delegate void PrintAndIncrement();

      private static PrintAndIncrement[] CreateDelegates()
      {
         PrintAndIncrement[] delegates = new PrintAndIncrement[3];
         int someVariable = 0;
         int anotherVariable = 1;
         for (int i = 0; i < 3; i++)
         {
            delegates[i] = delegate
            {
               Console.WriteLine(someVariable++);
            };
         }
         return delegates;
      }
   }
}
