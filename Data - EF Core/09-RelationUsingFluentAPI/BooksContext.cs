using Microsoft.EntityFrameworkCore;

namespace _09_RelationUsingFluentAPI
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

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<Book>()
            .HasMany(b => b.Chapters)
            .WithOne(c => c.Book);
         modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.WrittenBooks);
         modelBuilder.Entity<Book>()
            .HasOne(b => b.Reviewer)
            .WithMany(r => r.ReviewedBooks);
         modelBuilder.Entity<Book>()
            .HasOne(b => b.Editor)
            .WithMany(e => e.EditedBooks);

         modelBuilder.Entity<Chapter>()
            .HasOne(c => c.Book)
            .WithMany(b => b.Chapters)
            .HasForeignKey(c => c.BookId);

         modelBuilder.Entity<User>()
            .HasMany(a => a.WrittenBooks)
            .WithOne(b => b.Author);
         modelBuilder.Entity<User>()
            .HasMany(r => r.ReviewedBooks)
            .WithOne(b => b.Reviewer);
         modelBuilder.Entity<User>()
            .HasMany(e => e.EditedBooks)
            .WithOne(b => b.Editor);
      }
   }
}