﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MigrationsLib
{
   public class MenuCardConfiguration : IEntityTypeConfiguration<MenuCard>
   {
      public void Configure(EntityTypeBuilder<MenuCard> builder)
      {
         builder.ToTable("MenuCards")
            .HasKey(c => c.MenuCardId);
         builder.Property(c => c.MenuCardId)
            .ValueGeneratedOnAdd();
         builder.Property(c => c.Title)
            .HasMaxLength(50);

         builder.HasMany(c => c.Menus)
            .WithOne(m => m.MenuCard);

         builder.Property<DateTime>("LastUpdated");
         builder.Property<bool>("IsDeleted");
      }
   }
}