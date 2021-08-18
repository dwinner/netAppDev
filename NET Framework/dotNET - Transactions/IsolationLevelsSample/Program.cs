/**
 * Уровни изоляции данных при транзакциях
 */

using System;
using System.Transactions;

namespace IsolationLevelsSample
{
   class Program
   {
      static void Main()
      {
         var txOptions = new TransactionOptions
         {
            IsolationLevel = IsolationLevel.ReadUncommitted,
            Timeout = TimeSpan.FromSeconds(90)
         };

         using (var scope = new TransactionScope(TransactionScopeOption.Required, txOptions))
         {
            // NOTE: Поведение: Читать данные без ожидания снятия блокировок из других транзакций;
            // возможны грязные чтения...
            scope.Complete();
         }
      }
   }
}
