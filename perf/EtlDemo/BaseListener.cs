using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace EtlDemo
{
   internal abstract class BaseListener : EventListener
   {
      private readonly List<SourceConfig> _configs = new List<SourceConfig>();

      protected BaseListener(IEnumerable<SourceConfig> sources)
      {
         _configs.AddRange(sources);

         foreach (var source in _configs)
         {
            var eventSource = FindEventSource(source.Name);
            if (eventSource != null)
            {
               EnableEvents(eventSource, source.Level, source.Keywords);
            }
         }
      }

      protected override void OnEventSourceCreated(EventSource eventSource)
      {
         base.OnEventSourceCreated(eventSource);

         foreach (var source in _configs)
         {
            if (string.Equals(source.Name, eventSource.Name))
            {
               EnableEvents(eventSource, source.Level, source.Keywords);
            }
         }
      }

      protected override void OnEventWritten(EventWrittenEventArgs eventData)
      {
         WriteEvent(eventData);
      }

      private static EventSource FindEventSource(string name)
      {
         foreach (var eventSource in EventSource.GetSources())
         {
            if (string.Equals(eventSource.Name, name))
            {
               return eventSource;
            }
         }

         return null;
      }

      protected abstract void WriteEvent(EventWrittenEventArgs eventData);
   }
}