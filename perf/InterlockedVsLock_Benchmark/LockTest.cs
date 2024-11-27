using System;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace InterlockedVsLock_Benchmark;

public class LockTest
{
   private const int ThreadCount = 8;
   private const int TargetCount = 1_000_000;
   private readonly object _syncObject = new();
   private int _accumulatorInterlocked;
   private int _accumulatorLock;

   [IterationSetup]
   public void ResetAccumulators()
   {
      _accumulatorLock = 0;
      _accumulatorInterlocked = 0;
   }

   [Benchmark /*(Baseline = true)*/]
   public void LockPerf()
   {
      var action = new Action(() =>
      {
         var quit = false;
         while (!quit)
         {
            lock (_syncObject)
            {
               if (++_accumulatorLock >= TargetCount)
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
         var quit = false;
         while (!quit)
         {
            if (Interlocked.Increment(ref _accumulatorInterlocked) > TargetCount)
            {
               quit = true;
            }
         }
      });
      TestPerf(action);
   }

   private static void TestPerf(Action action)
   {
      var tasks = new Task[ThreadCount];
      for (var i = 0; i < tasks.Length; i++)
      {
         tasks[i] = Task.Run(action);
      }

      Task.WaitAll(tasks);
   }
}