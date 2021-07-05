using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _03_MenuSamples
{
   public class MenuCardConfiguration : IEntityTypeConfiguration<MenuCard>
   {
      public void Configure(EntityTypeBuilder<MenuCard> builder)
      {
         builder.ToTable(nameof(MenusContext.MenuCards))
            .HasKey(card => card.MenuCardId);
         builder.Property(card => card.MenuCardId)
            .ValueGeneratedOnAdd();
         builder.Property(card => card.Title)
            .HasMaxLength(50);

         builder.HasMany(card => card.Menus)
            .WithOne(menu => menu.MenuCard);

         builder.Property<DateTime>("LastUpdated");
         builder.Property<bool>("IsDeleted");
      }
   }
}