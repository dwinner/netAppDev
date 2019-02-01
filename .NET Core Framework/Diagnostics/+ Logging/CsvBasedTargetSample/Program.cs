using NlogTester.Lib;
using NLog;

namespace CsvBasedTargetSample
{
   internal static class Program
   {
      private const string NlogName = "csv";

      private static void Main()
      {
         var logger = LogManager.GetLogger(NlogName);
         LogUtils.Log(logger);
      }
   }
}