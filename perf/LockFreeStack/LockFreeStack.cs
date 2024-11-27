using System.Threading;

namespace LockFreeStack
{
   internal class LockFreeStack<T>
   {
      private int _count;
      private Node _head;

      public int Count => _count;

      public void Push(T value)
      {
         var newNode = new Node { Value = value };
         while (true)
         {
            newNode.Next = _head;
            if (Interlocked.CompareExchange(ref _head, newNode, newNode.Next) == newNode.Next)
            {
               Interlocked.Increment(ref _count);
               return;
            }
         }
      }

      public T Pop()
      {
         while (true)
         {
            var node = _head;
            if (node == null)
            {
               return default;
            }

            if (Interlocked.CompareExchange(ref _head, node.Next, node) == node)
            {
               Interlocked.Decrement(ref _count);
               return node.Value;
            }
         }
      }

      private class Node
      {
         public Node Next;
         public T Value;

         public override string ToString() => Value.ToString();
      }
   }
}