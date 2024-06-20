namespace CompositeCommand;

internal class MoneyTransferCommand : CompositeBankAccountCommand
{
   public MoneyTransferCommand(BankAccount from, BankAccount to, int amount)
   {
      AddRange(new[]
      {
         new BankAccountCommand(from, BankAccountCommand.Action.Withdraw, amount),
         new BankAccountCommand(to, BankAccountCommand.Action.Deposit, amount)
      });
   }

   public override bool Success { get; set; }

   public override void Call()
   {
      var ok = true;
      foreach (var cmd in this)
      {
         if (ok)
         {
            cmd.Call();
            ok = cmd.Success;
         }
         else
         {
            cmd.Success = false;
         }
      }
   }
}