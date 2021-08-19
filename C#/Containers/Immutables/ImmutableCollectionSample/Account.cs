namespace ImmutableCollectionSample
{
   public sealed class Account
   {
      public Account(string aName, decimal anAmount)
      {
         Name = aName;
         Amount = anAmount;
      }

      public string Name { get; }
      public decimal Amount { get; }
   }
}