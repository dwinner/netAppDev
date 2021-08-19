namespace BankAccounts
{
   public class SaverAccount : IBankAccount
   {
      public decimal Balance { get; private set; }

      public SaverAccount() : this(0M) { }

      public SaverAccount(decimal balance)
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

      public override string ToString()
      {
         return string.Format("Venus Bank Saver: Balance = {0, 6:C}", Balance);
      }
   }
}