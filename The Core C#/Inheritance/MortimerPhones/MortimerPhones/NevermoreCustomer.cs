namespace MortimerPhones
{
   public class NevermoreCustomer : GenericCustomer
   {
      public string ReferrerName { get; set; }

      // ReSharper disable once UnusedField.Compiler
      private uint _highCostMinutesUsed;

      public NevermoreCustomer(string name) : this(name, "<None>") { }

      private NevermoreCustomer(string name, string referrerName)
         : base(name)
      {
         ReferrerName = referrerName;
      }
   }
}