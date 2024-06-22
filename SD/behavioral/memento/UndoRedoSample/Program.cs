using static System.Console;

namespace UndoRedoSample;

internal class Program
{
   private static void Main()
   {
      var bankAccount = new BankAccount(100);
      bankAccount.Deposit(50);
      bankAccount.Deposit(25);
      WriteLine(bankAccount);

      bankAccount.Undo();
      WriteLine($"Undo 1: {bankAccount}");
      bankAccount.Undo();
      WriteLine($"Undo 2: {bankAccount}");
      bankAccount.Redo();
      WriteLine($"Redo 2: {bankAccount}");
   }
}