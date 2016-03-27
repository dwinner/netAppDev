namespace ifElseSwitchLinking
{
   public interface ILedgerEntry
   {
      bool IsCredit { get; }
      bool IsDebit { get; }
   };
}