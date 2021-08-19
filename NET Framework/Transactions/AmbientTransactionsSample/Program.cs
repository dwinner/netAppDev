/**
 * Охватывающие транзакции
 */

using System;
using System.Transactions;
using DataLibrary;
using Utilities;

namespace AmbientTransactionsSample
{
   class Program
   {
      static void Main()
      {
         //TransactionScopeSimple();         
         //NewTransactionScope();
      }

      #region Простая охватывающая транзакция

      public static void TransactionScopeSimple()
      {
         using (var scope = new TransactionScope())
         {
            Transaction.Current.TransactionCompleted += Current_TransactionCompleted;
            TxUtilities.DisplayTransactionInformation("Ambient TX created", // Охватывающая транзакция создана
               Transaction.Current.TransactionInformation);
            var firstStudent = new Student
            {
               FirstName = "Angela",
               LastName = "Nagel",
               Company = "Kantine M101"
            };
            var studentData = new StudentData();
            studentData.AddStudent(firstStudent);
            if (!TxUtilities.AbortTransaction())
            {
               scope.Complete();
            }
            else
            {
               Console.WriteLine("Transaction will be aborted"); // Транзакция будет прекращена
            }
         }
      }

      #endregion


      #region Создание новой охватывающей транзакции

      public static void NewTransactionScope()
      {
         using (var scope = new TransactionScope())
         {
            Transaction.Current.TransactionCompleted += Current_TransactionCompleted;
            TxUtilities.DisplayTransactionInformation("Ambient TX created",
               Transaction.Current.TransactionInformation);
            using (var innerScope = new TransactionScope(TransactionScopeOption.RequiresNew))
               // Требуем новую независимую транзакцию
            {
               Transaction.Current.TransactionCompleted += Current_TransactionCompleted;
               TxUtilities.DisplayTransactionInformation("Inner transaction scope",
                  Transaction.Current.TransactionInformation);
               innerScope.Complete();
            }
            scope.Complete();
         }
      }

      #endregion


      static void Current_TransactionCompleted(object sender, TransactionEventArgs e)
      {
         TxUtilities.DisplayTransactionInformation("TX completed", e.Transaction.TransactionInformation);
      }
   }
}
