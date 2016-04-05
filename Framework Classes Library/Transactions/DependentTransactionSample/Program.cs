/**
 * Зависимые транзакции
 */

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Utilities;

namespace DependentTransactionSample
{
   class Program
   {
      static void Main()
      {
         DependentTransaction();
      }

      static void TxTask(object state)
      {
         var depTx = state as DependentTransaction;
         if (depTx == null)
         {
            return;
         }

         TxUtilities.DisplayTransactionInformation("Dependent Transaction", depTx.TransactionInformation);
         Thread.Sleep(3000);
         depTx.Complete();
         TxUtilities.DisplayTransactionInformation("Dependent Transaction Complete", depTx.TransactionInformation);
      }

      static void DependentTransaction()
      {
         var commitTx = new CommittableTransaction();
         TxUtilities.DisplayTransactionInformation("Root TX Created", commitTx.TransactionInformation);

         try
         {
            // NOTE: Создаем зависимую транзакцию
            Task.Factory.StartNew(TxTask, commitTx.DependentClone(DependentCloneOption.BlockCommitUntilComplete));
            if (TxUtilities.AbortTransaction())
            {
               throw new ApplicationException("transaction abort");
            }

            commitTx.Commit();
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
            commitTx.Rollback(ex);
         }

         TxUtilities.DisplayTransactionInformation("TX finished", commitTx.TransactionInformation);
      }
   }
}
