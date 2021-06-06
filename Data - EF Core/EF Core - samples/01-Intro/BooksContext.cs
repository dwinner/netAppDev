using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace _01_Intro
{
   public class BooksContext : DbContext
   {
      private const string ConnectionString =
         @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Books;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

      public DbSet<Book> Books { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         base.OnConfiguring(optionsBuilder);
         optionsBuilder.UseSqlServer(ConnectionString);
         optionsBuilder.LogTo(message => Debug.WriteLine(message));
      }
   }
}