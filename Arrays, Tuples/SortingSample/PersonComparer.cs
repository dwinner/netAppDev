using System;
using System.Collections.Generic;

namespace SortingSample
{
   public class PersonComparer : IComparer<Person>
   {
      private readonly PersonCompareType _compareType;

      public PersonComparer(PersonCompareType compareType)
      {
         _compareType = compareType;
      }

      public int Compare(Person firstPerson, Person secondPerson)
      {
         if (firstPerson == null)
            throw new ArgumentNullException("firstPerson");
         if (secondPerson == null)
            throw new ArgumentNullException("secondPerson");

         switch (_compareType)
         {
            case PersonCompareType.FirstName:
               return string.Compare(firstPerson.FirstName, secondPerson.FirstName, StringComparison.Ordinal);
            case PersonCompareType.LastName:
               return string.Compare(firstPerson.LastName, secondPerson.LastName, StringComparison.Ordinal);
            default:
               throw new ArgumentException("unexpected compare type");
         }
      }
   }
}