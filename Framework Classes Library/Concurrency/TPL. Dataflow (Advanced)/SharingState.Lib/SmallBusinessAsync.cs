using System.Threading;
using System.Threading.Tasks;

namespace SharingState.Lib
{
   public class SmallBusinessAsync
   {
      private decimal _income;
      private decimal _receivables;
      private readonly ConcurrentExclusiveSchedulerPair _rwScheduler = new ConcurrentExclusiveSchedulerPair();

      public Task<decimal> NetWorthAsync
         => Task.Factory.StartNew(() => _income + _receivables, CancellationToken.None, TaskCreationOptions.None,
            _rwScheduler.ConcurrentScheduler);

      public Task RaisedInvoiceForAsync(decimal amount)
         => Task.Factory.StartNew(() => _receivables += amount, CancellationToken.None, TaskCreationOptions.None,
            _rwScheduler.ExclusiveScheduler);

      public Task ReceivePaymentAsync(decimal payment)
         => Task.Factory.StartNew(() =>
         {
            _income += payment;
            _receivables -= payment;
         }, CancellationToken.None, TaskCreationOptions.None, _rwScheduler.ExclusiveScheduler);
   }
}