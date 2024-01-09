/*
 * Многомерные прямоугольные массивы
 */

using System;

namespace _05_DimensionalArrays
{
   internal static class Program
   {
      private static void Main()
      {
         var twoDim1 = new int[5, 3];
         int[,] twoDim2 =
         {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
         };
         foreach (var i in twoDim2)
         {
            Console.WriteLine(i);
         }

         Console.ReadKey(true);
      }

      private static void Rectangular()
      {
         var matrix = new int[3, 3]; // 2-dimensional rectangular array

         // The GetLength method of an array returns the length for a given dimension (starting at 0):

         for (var i = 0; i < matrix.GetLength(0); i++)
         for (var j = 0; j < matrix.GetLength(1); j++)
         {
            matrix[i, j] = i * 3 + j;
         }

         Console.WriteLine(matrix);

         // A rectangular array can be initialized as follows:

         int[,] matrix2 =
         {
            { 0, 1, 2 },
            { 3, 4, 5 },
            { 6, 7, 8 }
         };

         Console.WriteLine(matrix2);
      }

      private static void Jagged()
      {
         var matrix = new int[3][];

         // The inner dimensions aren’t specified in the declaration. Unlike a rectangular array,
         // each inner array can be an arbitrary length. Each inner array is implicitly initialized
         // to null rather than an empty array. Each inner array must be created manually:

         for (var i = 0; i < matrix.Length; i++)
         {
            matrix[i] = new int[3]; // Create inner array
            for (var j = 0; j < matrix[i].Length; j++)
            {
               matrix[i][j] = i * 3 + j;
            }
         }

         Console.WriteLine(matrix);

         // A jagged array can be initialized as follows:

         int[][] matrix2 =
         {
            new[] { 0, 1, 2 },
            new[] { 3, 4, 5 },
            new[] { 6, 7, 8, 9 }
         };

         Console.WriteLine(matrix2);
      }
   }
}