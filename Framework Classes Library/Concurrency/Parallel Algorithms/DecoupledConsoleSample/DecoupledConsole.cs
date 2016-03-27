using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace DecoupledConsoleSample
{
   public static class DecoupledConsole
   {
      private static readonly BlockingCollection<Action> BlockingQueue;
      private static readonly Task MessageWorkerTask;

      static DecoupledConsole()
      {
         BlockingQueue = new BlockingCollection<Action>();
         MessageWorkerTask = Task.Factory.StartNew(() =>
         {
            foreach (Action action in BlockingQueue.GetConsumingEnumerable())
            {
               action();
            }
         }, TaskCreationOptions.LongRunning);
      }

      public static Task MessageWorker
      {
         get { return MessageWorkerTask; }
      }

      public static void WriteLine(object value)
      {
         BlockingQueue.Add(() => Console.WriteLine(value));
      }

      public static void WriteLine(string format, params object[] values)
      {
         BlockingQueue.Add(() => Console.WriteLine(format, values));
      }
   }
}