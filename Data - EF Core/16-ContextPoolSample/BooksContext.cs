using Microsoft.EntityFrameworkCore;

namespace _16_ContextPoolSample
{
   public class BooksContext : DbContext
   {
      public BooksContext(DbContextOptions<BooksContext> options)
         : base(options)
      {
      }

      public DbSet<Book> Books { get; set; }
   }
}