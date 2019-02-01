/**
 * Корреляция между методами при трассировке
 */

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TraceCorrelation
{
   public class Program
   {
      internal static SourceSwitch TraceSwitch = new SourceSwitch("Wrox.ProCSharp.Instrumentation")
      {
         Level = SourceLevels.Verbose
      };

      internal static TraceSource WroxTraceSource = new TraceSource("Wrox.ProCSharp.Instrumentation");

      static void Main()
      {
         // Запустить новое действие
         if (Trace.CorrelationManager.ActivityId == Guid.Empty)
         {
            Guid newGuid = Guid.NewGuid();
            Trace.CorrelationManager.ActivityId = newGuid;
         }

         WroxTraceSource.TraceEvent(TraceEventType.Start, 0, "Main started");
         Trace.CorrelationManager.StartLogicalOperation("Main");  // Запустить логическую операцию

         TraceSourceDemo1();
         StartActivityA();

         Trace.CorrelationManager.StopLogicalOperation();
         Thread.Sleep(3000);
         WroxTraceSource.TraceEvent(TraceEventType.Stop, 0, "Main stopped");
      }

      private static void StartActivityA()
      {
         Guid oldGuid = Trace.CorrelationManager.ActivityId;
         Guid newActivityId = Guid.NewGuid();
         Trace.CorrelationManager.ActivityId = newActivityId;

         Trace.CorrelationManager.StartLogicalOperation("StartActivityA");
         WroxTraceSource.TraceEvent(TraceEventType.Verbose, 0, "starting Foo in StartNewActivity");
         Foo();

         WroxTraceSource.TraceEvent(TraceEventType.Verbose, 0, "starting a new task");
         Task.Factory.StartNew(WorkForATask);

         Trace.CorrelationManager.StopLogicalOperation();
         Trace.CorrelationManager.ActivityId = oldGuid;
      }

      private static void WorkForATask()
      {
         WroxTraceSource.TraceEvent(TraceEventType.Start, 0, "WorkForATask started");
         WroxTraceSource.TraceEvent(TraceEventType.Verbose, 0, "running WorkForATask");
         WroxTraceSource.TraceEvent(TraceEventType.Stop, 0, "WorkForATask completed");
      }

      private static void Foo()
      {
         Trace.CorrelationManager.StartLogicalOperation("Foo operation");
         WroxTraceSource.TraceEvent(TraceEventType.Verbose, 0, "running Foo");
         Trace.CorrelationManager.StopLogicalOperation();
      }

      private static void TraceSourceDemo1()
      {
         WroxTraceSource.TraceInformation("Info message");
         WroxTraceSource.TraceEvent(TraceEventType.Error, 3, "Error message");
         WroxTraceSource.TraceData(TraceEventType.Information, 2, "data1", 4, 5);
         WroxTraceSource.Flush();
         WroxTraceSource.Close();
      }
   }
}
