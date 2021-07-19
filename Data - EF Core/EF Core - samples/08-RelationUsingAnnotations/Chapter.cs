using System.ComponentModel.DataAnnotations.Schema;

namespace _08_RelationUsingAnnotations
{
   public class Chapter
   {
      public int ChapterId { get; set; }

      public int Number { get; set; }

      public string Title { get; set; }

      public int BookId { get; set; }

      [ForeignKey(nameof(BookId))]
      public Book Book { get; set; }
   }
}