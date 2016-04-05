/**
 * Многомерные массивы
 */

using System;

namespace MultiDimArrays
{
   static class Program
   {
      static void Main()
      {
         // Прямоугольный массив
         var twodim = new int[3, 3];
         twodim[0, 0] = 1;
         twodim[0, 1] = 2;
         twodim[0, 2] = 3;
         twodim[1, 0] = 4;
         twodim[1, 1] = 5;
         twodim[1, 2] = 6;
         twodim[2, 0] = 7;
         twodim[2, 1] = 8;
         twodim[2, 2] = 9;
         double dimLength = Math.Sqrt(twodim.Length);
         for (int i = 0; i < dimLength; i++)
         {
            for (int j = 0; j < dimLength; j++)
            {
               Console.WriteLine(twodim[i, j]);
            }
         }

         // 3-х мерный массив
         int[, ,] threedim =
         {
            { { 1, 2 }, { 3, 4 } },
            { { 5, 6 }, { 7, 8 } },
            { { 9, 10 }, { 11, 12 } }
         };
         Console.WriteLine(threedim[0, 1, 1]);

         // Зубчатый массив
         var jagged = new int[3][];
         jagged[0] = new[] { 1, 2 };
         jagged[1] = new[] { 3, 4, 5, 6, 7, 8 };
         jagged[2] = new[] { 9, 10, 11 };

         for (int i = 0; i < jagged.Length; i++)
         {
            for (int j = 0; j < jagged[i].Length; j++)
            {
               Console.WriteLine("Row: {0}, Element: {1}, Value: {2}", i, j, jagged[i][j]);
            }
         }
      }
   }
}
