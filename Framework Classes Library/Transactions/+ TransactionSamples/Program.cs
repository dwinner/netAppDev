using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using DataLibrary;
using Utilities;

namespace Wrox.ProCSharp.Transactions
{
   class Program
   {
      static void Main()
      {
         string[] args = Environment.GetCommandLineArgs();
         string[] availableCommands = { "/C", "/T", "/D", "/S", "/N" };

         if (args.Length != 2 || !availableCommands.Contains(args[1].ToUpper()))
         {
            Console.WriteLine("Transaction Samples for Professional C#");
            Console.WriteLine("Parameter List:");
            Console.WriteLine("\t/C\tCommittable Transactions");
            Console.WriteLine("\t/T\tTransaction Promotion");
            Console.WriteLine("\t/D\tDependent Transaction");
            Console.WriteLine("\t/S\tTransaction Scope");
            Console.WriteLine("\t/N\tNested Scope");
            return;
         }
         Task t = null;
         switch (args[1].ToUpper())
         {
            case "/C":
               t = CommittableTransactionAsync();
               break;
            case "/T":
               t = TransactionPromotionAsync();
               break;
            case "/D":
               DependentTransaction();
               break;
            case "/S":
               t = TransactionScopeAsync();
               break;
            case "/N":
               NestedScopes();
               break;
         }
         if (t != null) t.Wait();
      }

      static void NestedScopes()
      {
         using (var scope = new TransactionScope())
         {
            Transaction.Current.TransactionCompleted += OnTransactionCompleted;

            TransactionUtilities.DisplayTransactionInformation("Ambient TX created",
                  Transaction.Current.TransactionInformation);

            using (var scope2 =
                  new TransactionScope(TransactionScopeOption.RequiresNew))
            {
               Transaction.Current.TransactionCompleted += OnTransactionCompleted;

               TransactionUtilities.DisplayTransactionInformation(
                      "Inner Transaction Scope",
                      Transaction.Current.TransactionInformation);

               scope2.Complete();
            }
            scope.Complete();
         }

      }

      static async Task TransactionScopeAsync()
      {
         using (var scope = new TransactionScope())
         {
            Transaction.Current.TransactionCompleted += OnTransactionCompleted;

            TransactionUtilities.DisplayTransactionInformation("Ambient TX created",
                  Transaction.Current.TransactionInformation);

            var s1 = new Student
            {
               FirstName = "Angela",
               LastName = "Nagel",
               Company = "Kantine M101"
            };
            var db = new StudentData();
            await db.AddStudentAsync(s1);

            if (!TransactionUtilities.AbortTransaction())
               scope.Complete();
            else
               Console.WriteLine("transaction will be aborted");

         } // scope.Dispose()

      }

      static void OnTransactionCompleted(object sender, TransactionEventArgs e)
      {
         TransactionUtilities.DisplayTransactionInformation("TX completed",
               e.Transaction.TransactionInformation);
      }


      static void DependentTransaction()
      {
         var tx = new CommittableTransaction();
         TransactionUtilities.DisplayTransactionInformation("Root TX created",
               tx.TransactionInformation);

         try
         {
            Task.Factory.StartNew(TxTask, tx.DependentClone(DependentCloneOption.BlockCommitUntilComplete));

            if (TransactionUtilities.AbortTransaction())
            {
               throw new ApplicationException("transaction abort");
            }

            tx.Commit();
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
            tx.Rollback();
         }

         TransactionUtilities.DisplayTransactionInformation("TX finished",
               tx.TransactionInformation);

      }

      static void TxTask(object obj)
      {
         var tx = obj as DependentTransaction;
         if (tx == null)
         {
            return;
         }

         TransactionUtilities.DisplayTransactionInformation("Dependent Transaction",
            tx.TransactionInformation);

         Thread.Sleep(3000);

         tx.Complete();

         TransactionUtilities.DisplayTransactionInformation("Dependent TX Complete",
            tx.TransactionInformation);
      }


      static async Task TransactionPromotionAsync()
      {
         var tx = new CommittableTransaction();
         TransactionUtilities.DisplayTransactionInformation("TX created",
               tx.TransactionInformation);

         try
         {
            var s1 = new Student
            {
               FirstName = "Matthias",
               LastName = "Nagel",
               Company = "CN innovation"
            };
            var db = new StudentData();
            await db.AddStudentAsync(s1, tx);

            var s2 = new Student
            {
               FirstName = "Stephanie",
               LastName = "Nagel",
               Company = "CN innovation"
            };
            await db.AddStudentAsync(s2, tx);

            TransactionUtilities.DisplayTransactionInformation("2nd connection enlisted",
                  tx.TransactionInformation);

            if (TransactionUtilities.AbortTransaction())
            {
               throw new ApplicationException("transaction abort");
            }

            tx.Commit();
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
            Console.WriteLine();
            tx.Rollback();
         }

         TransactionUtilities.DisplayTransactionInformation("TX finished",
               tx.TransactionInformation);

      }

      static async Task CommittableTransactionAsync()
      {
         var tx = new CommittableTransaction();
         TransactionUtilities.DisplayTransactionInformation("TX created",
               tx.TransactionInformation);

         try
         {
            var s1 = new Student
            {
               FirstName = "Stephanie",
               LastName = "Nagel",
               Company = "CN innovation"
            };
            var db = new StudentData();
            await db.AddStudentAsync(s1, tx);

            if (TransactionUtilities.AbortTransaction())
            {
               throw new ApplicationException("transaction abort");
            }

            tx.Commit();
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
            Console.WriteLine();
            tx.Rollback();
         }

         TransactionUtilities.DisplayTransactionInformation("TX completed",
               tx.TransactionInformation);

      }
   }
}
