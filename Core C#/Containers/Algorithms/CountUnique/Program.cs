/**
 * Отбор уникальных элементов
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace GetUnique
{
   internal static class Program
   {
      private static void Main()
      {
         var list = new List<int>();
         list.AddRange(new[] {1, 1, 1, 2, 3, 4, 7, 7, 7, 7, 7, 7, 7, 5, 5, 10, 8, 8});
         Console.Write("Original: ");
         PrintCollection(list);
         var uniqueList = GetUniques(list);
         Console.Write("Uniques: ");
         PrintCollection(uniqueList);
         CountUniques(list);
         Console.ReadKey();
      }

      private static ICollection<T> GetUniques<T>(IEnumerable<T> list)
      {
         var found = new Dictionary<T, bool>(); // Для отслеживания элементов используем словарь
         var uniques = new List<T>();
         foreach (var currentValue in list.Where(val => !found.ContainsKey(val)))
            // Этот алгоритм сохраняет оригинальный порядок элементов
         {
            found[currentValue] = true;
            uniques.Add(currentValue);
         }
         return uniques;
      }

      private static void CountUniques<T>(IEnumerable<T> coll)
      {
         var counts = new Dictionary<T, int>();

         foreach (var val in coll)
         {
            if (counts.ContainsKey(val))
               counts[val]++;
            else
            {
               counts[val] = 1;
            }
         }
         foreach (var pair in counts)
         {
            Console.WriteLine("{0} appears {1} time(s)", pair.Key, pair.Value);
         }
      }

      private static void PrintCollection<T>(IEnumerable<T> list)
      {
         foreach (var val in list)
         {
            Console.Write("{0} ", val);
         }
         Console.WriteLine();
      }
   }
}