using static System.Console;

namespace CompositeCommand;

internal class Program
{
   private static void Main()
   {
      var bankAccount = new BankAccount();
      var cmdDeposit = new BankAccountCommand(bankAccount, BankAccountCommand.Action.Deposit, 100);
      var cmdWithdraw = new BankAccountCommand(bankAccount, BankAccountCommand.Action.Withdraw, 1000);
      cmdDeposit.Call();
      cmdWithdraw.Call();
      WriteLine(bankAccount);
      cmdWithdraw.Undo();
      cmdDeposit.Undo();
      WriteLine(bankAccount);

      var from = new BankAccount();
      from.Deposit(100);
      var to = new BankAccount();

      var mtc = new MoneyTransferCommand(from, to, 1000);
      mtc.Call();

      // Deposited $100, balance is now 100
      // balance: 100
      // balance: 0

      WriteLine(from);
      WriteLine(to);
   }
}