using System.Collections.Generic;

namespace _06_BooksSample
{
   public class Book
   {
      // parameterless constructor needed for EF Core
      private Book()
      {
      }

      public Book(string title, string publisher)
      {
         Title = title;
         Publisher = publisher;
      }

      public int BookId { get; } = 0;

      public string Title { get; set; }

      public string Publisher { get; }

      public virtual List<BookAuthor> BookAuthors { get; set; }

      public override string ToString() => $"id: {BookId}, title: {Title}, publisher: {Publisher}";
   }
}