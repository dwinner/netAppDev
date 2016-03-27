using System;

namespace GzipXmlSerializationSample
{
   public class Person : IEquatable<Person>
   {
      public Person(string firstName, string lastName)
      {
         FirstName = firstName;
         LastName = lastName;
      }

      public string FirstName { get; }
      public string LastName { get; }

      public bool Equals(Person other)
      {
         return !ReferenceEquals(null, other) &&
                (ReferenceEquals(this, other) ||
                 string.Equals(FirstName, other.FirstName) && string.Equals(LastName, other.LastName));
      }

      public override bool Equals(object obj)
      {
         return !ReferenceEquals(null, obj) &&
                (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((Person) obj));
      }

      public override int GetHashCode()
      {
         unchecked
         {
            return ((FirstName?.GetHashCode() ?? 0)*397) ^
                   (LastName?.GetHashCode() ?? 0);
         }
      }

      public static bool operator ==(Person left, Person right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(Person left, Person right)
      {
         return !Equals(left, right);
      }
   }
}