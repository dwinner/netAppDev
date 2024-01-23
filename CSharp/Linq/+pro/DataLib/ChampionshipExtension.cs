using System;
using System.Collections.Generic;

namespace DataLib
{
   public static class ChampionshipExtension
   {
      private static readonly Func<string, string[]> SplitFunc = s =>
      {
         var result = new string[2];
         int index = s.LastIndexOf(' ');
         result[0] = s.Substring(0, index);
         result[1] = s.Substring(index + 1);

         return result;
      };

      private static readonly Func<int, int, string[], RacerInfo> GetRacerInfoFunc =
         (year, place, names) =>
            new RacerInfo { Year = year, Position = place, FirstName = names[0], LastName = names[1] };

      public static IEnumerable<RacerInfo> GetRacerInfo(this IEnumerable<Championship> source)
      {
         foreach (var item in source)
         {
            yield return GetRacerInfoFunc(item.Year, 1, SplitFunc(item.First));
            yield return GetRacerInfoFunc(item.Year, 2, SplitFunc(item.Second));
            yield return GetRacerInfoFunc(item.Year, 3, SplitFunc(item.Third));
         }
      }
   }
}