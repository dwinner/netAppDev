using static System.Console;

namespace YaDynamicProxy;

internal static class Program
{
   private static void Main()
   {
      var bankAccount = Log<BankAccount>.As<IBankAccount>();

      bankAccount.Deposit(100);
      bankAccount.Withdraw(50);

      WriteLine(bankAccount);
   }
}