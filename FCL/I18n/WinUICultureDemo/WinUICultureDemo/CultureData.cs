﻿using System;
using System.Collections.Generic;
using System.Globalization;

#nullable enable

namespace WinUICultureDemo
{
   public record CultureData(CultureInfo CultureInfo)
   {
      private const double Number = 9876543.21;
      private RegionInfo? _regionInfo;
      public IList<CultureData> SubCultures { get; } = new List<CultureData>();
      public string NumberSample => Number.ToString("N", CultureInfo);
      public string DateSample => DateTime.Today.ToString("D", CultureInfo);
      public string TimeSample => DateTime.Now.ToString("T", CultureInfo);

      public RegionInfo? RegionInfo
      {
         get
         {
            try
            {
               return _regionInfo ??= new RegionInfo(CultureInfo.Name);
            }
            catch (ArgumentException)
            {
               // with some neutral cultures regions are not available
               return null;
            }
         }
      }
   }
}

#nullable restore