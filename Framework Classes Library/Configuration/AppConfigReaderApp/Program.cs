// Чтение параметров конфигурации

using System;
using System.Configuration;
using static System.Console;
using static System.Enum;

namespace AppConfigReaderApp
{
   internal static class Program
   {
      private static void Main()
      {
         // Получаем специальные данные из файла *.config.
         var appReader = new AppSettingsReader();
         var numberOfTimes = (int) appReader.GetValue("RepeatCount", typeof (int));
         var textColor = (string) appReader.GetValue("TextColor", typeof (string));

         ForegroundColor = (ConsoleColor) Parse(typeof (ConsoleColor), textColor);
         for (var i = 0; i < numberOfTimes; i++)
            WriteLine("Howdy!");
         ReadKey();
      }
   }
}