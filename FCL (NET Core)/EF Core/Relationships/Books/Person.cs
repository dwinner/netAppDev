using System.Collections.Generic;

public class Person
{
   public ICollection<Book> WrittenBooks = new HashSet<Book>();

   public Person(string firstName, string lastName, int personId = 0)
   {
      FirstName = firstName;
      LastName = lastName;
      PersonId = personId;
   }

   public int PersonId { get; }

   public string FirstName { get; set; }
   public string LastName { get; set; }

   public Address? BusinessAddress { get; set; }
   public Address? PrivateAddress { get; set; }
}