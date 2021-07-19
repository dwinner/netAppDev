using System;
using System.Linq;

namespace _11_TPHWithFluentAPI
{
   internal static class Program
   {
      private static void Main()
      {
         CreateDatabase();
         AddSampleData();
         QuerySample();
         DeleteDatabase();
      }

      private static void QuerySample()
      {
         using var context = new BankContext();
         var creditcardPayments = context.Payments.OfType<CreditcardPayment>();
         foreach (var payment in creditcardPayments)
         {
            Console.WriteLine($"{payment.Name}, {payment.Amount}");
         }
      }

      private static void AddSampleData()
      {
         using var context = new BankContext();
         context.Payments.Add(new CashPayment {Name = "Donald", Amount = 0.5M});
         context.Payments.Add(new CashPayment {Name = "Scrooge", Amount = 20000M});
         context.Payments.Add(new CreditcardPayment
            {Name = "Gus Goose", Amount = 300M, CreditcardNumber = "987654321"});
         context.SaveChanges();
      }

      private static void CreateDatabase()
      {
         using var context = new BankContext();
         var created = context.Database.EnsureCreated();
         var creationInfo = created ? "created" : "exists";
         Console.WriteLine($"database {creationInfo}");
      }

      private static void DeleteDatabase()
      {
         Console.Write("Delete the database? ");
         var input = Console.ReadLine();
         if (input?.ToLower() == "y")
         {
            using var context = new BankContext();
            var deleted = context.Database.EnsureDeleted();
            var deletionInfo = deleted ? "deleted" : "not deleted";
            Console.WriteLine($"database {deletionInfo}");
         }
      }
   }
}