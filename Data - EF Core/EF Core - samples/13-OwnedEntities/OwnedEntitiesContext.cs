using Microsoft.EntityFrameworkCore;

namespace _13_OwnedEntities
{
   public class OwnedEntitiesContext : DbContext
   {
      private const string ConnectionString =
         @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=OwnedEntities;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

      public DbSet<Person> People { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         base.OnConfiguring(optionsBuilder);

         optionsBuilder.UseSqlServer(ConnectionString);
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<Person>()
            .OwnsOne(person => person.CompanyAddress)
            .OwnsOne(address => address.Location, builder =>
            {
               builder.Property(location => location.City).HasColumnName("BusinessCity");
               builder.Property(location => location.Country).HasColumnName("BusinessCountry");
            });
         modelBuilder.Entity<Person>()
            .OwnsOne(p => p.PrivateAddress)
            .ToTable("Addr")
            .OwnsOne(a => a.Location);
      }
   }
}