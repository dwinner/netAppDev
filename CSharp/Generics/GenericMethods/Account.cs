namespace GenericMethods
{
   public class Account : IAccount
   {
      public decimal Balance { get; private set; }

      public string Name { get; private set; }

      public Account(string name, decimal balance)
      {
         Name = name;
         Balance = balance;
      }
   }
}