using Microsoft.EntityFrameworkCore;

namespace BooksLib
{
   public class BooksContext : DbContext
   {
      public BooksContext(DbContextOptions<BooksContext> options)
         : base(options)
      {
      }

      public DbSet<Book> Books => Set<Book>();

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         base.OnConfiguring(optionsBuilder);
      }
   }
}