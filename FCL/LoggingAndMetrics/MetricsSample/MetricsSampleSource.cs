using System;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Threading;

namespace MetricsSample
{
   [EventSource(Name = "Wrox.ProCSharp.MetricsSample")]
   internal class MetricsSampleSource : EventSource
   {
      public static readonly MetricsSampleSource Log = new();
      private IncrementingEventCounter? _errorCounter;
      private long _requestDuration;
      private PollingCounter? _requestDurationCounter;

      private IncrementingEventCounter? _totalRequestsCounter;

      private MetricsSampleSource()
         : base("Wrox.ProCSharp.MetricsSample")
      {
      }

      protected override void OnEventCommand(EventCommandEventArgs command)
      {
         switch (command.Command)
         {
            case EventCommand.Enable:
               _totalRequestsCounter ??= new IncrementingEventCounter("requests", this)
               {
                  DisplayName = "Total requests",
                  DisplayUnits = "Count",
                  DisplayRateTimeScale = TimeSpan.FromSeconds(1)
               };
               _errorCounter ??= new IncrementingEventCounter("errors", this)
               {
                  DisplayName = "Errors",
                  DisplayUnits = "Count",
                  DisplayRateTimeScale = TimeSpan.FromSeconds(1)
               };
               _requestDurationCounter ??=
                  new PollingCounter("request-duration", this, () => Interlocked.Read(ref _requestDuration))
                  {
                     DisplayName = "Request duration",
                     DisplayUnits = "ms"
                  };
               break;
         }
      }

      public Stopwatch? RequestStart()
      {
         if (IsEnabled())
         {
            _totalRequestsCounter?.Increment();

            return Stopwatch.StartNew();
         }

         return default;
      }

      public void RequestStop(Stopwatch? stopwatch)
      {
         if (stopwatch?.IsRunning == true)
         {
            stopwatch.Stop();
            Interlocked.Exchange(ref _requestDuration, stopwatch.ElapsedMilliseconds);
         }
      }

      public void Error()
      {
         if (IsEnabled())
         {
            _errorCounter?.Increment();
         }
      }

      protected override void Dispose(bool disposing)
      {
         if (disposing)
         {
            _totalRequestsCounter?.Dispose();
            _totalRequestsCounter = null;
            _errorCounter?.Dispose();
            _errorCounter = null;
            _requestDurationCounter?.Dispose();
            _requestDurationCounter = null;
         }

         base.Dispose(disposing);
      }
   }
}