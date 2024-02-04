using System.Diagnostics.Tracing;

namespace CpuInefficient;

[EventSource(Name = "My-Subarray-MaxUpdated")]
public sealed class MyEventCounterSource : EventSource
{
   public static readonly MyEventCounterSource Log = new();
   private readonly EventCounter _requestCounter;

   private MyEventCounterSource() : base(EventSourceSettings.EtwSelfDescribingEventFormat) =>
      _requestCounter = new EventCounter("Highest sum updated", this);

   public void Request(int currentMax, float elapsedMSec)
   {
      WriteEvent(1, currentMax, elapsedMSec);
      _requestCounter.WriteMetric(elapsedMSec);
   }
}