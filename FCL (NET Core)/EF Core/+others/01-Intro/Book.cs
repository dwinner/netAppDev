using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _01_Intro
{
   [Table(nameof(BooksContext.Books))]
   public class Book
   {
      [Key]
      public int BookId { get; set; }

      [Required]
      [StringLength(50)]
      public string Title { get; set; }

      [StringLength(30)]
      public string Publisher { get; set; }
   }
}