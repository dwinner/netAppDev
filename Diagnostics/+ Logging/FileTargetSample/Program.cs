/**
 * Логирование в файл
 */

using NlogTester.Lib;
using NLog;

namespace FileTargetSample
{
   internal static class Program
   {
      private const string NlogName = "file";

      private static void Main()
      {
         var logger = LogManager.GetLogger(NlogName);
         LogUtils.Log(logger);
      }
   }
}