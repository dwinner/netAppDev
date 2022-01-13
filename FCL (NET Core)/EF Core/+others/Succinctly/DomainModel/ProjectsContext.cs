using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;

namespace DomainModel
{
   public sealed class ProjectsContext : DbContext
   {
      public ProjectsContext(string connectionString)
         : base(GetOptions(connectionString))
      {
         Database.EnsureCreated();
      }

      public ProjectsContext(DbContextOptions<ProjectsContext> options)
         : base(options)
      {
         Database.EnsureCreated();
      }

      public Func<string> UserProviderFunc { get; } = () => WindowsIdentity.GetCurrent().Name;

      public Func<DateTime> TimestampProviderFunc { get; } = () => DateTime.UtcNow;

      //public DbSet<Tool> Tools { get; private set; }

      public DbSet<Resource> Resources { get; private set; }

      public DbSet<Project> Projects { get; private set; }

      public DbSet<Customer> Customers { get; private set; }

      public DbSet<Technology> Technologies { get; private set; }

      [DbFunction]
      public static string Soundex(string text) => throw new NotImplementedException();

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         base.OnConfiguring(optionsBuilder);
      }

      private static DbContextOptions GetOptions(string connectionString)
      {
         var builder = new DbContextOptionsBuilder();
         return builder.UseSqlServer(connectionString,
            b => b.MigrationsAssembly("UnitTests")).Options;
      }

      protected override void OnModelCreating(ModelBuilder builder)
      {
         builder.Entity<Resource>().OwnsOne(x => x.Contact);
         builder.Entity<Customer>().OwnsOne(x => x.Contact);

         builder.Entity<Project>()
            .HasOne(x => x.Detail)
            .WithOne(x => x.Project)
            .HasForeignKey<ProjectDetail>(x => x.ProjectId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

         builder.Entity<Resource>()
            .HasMany(x => x.Technologies);

         builder.Entity<Technology>()
            .HasMany(x => x.Resources);

         builder
            .Entity<Project>()
            .HasMany(x => x.ProjectResources)
            .WithOne(x => x.Project)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

         builder
            .Entity<Project>()
            .HasOne(x => x.Detail)
            .WithOne(x => x.Project)
            .OnDelete(DeleteBehavior.Cascade);

         foreach (var entity in builder.Model.GetEntityTypes()
            .Where(x => typeof(IAuditable).IsAssignableFrom(x.ClrType)))
         {
            entity.AddProperty("CreatedBy", typeof(string)).SetMaxLength(50);
            entity.AddProperty("CreatedAt", typeof(DateTime));
            entity.AddProperty("UpdatedBy", typeof(string)).SetMaxLength(50);
            entity.AddProperty("UpdatedAt", typeof(DateTime?));
         }

         base.OnModelCreating(builder);
      }

      public override int SaveChanges()
      {
         foreach (var entry in ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
         {
            if (entry.Entity is IAuditable)
            {
               if (entry.State == EntityState.Added)
               {
                  entry.Property("CreatedBy").CurrentValue = UserProviderFunc();
                  entry.Property("CreatedAt").CurrentValue = TimestampProviderFunc();
               }
               else
               {
                  entry.Property("UpdatedBy").CurrentValue = UserProviderFunc();
                  entry.Property("UpdatedAt").CurrentValue = TimestampProviderFunc();
               }
            }

            Validator.ValidateObject(entry.Entity, new ValidationContext(entry.Entity));
         }

         return base.SaveChanges();
      }
   }
}