using System.Collections.Concurrent;

namespace TaskBasedProdCons;

public class PcQueue : IDisposable
{
   private readonly BlockingCollection<Task> _taskQ = new();

   public PcQueue(int workerCount)
   {
      // Create and start a separate Task for each consumer:
      for (var i = 0; i < workerCount; i++)
      {
         Task.Factory.StartNew(Consume);
      }
   }

   public void Dispose()
   {
      _taskQ.CompleteAdding();
   }

   public Task EnqueueAsync(Action action, CancellationToken cancelToken = default)
   {
      var task = new Task(action, cancelToken);
      _taskQ.Add(task, cancelToken);
      return task;
   }

   public Task<TResult> EnqueueAsync<TResult>(Func<TResult> func, CancellationToken cancelToken = default)
   {
      var task = new Task<TResult>(func, cancelToken);
      _taskQ.Add(task, cancelToken);
      return task;
   }

   private void Consume()
   {
      foreach (var task in _taskQ.GetConsumingEnumerable())
      {
         try
         {
            if (!task.IsCanceled)
            {
               task.RunSynchronously();
            }
         }
         catch (InvalidOperationException)
         {
            // Preventing the race condition
         }
      }
   }
}