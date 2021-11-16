using System.Diagnostics;

namespace CsCsLang
{
   internal class ProcessorTimeFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var pr = Process.GetCurrentProcess();
         var ts = pr.TotalProcessorTime;

         return new Variable(ts.TotalMilliseconds);
      }
   }
}