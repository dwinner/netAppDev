namespace RxAsyncEnumerable;

internal class QueueClient : IDisposable
{
   public void Dispose()
   {
   }

   public Task<QueueItem> ReadNextItemAsync() => Task.FromResult(new QueueItem { Data = 1 });
}