using System;
using System.Collections.Generic;

namespace ListSamples
{
   public class RacerComparer : IComparer<Racer>
   {
      public enum CompareType
      {
         FirstName,
         LastName,
         Country,
         Wins
      }

      private readonly CompareType _compareType;

      public RacerComparer(CompareType compareType)
      {
         _compareType = compareType;
      }

      public int Compare(Racer first, Racer second)
      {
         if (first == null && second == null)
            return 0;
         if (first == null)
            return -1;
         if (second == null)
            return 1;
         switch (_compareType)
         {
            case CompareType.FirstName:
               return string.Compare(first.FirstName, second.FirstName, StringComparison.CurrentCulture);
            case CompareType.LastName:
               return string.Compare(first.LastName, second.LastName, StringComparison.CurrentCulture);
            case CompareType.Country:
               int result = string.Compare(first.Country, second.Country, StringComparison.CurrentCulture);
               return result == 0
                  ? string.Compare(first.LastName, second.LastName, StringComparison.CurrentCulture)
                  : result;
            case CompareType.Wins:
               return first.Wins.CompareTo(second.Wins);
            default:
               throw new ArgumentException("Invalid Compare Type");  // недопустимый тип сравнения
         }
      }
   }
}