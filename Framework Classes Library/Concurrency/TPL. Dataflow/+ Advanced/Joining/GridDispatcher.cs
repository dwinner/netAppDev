using System;
using System.ServiceModel;
using System.Threading.Tasks.Dataflow;

namespace Joining
{
   public class GridDispatcher<T>
   {
      private readonly BufferBlock<Uri> _nodes = new BufferBlock<Uri>();

      private readonly JoinBlock<Uri, T> _scheduler =
         new JoinBlock<Uri, T>(new GroupingDataflowBlockOptions {Greedy = false});

      private readonly BufferBlock<T> _workItems = new BufferBlock<T>();

      public GridDispatcher(IGridNode<T> localService)
      {
         var localDispatcher = new ActionBlock<T>(wi =>
         {
            Console.WriteLine("Executing Locally");
            return localService.InvokeAsync(wi);
         },
            new ExecutionDataflowBlockOptions {BoundedCapacity = 2, MaxDegreeOfParallelism = 2});

         var dispatcher = new ActionBlock<Tuple<Uri, T>>((Action<Tuple<Uri, T>>) Dispatch);

         _workItems.LinkTo(localDispatcher);
         _workItems.LinkTo(_scheduler.Target2);
         _nodes.LinkTo(_scheduler.Target1);
         _scheduler.LinkTo(dispatcher);
      }

      public void RegisterNode(Uri uri)
      {
         _nodes.Post(uri);
      }

      public void SubmitWork(T workItem)
      {
         _workItems.Post(workItem);
      }

      private void Dispatch(Tuple<Uri, T> nodeAndWorkItemPair)
      {
         var cf = new ChannelFactory<IGridNode<T>>(new NetTcpBinding(), new EndpointAddress(nodeAndWorkItemPair.Item1));

         var proxy = cf.CreateChannel();
         proxy.InvokeAsync(nodeAndWorkItemPair.Item2)
            .ContinueWith(t =>
            {
               // ReSharper disable SuspiciousTypeConversion.Global
               ((IClientChannel) proxy).Close();
               // ReSharper restore SuspiciousTypeConversion.Global
               _nodes.Post(nodeAndWorkItemPair.Item1);
            });
      }
   }
}