using Microsoft.EntityFrameworkCore;

namespace _11_TPHWithFluentAPI
{
   public class BankContext : DbContext
   {
      private const string ConnectionString =
         @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LocalBank;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

      public DbSet<Payment> Payments { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         base.OnConfiguring(optionsBuilder);

         optionsBuilder.UseSqlServer(ConnectionString);
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<Payment>().Property(p => p.Name).IsRequired();
         modelBuilder.Entity<Payment>().Property(p => p.Amount).HasColumnType("Money");
         modelBuilder.Entity<Payment>().Property<string>(ColumnNames.Type); // shadow state for the discriminator
         modelBuilder.Entity<Payment>().HasDiscriminator<string>(ColumnNames.Type)
            .HasValue<CashPayment>(ColumnValues.Cash)
            .HasValue<CreditcardPayment>(ColumnValues.Creditcard);
      }
   }
}