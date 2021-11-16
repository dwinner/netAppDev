using System.Threading;

namespace CsCsLang
{
   internal class ThreadIDFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var threadID = Thread.CurrentThread.ManagedThreadId;
         return new Variable(threadID.ToString());
      }
   }
}