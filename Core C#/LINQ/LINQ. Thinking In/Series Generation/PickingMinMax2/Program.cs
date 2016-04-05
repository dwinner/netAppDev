// Получение максимальных/минимальных значений из совместимых массивов

using System;
using System.Collections.Generic;
using System.Linq;

namespace PickingMinMax2
{
   internal static class Program
   {
      private static void Main()
      {
         var allValues = new List<List<int>>();

         var bidValues1 = new List<int> { 1, 2, 3, 4, 5 };
         var bidValues2 = new List<int> { 2, 1, 4, 5, 6 };
         var bidValues3 = new List<int> { 4, 0, 6, 8, 1 };
         var bidValues4 = new List<int> { 9, 2, 4, 1, 6 };

         // Объединим все коллекции в одну
         allValues.Add(bidValues1);
         allValues.Add(bidValues2);
         allValues.Add(bidValues3);
         allValues.Add(bidValues4);

         // Вычислим максимальные значения, вычисляя значения в каждой позиции
         var maximums = allValues.Aggregate((firstList, secondList) => firstList.Zip(secondList, Math.Max).ToList());

         // Вычислим минимальные значения, вычисляя значения в каждой позиции
         var minimums = allValues.Aggregate((firstList, secondList) => firstList.Zip(secondList, Math.Min).ToList());

         Console.Write("Maximums: ");
         maximums.ForEach(i => Console.Write("{0} ", i));
         Console.WriteLine();

         Console.Write("Minimums: ");
         minimums.ForEach(i => Console.Write("{0} ", i));
         Console.WriteLine();
      }
   }
}