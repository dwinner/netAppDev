namespace RxAsyncObservables.Services;

internal class PrimeCheckService
{
   public virtual async Task<bool> IsPrimeAsync(int number) =>
      await Task.Run(() =>
      {
         for (var j = 2; j <= Math.Sqrt(number); j++)
         {
            if (number % j == 0)
            {
               return false;
            }
         }

         return true;
      }).ConfigureAwait(false);
}