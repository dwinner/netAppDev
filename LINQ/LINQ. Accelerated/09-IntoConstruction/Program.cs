/**
 * Конструкция into и продолжение
 */

using System;
using System.Linq;

namespace _09_IntoConstruction
{
   class Program
   {
      static void Main()
      {
         int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

         // Разбиение чисел на четные и нечетные
         var query = from x in numbers
                     group x by x % 2
                        into partition
                        //where partition.Key == 0
                        select new { partition.Key, Count = partition.Count(), Group = partition };
         foreach (var item in query)
         {
            Console.WriteLine("mod2 = {0}", item.Key);
            Console.WriteLine("Count = {0}", item.Count);
            foreach (var number in item.Group)
            {
               Console.Write("{0}, ", number);
            }
            Console.WriteLine(Environment.NewLine);
         }

         Console.ReadKey();
      }
   }
}
