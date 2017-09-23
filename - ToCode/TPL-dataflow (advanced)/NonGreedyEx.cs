using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace LinkingBlocks
{
    class Program
    {
        static void Main(string[] args)
        {           
            var greedy = new ExecutionDataflowBlockOptions();

            var nonGreedy = new ExecutionDataflowBlockOptions()
                {
                    BoundedCapacity = 1
                };

            ExecutionDataflowBlockOptions options = nonGreedy;

            var firstBlock = new ActionBlock<int>(i => Do(i,1,2),options);
            var secondBlock = new ActionBlock<int>(i => Do(i,2,1), options);
            var thirdBlock = new ActionBlock<int>(i => Do(i,3,2), options);

            var transform = new TransformBlock<int,int>(i=>i*2);

           transform.LinkTo(firstBlock);
           transform.LinkTo(secondBlock);
           transform.LinkTo(thirdBlock);

            for (int i = 0; i <= 10; i++)
            {
                transform.Post(i);
            }

            Console.ReadLine();
        }

        private static void Do(int workItem , int nWorker, int busyTimeInSeconds)
        {
            Console.WriteLine("Worker {0} Busy Processing {1}",nWorker,workItem);
            Thread.Sleep(busyTimeInSeconds * 1000 );
            Console.WriteLine("Worker {0} Done",nWorker);
        }
    }
}
