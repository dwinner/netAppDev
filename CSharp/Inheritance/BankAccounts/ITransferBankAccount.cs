﻿namespace BankAccounts
{
   public interface ITransferBankAccount : IBankAccount
   {
      bool TransferTo(IBankAccount destination, decimal amount);
   }
}