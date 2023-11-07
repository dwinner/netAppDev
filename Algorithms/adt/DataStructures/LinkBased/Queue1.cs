using System.Collections;
using System.Text;

namespace DataStructures.LinkBased;

/// <summary>
///    The <tt>Queue</tt> class represents a first-in-first-out (FIFO)
///    queue of generic items.
///    <remarks>
///       It supports the usual <em>Enqueue</em> and <em>Dequeue</em>
///       operations, along with methods for peeking at the first item,
///       testing if the queue is empty, and iterating through the items in FIFO order.
///       <p>
///          This implementation uses a singly-linked list with a nested class for
///          linked-list nodes. The <em>Enqueue</em>, <em>Dequeue</em>, <em>Peek</em>, <em>Size</em>, and <em>IsEmpty</em>
///          operations all take constant time in the worst case.
///       </p>
///    </remarks>
/// </summary>
public sealed class Queue1<TItem> : IEnumerable<TItem>
{
   private Node<TItem>? _first; // beginning of queue
   private Node<TItem>? _last; // end of queue    

   /// <summary>
   ///    Initializes an empty queue
   /// </summary>
   public Queue1()
   {
      _first = null;
      _last = null;
      Size = 0;
   }

   /// <summary>
   ///    Is this queue empty
   /// </summary>
   public bool IsEmpty => _first == null;

   /// <summary>
   ///    Returns the number of items in this queue
   /// </summary>
   public int Size { get; private set; }

   public IEnumerator<TItem> GetEnumerator() => new ListEnumerator<TItem>(_first);

   IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

   /// <summary>
   ///    Returns the item least recently added to this queue
   /// </summary>
   /// <returns>The item least recently added to this queue</returns>
   /// <exception cref="InvalidOperationException">If this queue is empty</exception>
   public TItem? Peek()
   {
      if (IsEmpty)
      {
         throw new InvalidOperationException("Queue underflow");
      }

      return _first != null ? _first.Item : default;
   }

   /// <summary>
   ///    Adds the item to this queue
   /// </summary>
   /// <param name="anItem">The item to add</param>
   public void Enqueue(in TItem anItem)
   {
      var oldLast = _last;
      _last = new Node<TItem>
      {
         Item = anItem,
         Next = null
      };
      if (IsEmpty)
      {
         _first = _last;
      }
      else
      {
         if (oldLast != null)
         {
            oldLast.Next = _last;
         }
      }

      Size++;
   }

   /// <summary>
   ///    Removes and returns the item on this queue that was least recently added
   /// </summary>
   /// <returns>The item on this queue that was least recently added</returns>
   /// <exception cref="InvalidOperationException">If this queue is empty</exception>
   public TItem Dequeue()
   {
      if (IsEmpty)
      {
         throw new InvalidOperationException("Queue underflow");
      }

      var item = _first!.Item;
      _first = _first.Next;
      Size--;
      if (IsEmpty)
      {
         _last = null; // to avoid loitering
      }

      return item ?? throw new InvalidOperationException("Dequeueing null item");
   }

   public override string ToString()
   {
      var strBuilder = new StringBuilder();
      foreach (var item in this)
      {
         strBuilder.Append($"{item} ");
      }

      return strBuilder.ToString();
   }

   /// <summary>
   ///    Helper linked list class
   /// </summary>
   /// <typeparam name="T">Type parameter</typeparam>
   private sealed class Node<T>
   {
      internal T? Item;
      internal Node<T>? Next;
   }

   /// <summary>
   ///    Iterator, doesn't implement Reset() since it's optional
   /// </summary>
   /// <typeparam name="T">Type parameter</typeparam>
   private sealed class ListEnumerator<T> : IEnumerator<T>
   {
      private Node<T>? _current;

      public ListEnumerator(Node<T>? aFirst) => _current = aFirst;

      public bool MoveNext()
      {
         if (_current == null)
         {
            return false;
         }

         Current = _current.Item ?? default!;
         _current = _current.Next;

         return true;
      }

      public void Reset()
      {
      }

      public T Current { get; private set; } = default!;

      object IEnumerator.Current => Current ?? default!;

      public void Dispose()
      {
      }
   }
}