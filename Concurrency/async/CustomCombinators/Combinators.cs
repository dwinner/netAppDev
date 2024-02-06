namespace CustomCombinators;

public static class Combinators
{
   public static async Task<TResult> WithTimeoutAsync<TResult>(this Task<TResult> self,
      TimeSpan timeout)
   {
      var winner = await Task.WhenAny(self, Task.Delay(timeout))
         .ConfigureAwait(false);
      if (winner != self)
      {
         throw new TimeoutException();
      }

      return await self.ConfigureAwait(false); // Unwrap result/re-throw
   }

   public static Task<TResult> WithCancellationAsync<TResult>(this Task<TResult> self,
      CancellationToken cancellationToken)
   {
      var tcs = new TaskCompletionSource<TResult>();
      var reg = cancellationToken.Register(() => tcs.TrySetCanceled());
      self.ContinueWith(ant =>
      {
         reg.Dispose();
         if (ant.IsCanceled)
         {
            tcs.TrySetCanceled();
         }
         else if (ant.IsFaulted)
         {
            tcs.TrySetException(ant.Exception.InnerException
                                ?? throw new InvalidOperationException());
         }
         else
         {
            tcs.TrySetResult(ant.Result);
         }
      }, cancellationToken);

      return tcs.Task;
   }

   public static async Task<TResult[]> WhenAllOrErrorAsync<TResult>(this Task<TResult>[] tasks)
   {
      var killJoy = new TaskCompletionSource<TResult[]>();
      foreach (var task in tasks)
      {
         task.ContinueWith(ant =>
         {
            if (ant.IsCanceled)
            {
               killJoy.TrySetCanceled();
            }
            else if (ant.IsFaulted)
            {
               killJoy.TrySetException(ant.Exception.InnerException 
                                       ?? throw new InvalidOperationException());
            }
         });
      }

      return await (await Task.WhenAny(killJoy.Task, Task.WhenAll(tasks))
            .ConfigureAwait(false))
         .ConfigureAwait(false);
   }
}