/**
 * Простой источник трассировки
 */

using System.Diagnostics;

namespace TracingDemo
{
   class Program
   {
      internal static SourceSwitch SourceSwitch = new SourceSwitch("Dwinner.Experience.Diagnostics")
      {
         Level = SourceLevels.Verbose
      };

      internal static TraceSource TraceSource = new TraceSource("Dwinner.Experience.Diagnostics")
      {
         Switch = SourceSwitch
      };

      static void Main()
      {
         TraceSource.TraceInformation("Info message");
         TraceSource.TraceEvent(TraceEventType.Error, 3, "Error message");
         TraceSource.TraceData(TraceEventType.Information, 2, "data1", 4, 5);
         TraceSource.Close();
      }
   }
}
