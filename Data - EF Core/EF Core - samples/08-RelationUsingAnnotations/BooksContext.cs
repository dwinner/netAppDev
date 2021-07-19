using Microsoft.EntityFrameworkCore;

namespace _08_RelationUsingAnnotations
{
   public class BooksContext : DbContext
   {
      private const string ConnectionString =
         @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BooksAnno;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

      public DbSet<Book> Books { get; set; }

      public DbSet<Chapter> Chapters { get; set; }

      public DbSet<User> Users { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         base.OnConfiguring(optionsBuilder);

         optionsBuilder.UseSqlServer(ConnectionString);
      }
   }
}