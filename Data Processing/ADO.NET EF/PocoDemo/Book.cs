using System.Collections.Generic;

namespace PocoDemo
{
   public class Book
   {
      public Book()
      {
         Authors = new HashSet<Author>();
      }

      public int Id { get; set; }
      public string Title { get; set; }
      public string Publisher { get; set; }
      public string Isbn { get; set; }

      public virtual ICollection<Author> Authors { get; set; }
   }
}