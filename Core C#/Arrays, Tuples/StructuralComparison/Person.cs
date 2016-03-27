using System;

namespace StructuralComparison
{
   public class Person : IEquatable<Person>
   {
      public int Id { get; private set; }

      public string FirstName { get; set; }

      public string LastName { get; set; }

      public Person()
      {         
      }

      public Person(int id, string firstName, string lastName)
      {
         Id = id;
         FirstName = firstName;
         LastName = lastName;
      }

      public bool Equals(Person other)
      {
         if (other == null)
            throw new ArgumentNullException("other");
         return FirstName == other.FirstName && LastName == other.LastName;
      }

      public override string ToString()
      {
         return string.Format("Id: {0}, FirstName: {1}, LastName: {2}", Id, FirstName, LastName);
      }
   }
}