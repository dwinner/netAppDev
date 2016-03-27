/**
 * Объединение блоков поставщика
 */

using System;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Joining
{
   internal static class Program
   {
      private static void Main()
      {
         TheAtomicCafe();

         SimpleJoin();

         InPairs();

         var host = new ServiceHost(typeof(NodeWorker));
         host.AddServiceEndpoint(typeof(IGridNode<WorkItem>), new NetTcpBinding(), "net.tcp://localhost:9000/Worker");
         host.Open();

         var grid = new GridDispatcher<WorkItem>(new NodeWorker());

         // grid.RegisterNode(new Uri("net.tcp://localhost:9000/Worker"));
         // grid.RegisterNode(new Uri("net.tcp://localhost:9000/Worker"));

         for (var i = 0; i < 10; i++)
         {
            grid.SubmitWork(new WorkItem
            {
               Lhs = i,
               Rhs = 4
            });
         }

         Console.ReadLine();
      }

      private static void InPairs()
      {
         var broadcastBlock = new BroadcastBlock<int>(i => i);

         var bufferOne = new BufferBlock<int>();
         var bufferTwo = new BufferBlock<int>();

         var firstJoinBlock = new JoinBlock<int, int>(new GroupingDataflowBlockOptions { Greedy = false });

         var consumer = new ActionBlock<Tuple<int, int>>(tuple =>
         {
            if (tuple.Item1 != tuple.Item2)
               Console.WriteLine(tuple);
         });

         bufferOne.LinkTo(firstJoinBlock.Target1);
         bufferTwo.LinkTo(firstJoinBlock.Target2);

         firstJoinBlock.LinkTo(consumer);

         broadcastBlock.LinkTo(bufferOne);
         broadcastBlock.LinkTo(bufferTwo);


         for (var nTask = 0; nTask < 4; nTask++)
         {
            var localTask = nTask;
            Task.Factory.StartNew(() =>
            {
               while (true)
               {
                  broadcastBlock.Post(localTask);
                  // Could cause join to pair incorrectly

                  //bufferOne.Post(localTask);
                  //bufferTwo.Post(localTask);
               }
               // ReSharper disable FunctionNeverReturns
            });
            // ReSharper restore FunctionNeverReturns
         }

         Console.ReadLine();
      }

      private static void SimpleJoin()
      {
         var bufferOne = new BufferBlock<int>();
         var bufferTwo = new BufferBlock<int>();

         var firstJoinBlock = new JoinBlock<int, int>(new GroupingDataflowBlockOptions { Greedy = false });

         var consumer = new ActionBlock<Tuple<int, int>>(tuple => { Console.WriteLine(tuple); });

         bufferOne.LinkTo(firstJoinBlock.Target1);
         bufferTwo.LinkTo(firstJoinBlock.Target2);

         firstJoinBlock.LinkTo(consumer);

         bufferOne.Post(1);
         bufferTwo.Post(1);

         Console.ReadLine();
      }

      private static void TheAtomicCafe()
      {
         var resteraunt = new Resteraunt(2);
         // var waiter = new Waiter(resteraunt);

         ConsoleKey key;
         var chef = new Chef(resteraunt);
         var id = 1;

         while ((key = Console.ReadKey(true).Key) != ConsoleKey.Q)
         {
            if (key == ConsoleKey.C)
            {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
               chef.MakeFoodAsync();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }

            if (key == ConsoleKey.N)
            {
               resteraunt.Customers.Post(new Customer(id++));
            }
         }
      }
   }
}