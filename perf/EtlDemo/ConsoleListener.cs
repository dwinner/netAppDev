using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace EtlDemo
{
   internal class ConsoleListener : BaseListener
   {
      public ConsoleListener(IEnumerable<SourceConfig> sources)
         : base(sources)
      {
      }

      protected override void WriteEvent(EventWrittenEventArgs eventData)
      {
         string outputString;
         switch (eventData.EventId)
         {
            case Events.ProcessingStartId:
               outputString = $"ProcessingStart ({eventData.EventId.ToString()})";
               break;
            case Events.ProcessingFinishId:
               outputString = $"ProcessingFinish ({eventData.EventId.ToString()})";
               break;
            case Events.FoundPrimeId:
               outputString = $"FoundPrime ({eventData.EventId.ToString()}): {((long)eventData.Payload[0]).ToString()}";
               break;
            case Events.NullStringId:
               outputString = $"NullString ({eventData.EventId.ToString()}): {(string)eventData.Payload[0]}";
               break;
            default:
               throw new InvalidOperationException("Unknown event");
         }

         Console.WriteLine(outputString);
      }
   }
}