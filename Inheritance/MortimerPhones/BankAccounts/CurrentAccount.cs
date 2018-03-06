namespace BankAccounts
{
   public class CurrentAccount : ITransferBankAccount
   {
      public decimal Balance { get; private set; }

      public CurrentAccount() : this(0M) { }

      public CurrentAccount(decimal balance)
      {
         Balance = balance;
      }

      public void PayIn(decimal amount)
      {
         Balance += amount;
      }

      public bool Withdraw(decimal amount)
      {
         if (Balance < amount)
            return false;
         Balance -= amount;
         return true;
      }

      public bool TransferTo(IBankAccount destination, decimal amount)
      {
         if (!Withdraw(amount))
            return false;
         destination.PayIn(amount);
         return true;
      }

      public override string ToString()
      {
         return string.Format("Jupiter Bank Current Account: Balance = {0,6:C}", Balance);
      }
   }
}