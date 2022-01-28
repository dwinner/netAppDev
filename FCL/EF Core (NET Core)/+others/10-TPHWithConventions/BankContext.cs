using Microsoft.EntityFrameworkCore;

namespace _10_TPHWithConventions
{
   public class BankContext : DbContext
   {
      private const string ConnectionString =
         @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LocalBank;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


      public DbSet<Payment> Payments { get; set; }

      public DbSet<CreditcardPayment> CreditcardPayments { get; set; }

      public DbSet<CashPayment> CashPayments { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         base.OnConfiguring(optionsBuilder);

         optionsBuilder.UseSqlServer(ConnectionString);
      }
   }
}