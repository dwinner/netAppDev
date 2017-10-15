namespace SharingState.Lib
{
   public class SmallBusiness
   {
      private decimal _income;
      private decimal _receivables;

      public virtual decimal NetWorth => _income + _receivables;

      public virtual void RaisedInvoiceFor(decimal amount)
      {
         _receivables += amount;
      }

      public virtual void ReceivePayments(decimal payment)
      {
         _income += payment;
         _receivables -= payment;
      }
   }
}