// Эмулирование функциональности F# в C#

using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace FSharpFunctionsEmulation
{
   internal static class Program
   {
      private static void Main()
      {
         const int x = 10;
         var projectorSteps = new List<Func<int, int>> { a => a + 1, a => a + 3, a => a - 4, a => 2 * a - 1 };
         var scannedSteps = x.Scan(projectorSteps);
         scannedSteps.ForEach(item => { Console.Write("{0} ", item); });
         Console.WriteLine();

         var scannedBackSteps = x.ScanBack(projectorSteps);
         scannedBackSteps.ForEach(item => { Console.Write("{0} ", item); });
         Console.WriteLine();

         var manyZipped = FSharpExt.Zip3(
            x.Scan(projectorSteps),
            x.ScanBack(projectorSteps),
            x.Scan(projectorSteps.Concat(projectorSteps.Skip(1))));
         manyZipped.ForEach(
            tripleValue => { Console.Write("{0} {1} {2}", tripleValue.Item1, tripleValue.Item2, tripleValue.Item3); });
         Console.WriteLine();

         x.Scan(projectorSteps).Iterate(a => Console.WriteLine("Score is {0}", a));

         int[] series = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };         
         var isApSeries =
            series.Pairwise()
               .Select(s => new { First = s.Key, Second = s.Value, Difference = s.Value - s.Key })
               .All(s => s.Difference == series[1] - series[0]);
         Console.WriteLine("isAP using Pairwise: {0}", isApSeries);

         var forAll2 = series.ForAll2((a, b) => b - a == series[1] - series[0]);
         Console.WriteLine("isAP using ForAll2: {0}", forAll2);

         var exists2 = series.Exists2((a, b) => a + b >= 100 && a + b <= 200);
         Console.WriteLine("Is there any such couple of elements: {0}", exists2);

         var pairwise = series.Pairwise();
         Console.WriteLine("Items picked Pairwise");
         pairwise.ForEach(pair => { Console.Write("{0} {1}", pair.Key, pair.Value); });
         Console.WriteLine();

         var oppositePartition = series.OppositePartition(a => a % 2 == 0);
         Console.Write("Opposite1: ");
         oppositePartition.Item1.ForEach(item => { Console.Write("{0} ", item); });
         Console.WriteLine();
         Console.Write("Opposite2: ");
         oppositePartition.Item2.ForEach(item => { Console.Write("{0} ", item); });
         Console.WriteLine();

         int[] theseOnes = { 1, 3, 52, 2, 1 };
         int[] thatOnes = { 4, 5, 2, 1, 3, 4 };
         int[] otherOnes = { 2, 3, 1, 1, 3, 14 };
         var intersectMany = new List<int[]> { theseOnes, thatOnes, otherOnes }.IntersectMany();
         Console.WriteLine("Intersect Many");
         intersectMany.ForEach(item => { Console.Write("{0} ", item); });

         var unionMany = new List<int[]> { theseOnes, thatOnes, otherOnes }.UnionMany();
         Console.WriteLine("Union Many");
         unionMany.ForEach(item => { Console.Write("{0} ", item); });
      }
   }
}