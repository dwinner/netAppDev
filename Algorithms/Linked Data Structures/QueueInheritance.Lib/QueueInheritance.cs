using LinkedList.Lib;

namespace QueueInheritance.Lib
{
   public class QueueInheritance<TItem> : List<TItem>
   {
      private const string DefaultName = "queue";

      public QueueInheritance()
         : base(DefaultName)
      {
      }

      public void Enqueue(TItem anItem) => InsertAtBack(anItem);

      public TItem Dequeue() => RemoveFromFront();
   }
}