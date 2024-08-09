namespace RxAsyncEnumerable;

internal class QueueItems : IAsyncEnumerable<QueueItem>
{
   public IAsyncEnumerator<QueueItem> GetAsyncEnumerator(CancellationToken cancellationToken = new()) =>
      GetEnumerator();

   public IAsyncEnumerator<QueueItem> GetEnumerator() =>
      new QueueItemsEnumerator(new QueueClient(), CancellationToken.None);
}