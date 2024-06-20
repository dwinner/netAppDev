namespace CommandSample;

internal class Program
{
   private static void Main()
   {
      var bankAccount = new BankAccount();
      var cmd = new BankAccountCommand(bankAccount, BankAccountCommand.Action.Deposit, 100);
      cmd.Call();
      Console.WriteLine(bankAccount);
      var commands = new List<BankAccountCommand>
      {
         new(bankAccount, BankAccountCommand.Action.Deposit, 100),
         new(bankAccount, BankAccountCommand.Action.Withdraw, 1000)
      };

      Console.WriteLine(bankAccount);

      foreach (var command in commands)
      {
         command.Call();
      }

      Console.WriteLine(bankAccount);

      foreach (var command in Enumerable.Reverse(commands))
      {
         command.Undo();
      }

      Console.WriteLine(bankAccount);
   }
}