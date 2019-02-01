// Запрет многопоточного доступа

using System;
using System.Threading.Tasks;
using PostSharp.Patterns.Threading;

namespace ControllingThreadUnsafeMark
{
   internal static class Program
   {
      private static void Main()
      {
         var averageCalculator = new AverageCalculator();
         var tasks = new Task[10];

         for (var i = 0; i < tasks.Length; i++)
         {
            try
            {
               var locali = i;
               tasks[i] = Task.Factory.StartNew(() => { averageCalculator.AddSample(locali); });
            }
            catch (ConcurrentAccessException concurrentAccessEx)
            {
               Console.WriteLine(concurrentAccessEx.Message);
            }
         }

         try
         {
            Task.WaitAll(tasks);
         }
         catch (AggregateException aggregateEx)
         {
            Console.WriteLine(aggregateEx.Message);
         }
      }
   }

   [ThreadUnsafe]
   public class AverageCalculator
   {
      private int _count;
      private float _sum;

      public void AddSample(float n)
      {
         _count++;
         _sum += n;
      }

      public float GetAverage() => _sum / _count;
   }
}