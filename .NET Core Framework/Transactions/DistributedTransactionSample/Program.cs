/**
 * Продвижение транзакций
 */

using System;
using System.Transactions;
using DataLibrary;
using Utilities;

namespace DistributedTransactionSample
{
   class Program
   {
      static void Main()
      {
         TransactionPromotion();
         Console.ReadLine();
      }

      static async void TransactionPromotion()
      {
         var commitTx = new CommittableTransaction(); // NOTE: Фиксируемая транзакция
         TxUtilities.DisplayTransactionInformation(
            "TX created", commitTx.TransactionInformation);
         try
         {
            var studentData = new StudentData();

            var firstStudent = new Student
            {
               FirstName = "Matthias",
               LastName = "Nagel",
               Company = "CN innovation"
            };
            await studentData.AddStudentAsync(firstStudent, commitTx);  // NOTE: Присоединение локальной транзакции к соединению

            var secondStudent = new Student
            {
               FirstName = "Stephanie",
               LastName = "Nagel",
               Company = "CN innovation"
            };
            await studentData.AddStudentAsync(secondStudent, commitTx); // NOTE: Транзакция продвигается до распределенной

            TxUtilities.DisplayTransactionInformation(
               "2nd connection enlisted", commitTx.TransactionInformation);

            if (TxUtilities.AbortTransaction())
            {
               throw new ApplicationException("transaction abort");
            }

            commitTx.Commit();
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
            Console.WriteLine();
            commitTx.Rollback(/*ex*/);
         }
      }
   }
}
