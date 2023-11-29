namespace RxAsyncEnumerable;

internal class QueueItemsEnumerator : IAsyncEnumerator<QueueItem>
{
   private readonly CancellationToken _cancellationToken;
   private readonly QueueClient _client;

   public QueueItemsEnumerator(QueueClient client, CancellationToken cancellationToken)
   {
      _client = client;
      _cancellationToken = cancellationToken;
   }

   public async ValueTask<bool> MoveNextAsync() =>
      await InternalMoveNextAsync(_cancellationToken).ConfigureAwait(false);

   public QueueItem Current { get; private set; }

   public ValueTask DisposeAsync()
   {
      _client.Dispose();
      return ValueTask.CompletedTask;
   }

   public void Dispose()
   {
      _client.Dispose();
   }

   private async Task<bool> InternalMoveNextAsync(CancellationToken cancellationToken)
   {
      if (cancellationToken.IsCancellationRequested)
      {
         return false;
      }

      await Task.Delay(2000, cancellationToken).ConfigureAwait(false);
      Current = await _client.ReadNextItemAsync().ConfigureAwait(false);

      return true;
   }
}