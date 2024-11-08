using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreadRand
{
    class Program
    {
        static Random rand = new Random();

        [ThreadStatic]
        static Random threadStaticRand;

        private static readonly ThreadLocal<Random> threadLocalRand = new ThreadLocal<Random>(()=>new Random());

        static void Main(string[] args)
        {
            int[] results = new int[100];

            Console.WriteLine("ThreadLocal version");
            Parallel.For(0, 5000,
                i =>
                {
                    var randomNumber = threadLocalRand.Value.Next(100);
                    Interlocked.Increment(ref results[randomNumber]);
                });


            Console.WriteLine("ThreadStatic Version");
            Parallel.For(0, 5000,
                i =>
                {
                    // thread statics are not initialized
                    if (threadStaticRand == null) threadStaticRand = new Random();
                    var randomNumber = threadStaticRand.Next(100);
                    Interlocked.Increment(ref results[randomNumber]);
                });
            
            PrintHistogram(results);
        }

        private static void PrintHistogram(int[] results)
        {
            for (int i = 0; i < results.Length / 10; i++)
            {
                int sum = 0;
                for (int j = i * 10; j < ((i + 1) * 10); j++)
                {
                    sum += results[j];
                }
                Console.Write("{0:D2}-{1:D3}: ",i*10,(i+1)*10);
                for (int j = 0; j < sum / 10; j++)
                {
                    Console.Write("#");
                }
                Console.WriteLine();
            }
        }        
    }
}
