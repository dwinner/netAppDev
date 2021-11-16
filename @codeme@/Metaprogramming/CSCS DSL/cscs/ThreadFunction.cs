using System.Threading;

namespace CsCsLang
{
   internal class ThreadFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var body = Utils.GetBodyBetween(script, Constants.StartArg, Constants.EndArg);

         var newThread = new Thread(ThreadProc);
         newThread.Start(body);

         var threadID = newThread.ManagedThreadId;
         return new Variable(threadID);
      }

      private static void ThreadProc(object stateInfo)
      {
         var body = (string) stateInfo;
         var threadScript = new ParsingScript(body);
         threadScript.ExecuteAll();
      }
   }
}