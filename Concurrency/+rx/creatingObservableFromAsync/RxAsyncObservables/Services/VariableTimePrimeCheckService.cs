namespace RxAsyncObservables.Services;

internal class VariableTimePrimeCheckService : PrimeCheckService
{
   private readonly int _numberToDelay;

   public VariableTimePrimeCheckService(int numberToDelay) => _numberToDelay = numberToDelay;

   public override async Task<bool> IsPrimeAsync(int number)
   {
      if (number == _numberToDelay)
      {
         await Task.Delay(2000).ConfigureAwait(false);
      }

      return await base.IsPrimeAsync(number).ConfigureAwait(false);
   }
}