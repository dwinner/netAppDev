using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _03_MenuSamples
{
   public class MenuConfiguration : IEntityTypeConfiguration<Menu>
   {
      public void Configure(EntityTypeBuilder<Menu> builder)
      {
         builder.ToTable(nameof(MenusContext.Menus))
            .HasKey(m => m.MenuId);

         builder.Property(m => m.MenuId)
            .ValueGeneratedOnAdd();

         builder.Property(m => m.Text)
            .HasMaxLength(120);

         builder.Property(m => m.Price)
            .HasColumnType("Money");

         builder.HasOne(menu => menu.MenuCard)
            .WithMany(menuCard => menuCard.Menus)
            .HasForeignKey(menu => menu.MenuCardId);
      }
   }
}