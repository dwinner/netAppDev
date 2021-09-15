using System.Threading;

namespace CsCsLang
{
   internal class SignalWaitFunction : ParserFunction
   {
      private static readonly AutoResetEvent waitEvent = new AutoResetEvent(false);
      private readonly bool m_isSignal;

      public SignalWaitFunction(bool isSignal) => m_isSignal = isSignal;

      protected override Variable Evaluate(ParsingScript script)
      {
         var result = m_isSignal ? waitEvent.Set() : waitEvent.WaitOne();
         return new Variable(result);
      }
   }
}