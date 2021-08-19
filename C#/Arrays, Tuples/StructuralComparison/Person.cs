using System;

namespace StructuralComparison
{
   public class Person : IEquatable<Person>
   {
      public string FirstName { private get; set; }

      public string LastName { private get; set; }

      public bool Equals(Person other)
      {
         if (other == null)
         {
            throw new ArgumentNullException(nameof(other));
         }

         return FirstName == other.FirstName && LastName == other.LastName;
      }

      public override string ToString()
         => $"FirstName: {FirstName}, LastName: {LastName}";
   }
}