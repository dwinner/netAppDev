/**
 * Реагирование на изменение системной конфигурации
 */

using System;
using Microsoft.Win32;

namespace SystemEventsSample
{
   internal static class Program
   {
      private static void Main()
      {
         SystemEvents.DisplaySettingsChanged +=
            (sender, args) => Console.WriteLine("Пользователь изменил параметры дисплеев");

         SystemEvents.InstalledFontsChanged +=
            (sender, args) => Console.WriteLine("Пользователь добавил или удалил новые шрифты в систему");

         SystemEvents.PaletteChanged +=
            (sender, args) => Console.WriteLine("Пользователь перешел к приложению, которое использует другую палитру");

         SystemEvents.PowerModeChanged += (sender, args) =>
         {
            string statusMessage;

            switch (args.Mode)
            {
               case PowerModes.Resume:
                  statusMessage = "ОС готова выйти из приостановленного состояния";
                  break;

               case PowerModes.StatusChange:
                  statusMessage = "Изменение режима питания";
                  break;

               case PowerModes.Suspend:
                  statusMessage = "ОС готова приостановить работу";
                  break;

               default:
                  statusMessage = string.Empty;
                  break;
            }

            Console.WriteLine(statusMessage);
         };

         SystemEvents.SessionEnded += (sender, args) =>
         {
            string statusMessage;

            switch (args.Reason)
            {
               case SessionEndReasons.Logoff:
                  statusMessage = "Пользователь выходит из системы";
                  break;

               case SessionEndReasons.SystemShutdown:
                  statusMessage = "ОС завершает работу";
                  break;

               default:
                  statusMessage = string.Empty;
                  break;
            }

            Console.WriteLine(statusMessage);
         };

         SystemEvents.SessionEnding += (sender, args) =>
         {
            var cancel = args.Cancel;
            Console.WriteLine(cancel ? "Пользователь отменил запрос на окончание сеанса" : string.Empty);

            if (cancel)
               return;

            string statusMessage;
            switch (args.Reason)
            {
               case SessionEndReasons.Logoff:
                  statusMessage = "Пользователь выходит из системы";
                  break;

               case SessionEndReasons.SystemShutdown:
                  statusMessage = "ОС завершает работу";
                  break;

               default:
                  statusMessage = string.Empty;
                  break;
            }

            Console.WriteLine(statusMessage);
         };

         SystemEvents.SessionSwitch += (sender, args) =>
         {
            var switchReason = args.Reason;
            Console.WriteLine("Изменился режим использования: {0}", switchReason);
         };

         SystemEvents.TimeChanged += (sender, args) => Console.WriteLine("Изменилось время системных часов");

         SystemEvents.UserPreferenceChanged += (sender, args) =>
         {
            var preferenceCategory = args.Category;
            Console.WriteLine("Произошло изменение пользовательских параметров: {0}", preferenceCategory);
         };

         Console.ReadLine();
      }
   }
}