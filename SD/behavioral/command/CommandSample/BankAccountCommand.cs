namespace CommandSample;

public class BankAccountCommand(BankAccount account, BankAccountCommand.Action action, int amount)
   : ICommand
{
   public enum Action
   {
      Deposit,
      Withdraw
   }

   private bool _succeeded;

   public void Call()
   {
      switch (action)
      {
         case Action.Deposit:
            account.Deposit(amount);
            _succeeded = true;
            break;

         case Action.Withdraw:
            _succeeded = account.Withdraw(amount);
            break;

         default:
            throw new ArgumentOutOfRangeException(nameof(action));
      }
   }

   public void Undo()
   {
      if (!_succeeded)
      {
         return;
      }

      switch (action)
      {
         case Action.Deposit:
            account.Withdraw(amount);
            break;

         case Action.Withdraw:
            account.Deposit(amount);
            break;

         default:
            throw new ArgumentOutOfRangeException(nameof(action));
      }
   }
}