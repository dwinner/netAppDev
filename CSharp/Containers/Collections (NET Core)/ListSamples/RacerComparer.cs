using System;
using System.Collections.Generic;

namespace ListSamples
{
   public class RacerComparer : IComparer<Racer>
   {
      private readonly CompareType _compareType;

      public RacerComparer(CompareType compareType) =>
         _compareType = compareType;

      public int Compare(Racer? x, Racer? y)
      {
         switch (x)
         {
            case null when y is null:
               return 0;
            case null:
               return -1;
         }

         if (y is null)
         {
            return 1;
         }

         const StringComparison compCase = StringComparison.CurrentCultureIgnoreCase;
         int CompareCountry(Racer first, Racer second)
         {
            var result = string.Compare(first.Country, second.Country, compCase);
            if (result == 0)
            {
               result = string.Compare(first.LastName, second.LastName, compCase);
            }

            return result;
         }

         return _compareType switch
         {
            CompareType.FirstName => string.Compare(x.FirstName, y.FirstName, compCase),
            CompareType.LastName => string.Compare(x.LastName, y.LastName, compCase),
            CompareType.Country => CompareCountry(x, y),
            CompareType.Wins => x.Wins.CompareTo(y.Wins),
            _ => throw new ArgumentException("Invalid Compare Type")
         };
      }
   }
}