using System.Threading;

namespace CsCsLang
{
   internal class SleepFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var sleepms = Utils.GetItem(script);
         Utils.CheckPosInt(sleepms);

         Thread.Sleep((int) sleepms.Value);

         return Variable.EmptyInstance;
      }
   }
}