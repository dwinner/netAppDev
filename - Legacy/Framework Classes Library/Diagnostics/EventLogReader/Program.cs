/**
 * Чтение журнала событий и уведомления о записи в журнал
 */

using System;
using System.Diagnostics.Eventing.Reader;

namespace EventLogReader
{
   internal static class Program
   {
      private static void Main()
      {
         var query = new EventLogQuery("ProCSharpLog", PathType.LogName /*, "query"*/);
         var eventLogWatcher = new EventLogWatcher(query);
         eventLogWatcher.EventRecordWritten += (sender, args) => Console.WriteLine(args.EventRecord);
         eventLogWatcher.Enabled = true;

         Console.ReadKey();
      }
   }
}