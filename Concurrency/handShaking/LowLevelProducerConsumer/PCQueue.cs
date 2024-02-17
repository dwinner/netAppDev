namespace LowLevelProducerConsumer;
#nullable disable
public class PcQueue
{
   private readonly Queue<Action> _itemQ = new();
   private readonly object _locker = new();
   private readonly Thread[] _workers;

   public PcQueue(int workerCount)
   {
      _workers = new Thread[workerCount];

      // Create and start a separate thread for each worker
      for (var i = 0; i < workerCount; i++)
      {
         (_workers[i] = new Thread(Consume)).Start();
      }
   }

   public void Shutdown(bool waitForWorkers)
   {
      // Enqueue one null item per worker to make each exit.
      foreach (var _ in _workers)
      {
         EnqueueItem(null);
      }

      // Wait for workers to finish
      if (waitForWorkers)
      {
         foreach (var worker in _workers)
         {
            worker.Join();
         }
      }
   }

   public void EnqueueItem(Action item)
   {
      lock (_locker)
      {
         _itemQ.Enqueue(item); // We must pulse because we're
         Monitor.Pulse(_locker); // changing a blocking condition.
      }
   }

   private void Consume()
   {
      while (true) // Keep consuming until
      { // told otherwise.
         Action item;
         lock (_locker)
         {
            while (_itemQ.Count == 0)
            {
               Monitor.Wait(_locker);
            }

            item = _itemQ.Dequeue();
         }

         if (item == null)
         {
            return; // This signals our exit.
         }

         item(); // Execute item.
      }
   }
}