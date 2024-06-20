namespace FunctionalCommand;

public class FunctionalCommand
{
   public FunctionalCommand()
   {
      var bankAccount = new BankAccount();
      var commands = new List<Action>
      {
         () => Deposit(bankAccount, 100),
         () => Withdraw(bankAccount, 100)
      };

      commands.ForEach(action => action());
   }

   public void Deposit(BankAccount account, int amount)
   {
      account.Balance += amount;
   }

   public void Withdraw(BankAccount account, int amount)
   {
      if (account.Balance >= amount)
      {
         account.Balance -= amount;
      }
   }

   public class BankAccount
   {
      public int Balance;
   }
}