namespace YaDynamicProxy;

public class BankAccount : IBankAccount
{
   private const int OverdraftLimit = -500;
   private int _balance;

   public void Deposit(int amount)
   {
      _balance += amount;
      Console.WriteLine($"Deposited ${amount}, balance is now {_balance}");
   }

   public bool Withdraw(int amount)
   {
      if (_balance - amount >= OverdraftLimit)
      {
         _balance -= amount;
         Console.WriteLine($"Withdrew ${amount}, balance is now {_balance}");
         return true;
      }

      return false;
   }

   public override string ToString() => $"{nameof(_balance)}: {_balance}";
}