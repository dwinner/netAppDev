using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Text;

namespace EtlDemo
{
   internal class FileListener : BaseListener
   {
      private readonly StreamWriter _writer;

      public FileListener(IEnumerable<SourceConfig> sources, string outputFile)
         : base(sources) =>
         _writer = new StreamWriter(outputFile);

      protected override void WriteEvent(EventWrittenEventArgs eventData)
      {
         var output = new StringBuilder();
         var time = DateTime.Now;
         output.AppendFormat("{0:yyyy-MM-dd-HH:mm:ss.fff} - {1} - ", time, eventData.Level);
         switch (eventData.EventId)
         {
            case Events.ProcessingStartId:
               output.Append("ProcessingStart");
               break;
            case Events.ProcessingFinishId:
               output.Append("ProcessingFinish");
               break;
            case Events.FoundPrimeId:
               output.AppendFormat("FoundPrime - {0:N0}", eventData.Payload[0]);
               break;
            default:
               throw new InvalidOperationException("Unknown event");
         }

         _writer.WriteLine(output.ToString());
      }

      public override void Dispose()
      {
         _writer.Close();
         base.Dispose();
      }
   }
}