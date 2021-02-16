/**
 * Запись в журналы событий, использующая ресурсные файлы
 */

using System;
using System.Diagnostics;
using System.IO;

namespace AdvancedEventSource
{
   static class Program
   {
      private static string _logName = "ProCSharpLog";
      private const string SourceName = "EventLogDemoApp";
      private const string ResourceFile = "Externals/EventLogDemoMessages.dll";

      static void Main()
      {
         if (!EventLog.SourceExists(SourceName))
         {
            if (!File.Exists(ResourceFile))
            {
               // Ресурсный файл сообщений не существует.
               Console.WriteLine("Message resource file does not exists");
               return;
            }

            var eventSource = new EventSourceCreationData(SourceName, _logName)
            {
               CategoryResourceFile = ResourceFile,
               CategoryCount = 4,
               MessageResourceFile = ResourceFile,
               ParameterResourceFile = ResourceFile
            };

            EventLog.CreateEventSource(eventSource);
         }
         else
         {
            _logName = EventLog.LogNameFromSourceName(SourceName, ".");
         }

         var eventLog = new EventLog(_logName, ".", SourceName);
         eventLog.RegisterDisplayName(ResourceFile, 5001);

         using (var log = new EventLog(_logName, ".", SourceName))
         {
            var info1 = new EventInstance(1000, 4, EventLogEntryType.Information);
            log.WriteEvent(info1);

            var info2 = new EventInstance(1001, 4, EventLogEntryType.Error);
            log.WriteEvent(info2, "avalon");

            var info3 = new EventInstance(1002, 3, EventLogEntryType.Error);
            byte[] additionalInfo = { 1, 2, 3 };
            log.WriteEvent(info3, additionalInfo);
         }
      }
   }
}
