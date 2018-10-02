using System.Data;

namespace DataTransactionsCastle
{
   public class InvoiceService
   {
      private bool _isRetry;

      public virtual void Save(Invoice anInvoice)
      {         
      }

      public void SaveRetry(Invoice anInvoice)
      {
         if (_isRetry)
            return;

         _isRetry = true;
         throw new DataException();
      }

      public void SaveFail(Invoice anInvoice)
      {
         throw new DataException();
      }
   }
}