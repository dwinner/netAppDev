// Мониторинг доменов

using System;
using System.Collections.Generic;

namespace AppDomainMonitoring
{
   internal static class Program
   {
      private static void Main()
      {
         using (new AppDomainMonitorDelta())
         {
            // Выделено около 10 миллионов байтов, которые переживут сборку мусора
            var list = new List<object>();
            for (var i = 0; i < 1000; i++) list.Add(new byte[10000]);

            // Выделено около 20 миллионов байтов, которые НЕ переживут сборку мусора
            for (var i = 0; i < 2000; i++) new byte[10000].GetType();

            // Раскручиваем процессор около 5-ти секунд
            long stop = Environment.TickCount + 5000;
            while (Environment.TickCount < stop)
            {
            }
         }
      }
   }
}