using System.Collections;
using System.Collections.Generic;

namespace SimpleDataStructures
{
   /// <summary>
   ///    Стек на основе связного списка
   /// </summary>
   public sealed class LinkedListStack<T> : IStack<T>
   {
      private Node _first;

      public void Push(T anItem)
      {
         var oldFirst = _first;
         _first = new Node
         {
            Item = anItem,
            Next = oldFirst
         };
         Size++;
      }

      public T Pop()
      {
         var item = _first.Item;
         _first = _first.Next;
         Size--;
         return item;
      }

      public bool IsEmpty => _first == null;

      public int Size { get; private set; }

      public IEnumerator<T> GetEnumerator() => new ListIterator(_first);

      IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

      private sealed class Node // Вложенный класс для определения узлов
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