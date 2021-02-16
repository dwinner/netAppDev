using LinkedList.Lib;

namespace StackComposition.Lib
{
   public class StackComposition<TItem>
   {
      private const string DefaultName = "composed stack";
      private readonly List<TItem> _stack;

      public StackComposition() => _stack = new List<TItem>(DefaultName);

      public bool IsEmpty => _stack.IsEmpty;

      public void Push(TItem anItem) => _stack.InsertAtFront(anItem);

      public TItem Pop() => _stack.RemoveFromFront();

      public override string ToString() => _stack.ToString();
   }
}