using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static _06_BooksSample.ColumnNames;

namespace _06_BooksSample
{
   public class BooksContext : DbContext
   {
      private const string ConnectionString =
         @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BooksSample;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

      public DbSet<Book> Books { get; set; }

      public DbSet<Author> Authors { get; set; }

      public DbSet<BookAuthor> BookAuthors { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         base.OnConfiguring(optionsBuilder);
         optionsBuilder.UseSqlServer(ConnectionString)
            /*.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))*/;
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder);

         modelBuilder.Entity<Book>().HasQueryFilter(b => !EF.Property<bool>(b, IsDeleted));

         modelBuilder.Entity<Book>().Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(50);
         modelBuilder.Entity<Book>().Property(b => b.Publisher)
            .HasField("_publisher")
            .IsRequired(false)
            .HasMaxLength(30);

         modelBuilder.Entity<Book>().Property<int>(BookId)
            .HasField("_bookId")
            .IsRequired();
         modelBuilder.Entity<Book>()
            .HasKey(BookId);

         // shadow properties
         modelBuilder.Entity<Book>().Property<bool>(IsDeleted);
         modelBuilder.Entity<Book>().Property<DateTime>(LastUpdated);

         modelBuilder.ApplyConfiguration(new AuthorConfiguration());
         modelBuilder.ApplyConfiguration(new BookAuthorConfiguration());
      }

      public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
      {
         ChangeTracker.DetectChanges();

         foreach (var item in ChangeTracker.Entries<Book>()
            .Where(e => e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted))
         {
            item.CurrentValues[LastUpdated] = DateTime.Now;
            if (item.State == EntityState.Deleted)
            {
               item.State = EntityState.Modified;
               item.CurrentValues[IsDeleted] = true;
            }
         }

         return base.SaveChangesAsync(cancellationToken);
      }

      // ReSharper disable once AsyncConverter.AsyncWait
      public override int SaveChanges() => SaveChangesAsync().Result;
   }
}