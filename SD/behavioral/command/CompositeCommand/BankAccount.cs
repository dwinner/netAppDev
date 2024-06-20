namespace CompositeCommand;

public class BankAccount(int balance = 0)
{
   private const int OverdraftLimit = -500;

   public void Deposit(int amount)
   {
      balance += amount;
      Console.WriteLine($"Deposited ${amount}, balance is now {balance}");
   }

   public bool Withdraw(int amount)
   {
      if (balance - amount >= OverdraftLimit)
      {
         balance -= amount;
         Console.WriteLine($"Withdrew ${amount}, balance is now {balance}");
         return true;
      }

      return false;
   }

   public override string ToString() => $"{nameof(balance)}: {balance}";
}