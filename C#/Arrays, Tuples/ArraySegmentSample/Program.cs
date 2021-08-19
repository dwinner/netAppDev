/**
 * Сегменты массива (Note: могут эффективно применяться при обходах массива параллельным образом)
 */

using System;
using System.Collections.Generic;

namespace ArraySegmentSample
{
   static class Program
   {
      static void Main()
      {
         int[] firstArray = { 1, 4, 5, 11, 13, 18 };
         int[] secondArray = { 3, 4, 5, 18, 21, 27, 33 };

         var segments = new[]
         {
            new ArraySegment<int>(firstArray, 0, 3),
            new ArraySegment<int>(secondArray, 3, 3)
         };

         int sumOfSegments = SumOfSegments(segments);
         Console.WriteLine(sumOfSegments);
      }

      static int SumOfSegments(IEnumerable<ArraySegment<int>> segments)
      {
         int sum = 0;
         foreach (var segment in segments)
         {
            for (int i = segment.Offset; i < segment.Offset + segment.Count; i++)
            {
               sum += segment.Array[i];
            }
         }
         
         return sum;
      }
   }
}
