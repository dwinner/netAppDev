using Microsoft.EntityFrameworkCore;

namespace HelloEFCore
{
   public sealed class ApplicationContext : DbContext
   {
      private const string ConnectionString =
         "Server=(localdb)\\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;";

      public ApplicationContext()
      {
         Database.EnsureCreated();
      }

      public DbSet<User> Users { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         optionsBuilder.UseSqlServer(ConnectionString);
         base.OnConfiguring(optionsBuilder);
      }
   }
}