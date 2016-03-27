/**
 * 1) Вызов UAC для запрашивания прав администратора.
 * 2) Демонстрация чтение/записи в журнал событий
 */

using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace EventLogDemo
{
   static class Program
   {
      public const string LogName = "CSharpHowToLog";
      // Если вы всего лишь хотите записать свое сообщение в системный журнал Application, выполните следующее:
      // public const string LogName = "Application";
      public const string LogSource = "EventLogDemo";

      [STAThread]
      static void Main()
      {
         if (CheckUacRequest())
         {
            // Показывать элементы пользовательского интерфейса не нужно;
            // они уже выведены. Поэтому просто завершяем процесс
            return;
         }

         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new Form1());
      }

      private static bool CheckUacRequest()
      {
         var args = Environment.GetCommandLineArgs();
         if (args.All(arg => string.CompareOrdinal("-createEventSource", arg) != 0))
         {
            return false;
         }

         CreateEventSource();
         return true;
      }

      private static void CreateEventSource()   // NOTE: Процесс должен иметь права администратора, чтобы выполнить эту операцию
      {
         /* Эти действия требуют прав администратора, и вы должны позаботиться
            о повышении прав во время установки приложения, а не на этапе выполнения */
         if (!EventLog.SourceExists(LogSource))
         {
            // Чтобы произвести запись в общий журнал приложения, передайте null в качестве имени приложения
            var data = new EventSourceCreationData(LogSource, LogName);
            EventLog.CreateEventSource(data);
         }
      }
   }
}
