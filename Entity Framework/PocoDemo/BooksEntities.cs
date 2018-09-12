using System.Data.Entity;

namespace PocoDemo
{
   public class BooksEntities : DbContext
   {
      public BooksEntities()
         : base("name=BooksEntities")
      {
      }

      public DbSet<Author> Authors { get; set; }
      public DbSet<Book> Books { get; set; }
   }
}