using Microsoft.EntityFrameworkCore;

namespace _12_TableSplitting
{
   public class MenusContext : DbContext
   {
      private const string ConnectionString =
         @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Menus;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

      public DbSet<Menu> Menus { get; set; }

      public DbSet<MenuDetails> MenuDetails { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         base.OnConfiguring(optionsBuilder);

         optionsBuilder.UseSqlServer(ConnectionString);
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<Menu>()
            .HasOne(menu => menu.Details)
            .WithOne(details => details.Menu)
            .HasForeignKey<MenuDetails>(d => d.MenuDetailsId);
         modelBuilder.Entity<Menu>().ToTable(SchemaNames.Menus);
         modelBuilder.Entity<MenuDetails>().ToTable(SchemaNames.Menus);
         modelBuilder.Entity<Menu>().Property(m => m.Price).HasColumnType("money");
      }
   }
}