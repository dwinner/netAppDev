using System.Data.Entity;

namespace CodeFirst
{
   public class MenuContext : DbContext
   {
      //private const string ConnectionString = @"server=(local)\sqlexpress;database=Menus;trusted_connection=true";

      public MenuContext()
         //: base(ConnectionString)
      {
      }

      public DbSet<Menu> Menus { get; set; }
      public DbSet<MenuCard> MenuCards { get; set; }

      protected override void OnModelCreating(DbModelBuilder modelBuilder)
      {
         modelBuilder.Entity<Menu>().Property(menu => menu.Price).HasColumnType("money");
         modelBuilder.Entity<Menu>().Property(menu => menu.Day).HasColumnType("date");
         modelBuilder.Entity<Menu>().Property(menu => menu.Text).HasMaxLength(40).IsRequired();
         modelBuilder.Entity<Menu>()
            .HasRequired(menu => menu.MenuCard)
            .WithMany(card => card.Menus)
            .HasForeignKey(menu => menu.MenuCardId);

         modelBuilder.Entity<MenuCard>().Property(card => card.Text).HasMaxLength(30).IsRequired();
         modelBuilder.Entity<MenuCard>().HasMany(card => card.Menus).WithRequired().WillCascadeOnDelete();
      }
   }
}