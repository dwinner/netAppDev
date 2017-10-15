using System.Threading;

namespace SharingState.Lib
{
   public class SmallBusinessReadWriteLock : SmallBusiness
   {
      private readonly ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();

      public override decimal NetWorth
      {
         get
         {
            _rwLock.EnterReadLock();
            try
            {
               return base.NetWorth;
            }
            finally
            {
               _rwLock.ExitReadLock();
            }
         }
      }

      public override void RaisedInvoiceFor(decimal amount)
      {
         _rwLock.EnterWriteLock();
         try
         {
            base.RaisedInvoiceFor(amount);
         }
         finally
         {
            _rwLock.ExitWriteLock();
         }
      }

      public override void ReceivePayments(decimal payment)
      {
         _rwLock.EnterWriteLock();
         try
         {
            base.ReceivePayments(payment);
         }
         finally
         {
            _rwLock.ExitWriteLock();
         }
      }
   }
}