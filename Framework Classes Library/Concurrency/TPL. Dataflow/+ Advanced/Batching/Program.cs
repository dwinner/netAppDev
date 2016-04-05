// Пакетное выполнение потребляющих узлов потока данных

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks.Dataflow;

namespace Batching
{
   internal static class Program
   {
      private static void Main()
      {
         IntervalBatcher();
         QuantityBatcher();
      }

      /*
            private static void DisplayValues(IEnumerable<int> values)
            {
               foreach (var value in values)
               {
                  Console.WriteLine(value);
               }

               Console.WriteLine();
            }

               private static IEnumerable<int> TimesTable(int input, int delay)
            {
               for (var i = 0; i < 12; i++)
               {
                  yield return i*input;
                  Thread.Sleep(delay);
               }
            }
      */

      #region Пакетное поступление данных

      private static void QuantityBatcher()
      {
         const int batchSize = 100;
         var batcher = new BatchBlock<int>(batchSize);

         var averager =
            new ActionBlock<int[]>(values => Console.WriteLine("{0} reduced to {1}", values.Length, values.Average()));

         batcher.LinkTo(averager);

         Console.WriteLine("Press Q to quit");
         var rnd = new Random();
         bool quit;
         do
         {
            Thread.Sleep(10);
            batcher.Post(rnd.Next(1, 100));
            quit = (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q);
         } while (!quit);

         batcher.Complete();
         batcher.Completion.Wait();
      }

      #endregion

      #region Пакетное поступление данных по регулярным интервалам времени

      private static void IntervalBatcher()
      {
         var batcher = new BatchBlock<int>(int.MaxValue);
         var averager = new ActionBlock<int[]>(values => Console.WriteLine(values.Average()));

         batcher.LinkTo(averager);

         // ReSharper disable UnusedVariable
         var timer = new Timer(_ => batcher.TriggerBatch(), null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
         // ReSharper restore UnusedVariable

         var rnd = new Random();
         while (true)
         {
            batcher.Post(rnd.Next(1, 100));
         }
         // ReSharper disable FunctionNeverReturns
      }

      // ReSharper restore FunctionNeverReturns

      #endregion
   }
}