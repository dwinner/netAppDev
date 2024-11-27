using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreadRand
{
   internal static class Program
   {
      private static Random _Rand = new Random();

      [ThreadStatic] private static Random _ThreadStaticRand;
      private static readonly ThreadLocal<Random> _ThreadLocalRand = new ThreadLocal<Random>(() => new Random());

      private static void Main()
      {
         var results = new int[100];

         Console.WriteLine("ThreadLocal version");
         Parallel.For(0, 5_000,
            i =>
            {
               var randomNumber = _ThreadLocalRand.Value.Next(100);
               Interlocked.Increment(ref results[randomNumber]);
            });


         Console.WriteLine("ThreadStatic Version");
         Parallel.For(0, 5_000,
            i =>
            {
               // thread statics are not initialized
               if (_ThreadStaticRand == null)
               {
                  _ThreadStaticRand = new Random();
               }

               var randomNumber = _ThreadStaticRand.Next(100);
               Interlocked.Increment(ref results[randomNumber]);
            });

         PrintHistogram(results);
      }

      private static void PrintHistogram(int[] results)
      {
         for (var i = 0; i < results.Length / 10; i++)
         {
            var sum = 0;
            for (var j = i * 10; j < (i + 1) * 10; j++)
            {
               sum += results[j];
            }

            Console.Write("{0:D2}-{1:D3}: ", i * 10, (i + 1) * 10);
            for (var j = 0; j < sum / 10; j++)
            {
               Console.Write("#");
            }

            Console.WriteLine();
         }
      }
   }
}