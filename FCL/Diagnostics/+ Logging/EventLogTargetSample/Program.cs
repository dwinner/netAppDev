using NlogTester.Lib;
using NLog;

namespace EventLogTargetSample
{
   internal static class Program
   {
      private const string NlogName = "eventlog";

      private static void Main()
      {
         var eventLogger = LogManager.GetLogger(NlogName);
         LogUtils.Log(eventLogger);
      }
   }
}