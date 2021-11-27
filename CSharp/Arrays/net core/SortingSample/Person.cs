using System;

namespace Wrox.ProCSharp.Arrays
{
   public record Person(string FirstName, string LastName) : IComparable<Person>
   {
      public int CompareTo(Person? other)
      {
         if (other == null)
         {
            throw new ArgumentNullException(nameof(other));
         }

         var result = string.Compare(LastName, other.LastName, StringComparison.CurrentCultureIgnoreCase);
         if (result == 0)
         {
            result = string.Compare(FirstName, other.FirstName, StringComparison.CurrentCultureIgnoreCase);
         }

         return result;
      }

      public override string ToString() => $"{FirstName} {LastName}";
   }
}