using System;
using System.Collections.Generic;
using System.Globalization;

namespace CultureDemo
{
   /// <summary>
   /// Некоторый сущностный класс, связанный с информацией о культуре
   /// </summary>
   public class CultureData
   {
      public CultureInfo CultureInfo { get; set; }

      public List<CultureData> SubCultures { get; set; }

      private const double Number = 9876543.21;

      public string NumberSample
      {
         get { return Number.ToString("N", CultureInfo); }
      }

      public string DateSample
      {
         get { return DateTime.Today.ToString("D", CultureInfo); }
      }

      public string TimeSample
      {
         get { return DateTime.Now.ToString("T", CultureInfo); }
      }

      public RegionInfo RegionInfo
      {
         get
         {
            RegionInfo regionInfo;
            try
            {
               regionInfo = new RegionInfo(CultureInfo.Name);
            }
            catch (ArgumentException)
            {
               // Для некоторых нейтральных культур регионы недоступны.
               return null;
            }

            return regionInfo;
         }
      }
   }
}