/**
 * Ускорение запросов
 */

using System;
using System.Linq;
using System.Diagnostics;

namespace PLinqDemo
{
   class Program
   {
      static void Main()
      {         
         var data = Enumerable.Range(1, 1000000);

         Stopwatch timer = new Stopwatch();
         timer.Start();

         var enumerable = data as int[] ?? data.ToArray();
         var query = from val in enumerable
                     where ComplexCriteria(val)
                     select val;
         int numFound = query.Count();

         timer.Stop();
         Console.WriteLine("LINQ: Found {0} results in {1}", numFound, timer.Elapsed);

         timer.Reset();
         timer.Start();

         // Note: Потенциальное ускорение запроса за счет использования параллельности
         query = from val in enumerable.AsParallel()/*.AsOrdered()*/
                 where ComplexCriteria(val)
                 select val;
         numFound = query.Count();
         timer.Stop();
         Console.WriteLine("PLINQ: Found {0} results in {1}",
            numFound, timer.Elapsed);

         Console.ReadKey();
      }

      private static bool ComplexCriteria(int val)
      {         
         Int64 sum = 0;
         for (int i = 0; i < val; ++i)
         {
            sum += val;
         }
         return sum % 2 == 0;
      }
   }
}
