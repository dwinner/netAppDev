using System;
using System.Diagnostics;

namespace CsCsLang
{
   internal class StopWatchFunction : ParserFunction
   {
      public enum Mode
      {
         START,
         STOP,
         ELAPSED,
         TOTAL_SECS,
         TOTAL_MS
      }

      private static readonly Stopwatch m_stopwatch = new Stopwatch();

      private readonly Mode m_mode;

      public StopWatchFunction(Mode mode) => m_mode = mode;

      protected override Variable Evaluate(ParsingScript script)
      {
         var isList = false;
         var args = Utils.GetArgs(script,
            Constants.StartArg, Constants.EndArg, out isList);

         if (m_mode == Mode.START)
         {
            m_stopwatch.Restart();
            return Variable.EmptyInstance;
         }

         var strFormat = Utils.GetSafeString(args, 0, "secs");
         var elapsedStr = "";
         var elapsed = -1.0;
         if (strFormat == "hh::mm:ss.fff")
            elapsedStr = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}",
               m_stopwatch.Elapsed.Hours, m_stopwatch.Elapsed.Minutes,
               m_stopwatch.Elapsed.Seconds, m_stopwatch.Elapsed.Milliseconds);
         else if (strFormat == "mm:ss.fff")
            elapsedStr = string.Format("{0:D2}:{1:D2}.{2:D3}",
               m_stopwatch.Elapsed.Minutes,
               m_stopwatch.Elapsed.Seconds, m_stopwatch.Elapsed.Milliseconds);
         else if (strFormat == "ss.fff")
            elapsedStr = string.Format("{0:D2}.{1:D3}",
               m_stopwatch.Elapsed.Seconds, m_stopwatch.Elapsed.Milliseconds);
         else if (strFormat == "secs") elapsed = Math.Round(m_stopwatch.Elapsed.TotalSeconds);
         else if (strFormat == "ms") elapsed = Math.Round(m_stopwatch.Elapsed.TotalMilliseconds);

         if (m_mode == Mode.STOP) m_stopwatch.Stop();

         return elapsed >= 0 ? new Variable(elapsed) : new Variable(elapsedStr);
      }
   }
}