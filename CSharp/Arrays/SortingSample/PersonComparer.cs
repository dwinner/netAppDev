using System;
using System.Collections.Generic;

namespace Wrox.ProCSharp.Arrays
{
   public enum PersonCompareType
   {
      FirstName,
      LastName
   }

   public class PersonComparer : IComparer<Person>
   {
      private readonly PersonCompareType _compareType;

      public PersonComparer(PersonCompareType compareType) =>
         _compareType = compareType;

      #region IComparer<Person> Members

      public int Compare(Person? x, Person? y)
      {
         switch (x)
         {
            case null when y is null:
               return 0;
            case null:
               return 1;
         }

         if (y is null)
         {
            return -1;
         }

         const StringComparison strComp = StringComparison.CurrentCultureIgnoreCase;
         return _compareType switch
         {
            PersonCompareType.FirstName => string.Compare(x.FirstName, y.FirstName, strComp),
            PersonCompareType.LastName => string.Compare(x.LastName, y.LastName, strComp),
            _ => throw new ArgumentException("unexpected compare type")
         };
      }

      #endregion
   }
}