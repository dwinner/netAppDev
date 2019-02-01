/**
 * Исключения, сгенерированные в статических конструкторах
 */

using System.IO;

namespace _06_ExceptionsInClassInitializers
{
   class Program
   {
      static void Main(string[] args)
      {
         EventLogger.WriteLog("Зарегистрировать это");
      }
   }

   class EventLogger
   {
      private static StreamWriter eventLog;
      private static string strLogName;

      static EventLogger()
      {
         eventLog = File.CreateText("logFile.txt");
         // Следующмй оператор сгенерирует исключение
         strLogName = (string)strLogName.Clone();
      }

      public static void WriteLog(string someText)
      {
         eventLog.Write(someText);
      }
   }
}
