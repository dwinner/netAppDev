using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterlockedVsLock
{

    public class LockTest
    {
        const int ThreadCount = 8;
        const int TargetCount = 1000000;
        private readonly object syncObject = new object();
        private int accumulatorLock = 0;
        private int accumulatorInterlocked = 0;

        [IterationSetup]
        public void ResetAccumulators()
        {
            accumulatorLock = 0;
            accumulatorInterlocked = 0;
        }
                
        [Benchmark]

        public void LockPerf()
        {
            var action = new Action(() =>
            {
                bool quit = false;
                while (!quit)
                {
                    lock (syncObject)
                    {
                        if (++accumulatorLock >= TargetCount)
                        {
                            quit = true;
                        }
                    }
                }
            });
            TestPerf(action);
        }

        [Benchmark]
        public void InterlockedPerf()
        {
            var action = new Action(() =>
            {
                bool quit = false;
                while (!quit)
                {
                    if (Interlocked.Increment(ref accumulatorInterlocked) > TargetCount)
                    {
                        quit = true;
                    }                    
                }
            });
            TestPerf(action);
        }

        public void TestPerf(Action action)
        {
            var tasks = new Task[ThreadCount];
            for(int i=0;i < tasks.Length;i++)
            {
                tasks[i] = Task.Run(action);
            }
            Task.WaitAll(tasks);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<LockTest>();
        }
    }
}
