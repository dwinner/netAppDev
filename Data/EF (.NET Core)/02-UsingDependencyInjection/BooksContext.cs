using Microsoft.EntityFrameworkCore;

namespace _02_UsingDependencyInjection
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