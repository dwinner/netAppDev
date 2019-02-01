/**
 * Создание специфичных культур
 */

using System;
using System.Globalization;

namespace _02_CreateSpecificCultures
{
   class Program
   {
      static void Main()
      {
         var cultureAndRegionInfoBuilder = new CultureAndRegionInfoBuilder("x-en-US-metric",
            CultureAndRegionModifiers.None);
         cultureAndRegionInfoBuilder.LoadDataFromCultureInfo(new CultureInfo("en-US"));
         cultureAndRegionInfoBuilder.LoadDataFromRegionInfo(new RegionInfo("US"));
         cultureAndRegionInfoBuilder.IsMetric = true;          // Внести изменения
         cultureAndRegionInfoBuilder.Save("x-en-US-metric");   // Создать файл LDML
         cultureAndRegionInfoBuilder.Register();               // Зарегистрировать в системе

         var regionInfo = new RegionInfo("x-en-US-metric");
         Console.WriteLine(regionInfo.IsMetric);

         Console.ReadKey();
      }
   }
}
