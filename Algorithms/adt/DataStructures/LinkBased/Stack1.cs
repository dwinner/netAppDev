using System.Collections;
using System.Diagnostics;
using System.Text;

namespace DataStructures.LinkBased;

/// <summary>
///    The <tt>Stack</tt> class represents a last-in-first-out (LIFO) stack of generic items.
///    It supports the usual <em>push</em> and <em>pop</em> operations, along with methods
///    for peeking at the top item, testing if the stack is empty, and iterating through
///    the items in LIFO order.
///    <p>
///       This implementation uses a singly-linked list with a nested class for
///       linked-list nodes.
///       The <em>push</em>, <em>pop</em>, <em>peek</em>, <em>size</em>, and <em>is-empty</em>
///       operations all take constant time in the worst case.
///    </p>
/// </summary>
/// <typeparam name="TItem">Element type parameter</typeparam>
public sealed class Stack1<TItem> : IEnumerable<TItem>
{
   private Node<TItem>? _first; // top of stack

   /// <summary>
   ///    Initializes an empty stack
   /// </summary>
   public Stack1()
   {
      _first = default;
      Size = default;
   }

   /// <summary>
   ///    true if this stack is empty; false otherwise
   /// </summary>
   public bool IsEmpty => _first == null;

   /// <summary>
   ///    The number of items in the stack
   /// </summary>
   public int Size { get; private set; }

   public IEnumerator<TItem> GetEnumerator() => new ListEnumerator<TItem>(_first);

   IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

   /// <summary>
   ///    Adds the item to this stack
   /// </summary>
   /// <param name="anItem">The item to add</param>
   public void Push(TItem anItem)
   {
      var oldFirst = _first;
      _first = new Node<TItem>
      {
         Item = anItem,
         Next = oldFirst
      };
      Size++;
   }

   /// <summary>
   ///    Removes and returns the item most recently added to this stack
   /// </summary>
   /// <returns>The item most recently added</returns>
   /// <exception cref="InvalidOperationException">If this stack is empty</exception>
   public TItem Pop()
   {
      if (IsEmpty)
      {
         throw new InvalidOperationException("Stack underflow");
      }

      Debug.Assert(_first != null, nameof(_first) + " != null");
      var item = _first.Item;
      _first = _first.Next;
      Size--;

      return item ?? throw new InvalidOperationException("item is null");
   }

   /// <summary>
   ///    Returns (but does not remove) the item most recently added to this stack
   /// </summary>
   /// <returns>The item most recently added to this stack</returns>
   /// <exception cref="InvalidOperationException">If this stack is empty</exception>
   public TItem Peek()
   {
      if (IsEmpty)
      {
         throw new InvalidOperationException("Stack underflow");
      }

      if (_first != null && _first.Item != null)
      {
         return _first.Item;
      }

      throw new InvalidOperationException("item is null");
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

   private sealed record Node<T>
   {
      internal T? Item;
      internal Node<T>? Next;
   }

   /// <summary>
   ///    Returns an iterator to this stack that iterates through the items in LIFO order
   /// </summary>
   /// <typeparam name="T">Item parameter</typeparam>
   private sealed class ListEnumerator<T> : IEnumerator<T>
   {
      private Node<T>? _firstNode;

      public ListEnumerator(Node<T>? firstNode)
      {
         _firstNode = firstNode;
         Current = default!;
      }

      public bool MoveNext()
      {
         if (_firstNode == null)
         {
            return false;
         }

         Current = _firstNode.Item ?? throw new InvalidOperationException("item is null");
         _firstNode = _firstNode.Next;

         return true;
      }

      public void Reset()
      {
      }

      public T Current { get; private set; }

      object IEnumerator.Current => Current ?? throw new InvalidOperationException("item is null");

      public void Dispose()
      {
      }
   }
}