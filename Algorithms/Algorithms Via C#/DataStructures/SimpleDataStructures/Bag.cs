using System.Collections;
using System.Collections.Generic;

namespace SimpleDataStructures
{
   /// <summary>
   ///    Простейший контейнер
   /// </summary>
   public sealed class Bag<TItem> : IEnumerable<TItem>
   {
      private Node _first;

      public IEnumerator<TItem> GetEnumerator() => new ListIterator(_first);

      IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

      public void Add(TItem item)
      {
         var oldFirst = _first;
         _first = new Node
         {
            Item = item,
            Next = oldFirst
         };
      }

      private sealed class Node
      {
         internal TItem Item;
         internal Node Next;
      }

      private sealed class ListIterator : IEnumerator<TItem>
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

         public TItem Current
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