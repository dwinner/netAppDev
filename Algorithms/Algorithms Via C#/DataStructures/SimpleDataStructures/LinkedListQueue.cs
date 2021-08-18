using System.Collections;
using System.Collections.Generic;

namespace SimpleDataStructures
{
   public sealed class LinkedListQueue<T> : IQueue<T>
   {
      private Node _first; // —сылка на самый "старый" узел
      private Node _last; // —сылка на самый "свежий" узел

      public IEnumerator<T> GetEnumerator() => new ListIterator(_first);

      IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

      public void Enqueue(T item)
      {
         var oldLast = _last;
         _last = new Node
         {
            Item = item,
            Next = null
         };
         if (IsEmpty)
            _first = _last;
         else
            oldLast.Next = _last;
         Size++;
      }

      public T Deque()
      {
         var item = _first.Item;
         _first = _first.Next;
         if (IsEmpty)
            _last = null;
         Size--;
         return item;
      }

      public int Size { get; private set; }

      public bool IsEmpty => _first == null;

      private sealed class Node
      {
         internal T Item;
         internal Node Next;
      }

      private sealed class ListIterator : IEnumerator<T>
      {
         private Node _current;

         public ListIterator(Node first)
         {
            _current = first;
         }

         public void Dispose()
         {
         }

         public bool MoveNext() => _current != null;

         public void Reset()
         {
         }

         public T Current
         {
            get
            {
               var item = _current.Item;
               _current = _current.Next;
               return item;
            }
         }

         object IEnumerator.Current => Current;
      }
   }
}