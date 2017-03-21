using ProgramVerificationSystems.SelfTester.Model.Poco;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Xml.Linq;

namespace ProgramVerificationSystems.SelfTester.Model.Utils
{
   /// <summary>
   ///    Утилиты для диагностики поведения devenv.exe
   /// </summary>
   public static class AppEventLogger
   {
      private const string SelfTesterEventLogName = "SelfTesterAppLog";
      private const string SelfTesterSourceName = "SelfTesterAppSource";
      private static readonly bool SelfTesterSourceExists;

      static AppEventLogger()
      {
         if (IsAdministrator == true)
         {
            SelfTesterSourceExists = EventLog.SourceExists(SelfTesterSourceName);
            if (!SelfTesterSourceExists)
            {
               var sourceCreationData = new EventSourceCreationData(SelfTesterSourceName, SelfTesterEventLogName);
               EventLog.CreateEventSource(sourceCreationData);
               SelfTesterSourceExists = true;
            }
            else
            {
               using (var testerEventLog = new EventLog(SelfTesterEventLogName, ".", SelfTesterSourceName))
               {
                  testerEventLog.Clear();
               }
            }
         }
         else
         {
            SelfTesterSourceExists = false;
         }
      }

      /// <summary>
      ///    Запущен ли пользователь под учетной записью администратора
      /// </summary>
      /// <value>true - администратор, false - не администратор, null - среда выполнения не поддерживает принципалы Windows</value>
      private static bool? IsAdministrator
      {
         get
         {
            try
            {
               var identity = WindowsIdentity.GetCurrent();

               if (identity != null)
               {
                  var principal = new WindowsPrincipal(identity);
                  return principal.IsInRole(WindowsBuiltInRole.Administrator);
               }

               return null;
            }
            catch (SecurityException)
            {
               return null;
            }
         }
      }

      /// <summary>
      ///    Запись сообщения в системный журнал событий
      /// </summary>
      /// <param name="message">Сообщение</param>
      /// <param name="entryType">Тип сообщения</param>
      /// <param name="line">Номер строки в файле источника</param>
      /// <param name="path">Путь к файлу вызывающего компонента</param>
      /// <param name="name">Имя вызывающего члена</param>
      public static void Log(string message, EventLogEntryType entryType = EventLogEntryType.Information,
         [CallerLineNumber] int line = -1,
         [CallerFilePath] string path = null,
         [CallerMemberName] string name = null)
      {
         if (!SelfTesterSourceExists)
            return;

         var messageBuilder = new StringBuilder();
         messageBuilder
            .AppendLine(string.Format("Source line: {0}", (line != -1 ? line.ToString() : "No Line Information.")))
            .AppendLine(string.Format("Source path: {0}", (path ?? "No source path")))
            .AppendLine(string.Format("Source member: {0}", (name ?? "No calling member")))
            .AppendLine()
            .AppendLine(string.Format("Message: {0}", message));
         using (var selfTesterEventLog = new EventLog(SelfTesterEventLogName, ".", SelfTesterSourceName))
         {
            selfTesterEventLog.WriteEntry(messageBuilder.ToString(), entryType);
         }
      }

      /// <summary>
      ///    Записывает сообщение в системный журнал событий
      /// </summary>
      /// <param name="totalElapsed">Время потраченное на анализ</param>
      /// <param name="startup">Версия VS для запуска</param>
      /// <param name="testSolutionInfo">Сущность для тестирования</param>
      /// <param name="entryType">Тип сообщения</param>
      public static void Log(TimeSpan totalElapsed, StartupConfigurationMode startup, TestSolutionInfo testSolutionInfo,
         EventLogEntryType entryType = EventLogEntryType.Information)
      {
         if (!SelfTesterSourceExists)
            return;

         var logDocument = new XDocument();
         var rootElement = new XElement("Root",
            new XElement("Elapsed", totalElapsed.ToString(@"hh\:mm\:ss")),
            new XElement("StartupMode", startup.ToString()),
            new XElement("SolutionInfo",
               new XElement("Solution", testSolutionInfo.AbsSolutionFileName),
               new XElement("ConfigMode", testSolutionInfo.ConfigurationMode.ToString()),
               new XElement("Platform", testSolutionInfo.Platform),
               new XElement("PreprocessorType", testSolutionInfo.PreprocessorType)));

         logDocument.Add(rootElement);
         using (var selfTesterEventLog = new EventLog(SelfTesterEventLogName, ".", SelfTesterSourceName))
         {
            selfTesterEventLog.WriteEntry(logDocument.ToString(), entryType);
         }
      }
   }
}