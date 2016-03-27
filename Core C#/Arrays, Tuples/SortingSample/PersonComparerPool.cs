using System;
using System.Collections.Generic;

namespace SortingSample
{
   public static class PersonComparerPool
   {
      private static readonly IDictionary<PersonCompareType, IComparer<Person>> PersonComparers =
         new Dictionary<PersonCompareType, IComparer<Person>>(Enum.GetValues(typeof(PersonCompareType)).Length);

      public static IComparer<Person> CreateComparerType(PersonCompareType personCompareType)
      {
         switch (personCompareType)
         {
            case PersonCompareType.FirstName:
               return GetComparer(personCompareType);
            case PersonCompareType.LastName:
               return GetComparer(personCompareType);
            default:
               throw new ArgumentException("unexpected compare type");
         }
      }

      private static IComparer<Person> GetComparer(PersonCompareType personCompareType)
      {
         IComparer<Person> comparer;
         return PersonComparers.TryGetValue(personCompareType, out comparer)
            ? comparer
            : (PersonComparers[personCompareType] = new PersonComparer(personCompareType));
      }
   }
}