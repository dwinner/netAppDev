using Microsoft.EntityFrameworkCore;

namespace _03_MenuSamples
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
         // optionsBuilder.UseSqlServer(ConnectionString, options => options.MaxBatchSize(1));
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder);
         modelBuilder.HasDefaultSchema("mc");
         modelBuilder.ApplyConfiguration(new MenuCardConfiguration());
         modelBuilder.ApplyConfiguration(new MenuConfiguration());

         #region commented

         //modelBuilder.Entity<MenuCard>()
         //    .ToTable("MenuCards")
         //    .HasKey(c => c.MenuCardId);
         //modelBuilder.Entity<MenuCard>()
         //    .Property(c => c.MenuCardId)
         //    .ValueGeneratedOnAdd();
         //modelBuilder.Entity<MenuCard>()
         //    .Property(c => c.Title)
         //    .HasMaxLength(50);

         ////modelBuilder.Entity<MenuCard>()
         ////    .HasMany(c => c.Menus)
         ////    .WithOne(m => m.MenuCard);

         //modelBuilder.Entity<Menu>()
         //    .ToTable("Menus")
         //    .HasKey(m => m.MenuId);

         //modelBuilder.Entity<Menu>()
         //    .Property(m => m.MenuId)
         //    .ValueGeneratedOnAdd();

         //modelBuilder.Entity<Menu>()
         //    .Property(m => m.Text)
         //    .HasMaxLength(120);

         //modelBuilder.Entity<Menu>()
         //    .Property(m => m.Price)
         //    .HasColumnType("Money");

         //modelBuilder.Entity<Menu>()
         //    .HasOne(m => m.MenuCard)
         //    .WithMany(m => m.Menus)
         //    .HasForeignKey(m => m.MenuCardId);

         #endregion
      }
   }
}