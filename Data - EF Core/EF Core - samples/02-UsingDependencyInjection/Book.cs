using System.ComponentModel.DataAnnotations.Schema;

namespace _02_UsingDependencyInjection
{
   [Table("Books")]
   public class Book
   {
      public int BookId { get; set; }

      public string Title { get; set; }

      public string Publisher { get; set; }
   }
}
