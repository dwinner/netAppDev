using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Session;
using System;
using Microsoft.Diagnostics.Tracing.Parsers.Clr;

namespace GCListener
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press Ctrl-C to exit");

            using (var session = new TraceEventSession("GCListenSession"))
            {
                Console.CancelKeyPress += (a,b) => session.Stop();
                session.EnableProvider(
                    ClrTraceEventParser.ProviderGuid, 
                    Microsoft.Diagnostics.Tracing.TraceEventLevel.Informational, 
                    (ulong)(ClrTraceEventParser.Keywords.GC));

                session.Source.Clr.GCStart += Clr_GCStart;
                session.Source.Clr.GCStop += Clr_GCStop;

                // This will run until session.Stop is called
                session.Source.Process();                
            }
        }

        private static void Clr_GCStart(Microsoft.Diagnostics.Tracing.Parsers.Clr.GCStartTraceData gcStartData)
        {
            Console.WriteLine($"GCStart: Process: {gcStartData.ProcessID}, Gen: {gcStartData.Depth}, Type: {gcStartData.Type}");
        }

        private static void Clr_GCStop(GCEndTraceData gcEndData)
        {
            Console.WriteLine($"GCStop: Process: {gcEndData.ProcessID}, Gen: {gcEndData.Depth}");
        }
    }
}
