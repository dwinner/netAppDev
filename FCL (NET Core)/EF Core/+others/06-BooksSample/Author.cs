using System.Collections.Generic;

namespace _06_BooksSample
{
   public class Author
   {
      private Author()
      {
      }

      public Author(string firstName, string lastName)
      {
         FirstName = firstName;
         LastName = lastName;
      }

      public int AuthorId { get; } = 0;

      public string FirstName { get; }

      public string LastName { get; }

      public virtual List<BookAuthor> BookAuthors { get; set; }

      public override string ToString() => $"{FirstName} {LastName}";
   }
}