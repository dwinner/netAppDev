using System.Collections.Generic;

namespace _07_RelationUsingConventions
{
   public class Book
   {
      public int BookId { get; set; }

      public string Title { get; set; }

      public List<Chapter> Chapters { get; } = new();

      public User Author { get; set; }
   }
}