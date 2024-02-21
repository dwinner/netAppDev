using System.Collections.Concurrent;

namespace ProdConsQueue;

public class PcQueue : IDisposable
{
   private readonly BlockingCollection<Action> _taskQ = new();

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

   public void EnqueueTask(Action action)
   {
      _taskQ.Add(action);
   }

   private void Consume()
   {
      // This sequence that we’re enumerating will block when no elements
      // are available and will end when CompleteAdding is called.

      foreach (var action in _taskQ.GetConsumingEnumerable())
      {
         action(); // Perform task.
      }
   }
}