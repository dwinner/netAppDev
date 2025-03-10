﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable enable

namespace WinUICultureDemo
{
   public class CulturesViewModel : INotifyPropertyChanged
   {
      private CultureData? _selectedCulture;
      public CulturesViewModel() => SetupCultures();
      public IList<CultureData> RootCultures { get; } = new List<CultureData>();

      public CultureData? SelectedCulture
      {
         get => _selectedCulture;
         set => SetProperty(ref _selectedCulture, value);
      }

      public event PropertyChangedEventHandler? PropertyChanged;

      private void SetProperty<T>(ref T item, T value, [CallerMemberName] string? propertyName = default)
      {
         if (!EqualityComparer<T>.Default.Equals(item, value))
         {
            item = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
         }
      }

      private void SetupCultures()
      {
         var cultureDataDict = CultureInfo.GetCultures(CultureTypes.AllCultures)
            .OrderBy(c => c.Name)
            .Select(c => new CultureData(c))
            .ToDictionary(c => c.CultureInfo.Name);

         List<CultureData> rootCultures = new();
         foreach (var cd in cultureDataDict.Values)
         {
            if (cd.CultureInfo.Parent.LCID == 0x7f) // check for invariant culture
            {
               rootCultures.Add(cd);
            }
            else // add to parent culture
            {
               if (cultureDataDict.TryGetValue(cd.CultureInfo.Parent.Name, out var parentCultureData))
               {
                  parentCultureData.SubCultures.Add(cd);
                  continue;
               }

               // with the latest culture updates, some cultures don't have the direct parent name in the list, take the next parent
               string parent = cd.CultureInfo.Parent.Name;
               var index = parent.IndexOf("-", StringComparison.CurrentCultureIgnoreCase);
               if (index < 0)
               {
                  // just add this culture to the root cultures
                  rootCultures.Add(cd);
                  continue;
               }

               string grandParent = parent[..index];
               if (cultureDataDict.TryGetValue(grandParent, out var grandParentCultureData))
               {
                  grandParentCultureData.SubCultures.Add(cd);
               }
               else // parent also not found to the root cultures, add it directly
               {
                  rootCultures.Add(cd);
               }
            }
         }

         foreach (var rootCulture in rootCultures.OrderBy(cd => cd.CultureInfo.EnglishName))
         {
            RootCultures.Add(rootCulture);
         }
      }
   }
}

#nullable restore