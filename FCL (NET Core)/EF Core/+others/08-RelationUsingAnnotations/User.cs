using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace _08_RelationUsingAnnotations
{
   public class User
   {
      public int UserId { get; set; }

      public string Name { get; set; }

      [InverseProperty(nameof(Book.Author))]
      public List<Book> WrittenBooks { get; set; }

      [InverseProperty(nameof(Book.Reviewer))]
      public List<Book> ReviewedBooks { get; set; }

      [InverseProperty(nameof(Book.ProjectEditor))]
      public List<Book> EditedBooks { get; set; }
   }
}