// Разбиение последовательности по процентному соотношению

using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace PartitionOperator
{
   internal static class Program
   {
      private static void Main()
      {
         int[] values = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
         int[] percentages = { 30, 60, 10 };
         var numberOfItems = new int[percentages.Length];
         for (var i = 0; i < percentages.Length; i++)
         {
            numberOfItems[i] = (int)Math.Floor((double)values.Length * percentages[i] / 100);
         }

         var distributions = new List<List<int>>();
         for (var i = 0; i < numberOfItems.Length; i++)
         {
            var innerList = new List<int>();
            if (i == 0)
            {
               for (var j = 0; j < numberOfItems[i]; j++)
               {
                  innerList.Add(values[j]);
               }
            }
            else
            {
               var index = 0;
               for (var k = 0; k < i; k++)
               {
                  index += numberOfItems[k];
               }
               for (var j = index; j < index + numberOfItems[i]; j++)
               {
                  innerList.Add(values[j]);
               }
            }

            distributions.Add(innerList);
         }

         distributions.ForEach((ints, index) =>
         {
            Console.Write("Partition # {0}:\t", index);
            Console.WriteLine(ints.Aggregate(string.Empty, (current, i) => $"{current}{i + " "}"));
         });
      }
   }
}