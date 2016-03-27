/**
 * Многомерные прямоугольные массивы
 */

using System;

namespace _05_DimensionalArrays
{
   class Program
   {
      static void Main(string[] args)
      {
         int[,] twoDim1 = new int[5, 3];
         int[,] twoDim2 = {
                             { 1, 2, 3 },
                             { 4, 5, 6 },
                             { 7, 8, 9 }
                          };
         foreach (int i in twoDim2)
         {
            Console.WriteLine(i);
         }
         Console.ReadKey(true);
      }
   }
}
