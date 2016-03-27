using System;
using System.Threading.Tasks;

namespace Joining
{
   public class NodeWorker : IGridNode<WorkItem>
   {
      public Task InvokeAsync(WorkItem workItem)
      {
         var result = workItem.Lhs + workItem.Rhs;
         Console.WriteLine(result);
         return Task.Delay(3000);
      }
   }
}