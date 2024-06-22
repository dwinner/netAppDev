namespace NullObject;

public class BankAccount(ILog log)
{
   private int _balance;
   private readonly ILog _log = new OptionalLog(log);

   public void Deposit(int amount)
   {
      _balance += amount;
      // check for null everywhere
      _log?.Info($"Deposited ${amount}, balance is now {_balance}");
   }

   public void Withdraw(int amount)
   {
      if (_balance >= amount)
      {
         _balance -= amount;
         _log?.Info($"Withdrew ${amount}, we have ${_balance} left");
      }
      else
      {
         _log?.Warn($"Could not withdraw ${amount} because balance is only ${_balance}");
      }
   }
}