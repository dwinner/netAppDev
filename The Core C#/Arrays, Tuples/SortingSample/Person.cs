using System;

namespace SortingSample
{
   public class Person : IComparable<Person>
   {
      public string FirstName { get; set; }

      public string LastName { get; set; }

      public override string ToString()
      {
         return string.Format("{0} {1}", FirstName, LastName);
      }

      public int CompareTo(Person other)
      {
         if (other == null)
            throw new ArgumentNullException("other");
         int result = string.Compare(LastName, other.LastName, StringComparison.Ordinal);
         if (result == 0)
            result = string.Compare(FirstName, other.FirstName, StringComparison.Ordinal);
         return result;
      }
   }
}