namespace NullObject;

internal static class Program
{
   private static void Main()
   {
      var log = Null<ILog>.Instance;
      var bankAccount = new BankAccount(log);
      bankAccount.Deposit(100);
      bankAccount.Withdraw(200);
   }
}