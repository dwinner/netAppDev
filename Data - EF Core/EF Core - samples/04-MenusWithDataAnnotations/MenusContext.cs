using Microsoft.EntityFrameworkCore;

namespace _04_MenusWithDataAnnotations
{
   public class MenusContext : DbContext
   {
      private const string ConnectionString =
         @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MenuCards;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

      public DbSet<Menu> Menus { get; set; }

      public DbSet<MenuCard> MenuCards { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         base.OnConfiguring(optionsBuilder);
         optionsBuilder.UseSqlServer(ConnectionString);
      }
   }
}