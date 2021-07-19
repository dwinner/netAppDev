using System.Collections.Generic;

namespace _09_RelationUsingFluentAPI
{
   public class Book
   {
      public int BookId { get; set; }

      public string Title { get; set; }

      public List<Chapter> Chapters { get; } = new();

      public User Author { get; set; }

      public User Reviewer { get; set; }

      public User Editor { get; set; }
   }
}