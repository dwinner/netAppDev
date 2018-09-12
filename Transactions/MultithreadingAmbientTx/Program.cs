/**
 * Передача охватывающих транзакций между потоками выполнения
 * 
 * NOTE: т.к. охватывающие транзакции ассоциированы с потоками выполнения,
 * то привычные правила передачи областей действия перестают действовать
 */

using System;
using System.Threading.Tasks;
using System.Transactions;
using Utilities;

namespace MultithreadingAmbientTx
{
   class Program
   {
      static void Main()
      {
         try
         {
            using (var scope = new TransactionScope())
            {
               Transaction.Current.TransactionCompleted += OnTransactionCompleted;
               TxUtilities.DisplayTransactionInformation("Main thread TX", Transaction.Current.TransactionInformation);
               //Task.Factory.StartNew(TaskMethod);
               Task.Factory.StartNew(TaskMethod,
                  Transaction.Current.DependentClone(DependentCloneOption.BlockCommitUntilComplete));
               scope.Complete();
            }
         }
         catch (TransactionAbortedException txAbortedEx)
         {
            Console.WriteLine("Main-Transaction was aborted {0}", txAbortedEx.Message);
         }

         Console.ReadKey();
      }

      public static void TaskMethod()
      {
         try
         {
            // NOTE: Здесь создается новая область действия в любом случае
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
               Transaction.Current.TransactionCompleted += OnTransactionCompleted;
               TxUtilities.DisplayTransactionInformation("Thread TX", Transaction.Current.TransactionInformation);
               scope.Complete();
            }
         }
         catch (TransactionAbortedException txAbortedEx)
         {
            Console.WriteLine("TaskMethod-Transaction was aborted {0}", txAbortedEx.Message);
         }
      }

      public static void TaskMethod(object state)
      {
         try
         {
            var dependentTx = state as DependentTransaction;
            if (dependentTx == null)   //Contract.Requires<ArgumentNullException>(dependentTx != null);
            {
               throw new ArgumentNullException("state");
            }

            Transaction.Current = dependentTx;  // NOTE: Передача зависимой транзакции в поток выполнения

            try
            {
               using (var scope = new TransactionScope())
               {
                  Transaction.Current.TransactionCompleted += OnTransactionCompleted;
                  TxUtilities.DisplayTransactionInformation("Task TX", Transaction.Current.TransactionInformation);
                  scope.Complete();
               }
            }
            finally
            {
               dependentTx.Complete();
            }
         }
         catch (TransactionAbortedException txAbortedEx)
         {
            Console.WriteLine("TaskMethod - Transaction was aborted, {0}", txAbortedEx);
         }
      }

      private static void OnTransactionCompleted(object sender, TransactionEventArgs e)
      {
         TxUtilities.DisplayTransactionInformation("TX completed", e.Transaction.TransactionInformation);
      }
   }
}
