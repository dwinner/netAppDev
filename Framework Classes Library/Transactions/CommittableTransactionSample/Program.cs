/**
 * Фиксируемые транзакции
 */

using System;
using System.Threading.Tasks;
using System.Transactions;
using DataLibrary;
using Utilities;

namespace CommittableTransactionSample
{
   class Program
   {
      static void Main()
      {
         Task continueTask = CommittableTransactionAsync();
         continueTask.Wait();
      }

      private static async Task CommittableTransactionAsync()
      {
         var committableTransaction = new CommittableTransaction();
         TxUtilities.DisplayTransactionInformation(
            "TX created", committableTransaction.TransactionInformation);
         try
         {
            var student = new Student
            {
               FirstName = "Stephanie",
               LastName = "Nagel",
               Company = "CN innovation"
            };

            var studentData = new StudentData();
            await studentData.AddStudentAsync(student, committableTransaction);

            if (TxUtilities.AbortTransaction())
            {
               throw new ApplicationException("Transaction abort");  // транзакция прервана
            }

            committableTransaction.Commit();
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
            Console.WriteLine();
            committableTransaction.Rollback(/*ex*/);
         }

         TxUtilities.DisplayTransactionInformation(
            "TX completed", committableTransaction.TransactionInformation);
      }
   }
}
