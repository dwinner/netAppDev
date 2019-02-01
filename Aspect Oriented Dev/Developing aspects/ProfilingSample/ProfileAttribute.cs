namespace ProfilingSample
{
   using System;
   using System.Diagnostics;

   using PostSharp.Aspects;
   using PostSharp.Serialization;

   using static NativeMethods;

   [PSerializable]
   public sealed class ProfileAttribute : OnMethodBoundaryAspect
   {
      public override void OnEntry(MethodExecutionArgs args)
      {
         var performanceData = new PerformanceData();
         performanceData.Start();
         args.MethodExecutionTag = performanceData;
      }

      public override void OnExit(MethodExecutionArgs args)
      {
         var performanceData = (PerformanceData) args.MethodExecutionTag;
         performanceData.Stop();
         if (args.Method.DeclaringType != null)
         {
            Console.WriteLine("{0}.{1} - Wall time {2} ms; Synchronous time {3} ms; CPU time: {4} ms",
                              args.Method.DeclaringType.Name, args.Method.Name,
                              performanceData.WallTime.TotalMilliseconds,
                              performanceData.SynchronousTime.TotalMilliseconds,
                              performanceData.UserTime.TotalMilliseconds + performanceData.KernelTime.TotalMilliseconds);
         }
      }

      public override void OnYield(MethodExecutionArgs args)
      {
         var performanceData = (PerformanceData) args.MethodExecutionTag;
         performanceData.Pause();
      }

      public override void OnResume(MethodExecutionArgs args)
      {
         var performanceData = (PerformanceData) args.MethodExecutionTag;
         performanceData.Resume();
      }

      private sealed class PerformanceData
      {
         private static readonly Stopwatch _Stopwatch = Stopwatch.StartNew();
         private ulong _kernelTime;
         private ulong _kernelTimestamp;
         private long _syncTime;
         private long _syncTimestamp;
         private ulong _userTime;
         private ulong _userTimestamp;
         private long _wallTime;
         private long _wallTimestamp;

         public TimeSpan WallTime => TimeSpan.FromSeconds((double) _wallTime / Stopwatch.Frequency);

         public TimeSpan SynchronousTime => TimeSpan.FromSeconds((double) _syncTime / Stopwatch.Frequency);

         public TimeSpan KernelTime => TimeSpan.FromMilliseconds(_kernelTime * 10E-4);

         public TimeSpan UserTime => TimeSpan.FromMilliseconds(_userTime * 10E-4);

         public void Start()
         {
            _wallTimestamp = _Stopwatch.ElapsedTicks;
            Resume();
         }

         public void Resume()
         {
            _syncTimestamp = _Stopwatch.ElapsedTicks;
            ulong creationTime, exitTime;
            GetThreadTimes(GetCurrentThread(), out creationTime, out exitTime, out _kernelTimestamp, out _userTimestamp);
         }

         public void Stop()
         {
            Pause();
            _wallTime += _Stopwatch.ElapsedTicks - _wallTimestamp;
         }

         public void Pause()
         {
            ulong creationTime, exitTime, kernelTime, userTime;
            GetThreadTimes(GetCurrentThread(), out creationTime, out exitTime, out kernelTime, out userTime);
            _kernelTime += kernelTime - _kernelTimestamp;
            _userTime += userTime - _userTimestamp;
            _syncTime += _Stopwatch.ElapsedTicks - _syncTimestamp;
         }
      }
   }
}