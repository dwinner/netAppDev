namespace AopTransactions
{
   using System;
   using System.Diagnostics;
   using System.IO;

   using Data;

   using static System.Console;

   internal static class Program
   {
      private const string ItDepartment = "00-001";
      private const string HrDepartment = "00-002";

      private static void Main()
      {
         // Initialization.
         CheckDtcRunning();
         CreateDatabases();

         // Perform a first transaction. It will be successful.
         WriteLine("Placing and paying one order...");
         WriteLine();
         IntegrationService.PlaceOrderAndPay(HrDepartment, ItDepartment, "Employee Benefit System", 450000);
         DumpDatabases();
         WriteLine();
         WriteLine();

         // Perform a second transaction. It will fail.
         WriteLine("Placing and paying a second order...");
         try
         {
            IntegrationService.PlaceOrderAndPay(HrDepartment, ItDepartment, "Employee Benefit System Rev 1", 75000);
         }
         catch (Exception e)
         {
            WriteLine("ERROR! {0}", e.Message);
            WriteLine(
                              "The transaction should be rolled back and the database state identical as before the transaction.");
         }

         WriteLine();
         DumpDatabases();
      }

      private static void CheckDtcRunning()
      {
         while (Process.GetProcessesByName("msdtc").Length == 0)
         {
            WriteLine("Distributed Transaction Coordinator (MSDTC) service is not running.");
            WriteLine("Execute 'net start msdtc' from an administrative command prompt and press Enter.");
            ReadLine();
         }
      }

      private static void DumpDatabases()
      {
         using (var financeDb = new FinanceDb())
         {
            foreach (Account account in financeDb.Accounts)
            {
               WriteLine("Account {0} ({1}) - Balance ${2}",
                                 account.Number,
                                 account.Name,
                                 account.Balance);
               WriteLine(
                                 "------------------------------------------------------------------------------------------");
               foreach (Operation operation in account.Operations)
               {
                  WriteLine("{0} - {1} - ${2}", operation.Time, operation.Description, operation.Amount);
               }
               WriteLine();
            }
         }

         using (var orderDb = new OrderDb())
         {
            WriteLine("Orders");
            WriteLine("----------------------------------------------------------------------------");
            foreach (Order order in orderDb.Orders)
            {
               WriteLine("{0} - {1}", order.Description, order.TotalAmount);
            }
         }
      }

      private static void CreateDatabases()
      {
         string dataDirectory = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]));
         AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory);

         using (var financeDb = new FinanceDb())
         {
            if (!financeDb.Database.CreateIfNotExists())
            {
               financeDb.Operations.RemoveRange(financeDb.Operations);
               financeDb.Accounts.RemoveRange(financeDb.Accounts);
            }

            financeDb.Accounts.Add(new Account
            {
               Name = "IT Department",
               Number = ItDepartment
            });

            financeDb.Accounts.Add(new Account
            {
               Name = "HR Department",
               Number = HrDepartment,
               Balance = 500000
            });
            financeDb.SaveChanges();
         }

         using (var orderDb = new OrderDb())
         {
            if (!orderDb.Database.CreateIfNotExists())
            {
               orderDb.Orders.RemoveRange(orderDb.Orders);
               orderDb.SaveChanges();
            }
         }
      }
   }
}