/**
 * Создание простого источника событий
 */

using System.Diagnostics;
using System.Security;

namespace SimpleEventSource
{
   class Program
   {
      private const string LogName = "ProCSharpLog";
      private const string SourceName = "EventLogDemoApp";

      static void Main()
      {
         // Создаем источник событий
         bool eventSourceExists;
         try
         {
            eventSourceExists = EventLog.SourceExists(SourceName);
         }
         catch (SecurityException)
         {
            eventSourceExists = false;
         }

         if (!eventSourceExists)
         {
            var eventSourceCreationData = new EventSourceCreationData(SourceName, LogName);
            EventLog.CreateEventSource(eventSourceCreationData);
         }

         // Записываем в журнал
         using (var log = new EventLog(LogName, ".", SourceName))
         {
            log.WriteEntry("Message 1");
            log.WriteEntry("Message 2", EventLogEntryType.Warning);
            log.WriteEntry("Message 3", EventLogEntryType.Information, 33);
         }
      }
   }
}
