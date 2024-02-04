using System.Diagnostics.Tracing;

namespace MemoryLeakSample;

[EventSource(Name = "My-EventCounter-Minimal")]
public sealed class MyEventCounterSource : EventSource
{
   public static readonly MyEventCounterSource Log = new();
   private readonly EventCounter _requestCounter;

   private MyEventCounterSource() : base(EventSourceSettings.EtwSelfDescribingEventFormat) =>
      _requestCounter = new EventCounter("Sentence Request", this);

   public void Request(string sentence, float elapsedMSec)
   {
      WriteEvent(1, sentence, elapsedMSec);
      _requestCounter.WriteMetric(elapsedMSec);
   }
}