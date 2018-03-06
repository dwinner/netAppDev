/**
 * Определение и реализация интерфейсов
 */

using System;

namespace BankAccounts
{
   static class Program
   {
      static void Main()
      {
         IBankAccount venusAccount = new SaverAccount();
         ITransferBankAccount jupiterAccount = new CurrentAccount();
         venusAccount.PayIn(200);
         jupiterAccount.PayIn(500);
         jupiterAccount.TransferTo(venusAccount, 100);
         Console.WriteLine(venusAccount);
         Console.WriteLine(jupiterAccount);

         Console.ReadKey();
      }
   }
}
