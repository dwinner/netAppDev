using EfCrudSvc.Services;
using Microsoft.EntityFrameworkCore;

namespace EfCrudSvc;

public sealed class ApplicationContext : DbContext
{
   public ApplicationContext(DbContextOptions<ApplicationContext> options)
      : base(options)
   {
      Database.EnsureCreated();
   }

   public DbSet<User> Users { get; set; } = null!;

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder.Entity<User>().HasData(
         new User(1, "Tom", 37),
         new User(2, "Bob", 41),
         new User(3, "Sam", 24)
      );
   }
}