using Microsoft.EntityFrameworkCore;

namespace EF;

public class CrmContext(DbContextOptions<CrmContext> options) : DbContext(options)
{
   public CrmContext(string connectionString)
      : this(new DbContextOptionsBuilder<CrmContext>(). /*UseSqlServer(connectionString).*/Options)
   {
   }

   public DbSet<User> Users { get; set; }
   public DbSet<Company> Companies { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder.Entity<User>(x =>
      {
         x.HasKey(user => user.UserId);
         x.Property(k => k.Email);
         x.Property(k => k.Type);
         x.Property(k => k.IsEmailConfirmed);
         x.Ignore(k => k.DomainEvents);
      });

      modelBuilder.Entity<Company>(x =>
      {
         x.HasKey(company => company.DomainName);
         x.Property(p => p.DomainName);
         x.Property(p => p.NumberOfEmployees);
      });
   }
}