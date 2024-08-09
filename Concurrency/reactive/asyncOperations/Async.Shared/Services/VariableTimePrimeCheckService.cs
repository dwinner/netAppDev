namespace Async.Shared.Services;

public class VariableTimePrimeCheckService(int numberToDelay) : PrimeCheckService
{
   public override async Task<bool> IsPrimeAsync(int number)
   {
      if (number == numberToDelay)
      {
         await Task.Delay(2_000).ConfigureAwait(false);
      }

      return await base.IsPrimeAsync(number).ConfigureAwait(false);
   }
}