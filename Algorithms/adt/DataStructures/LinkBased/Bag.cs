using System.Collections;
using System.Diagnostics;

namespace DataStructures.LinkBased;

/// <summary>
///    The <tt>Bag</tt> class represents a bag (or multiset) of
///    generic items. It supports insertion and iterating over the
///    items in arbitrary order.
///    <p>This implementation uses a singly-linked list with a static nested class Node</p>
///    The <em>Add</em>, <em>IsEmpty</em>, and <em>Size</em> operations
///    take constant time. Iteration takes time proportional to the number of items
/// </summary>
/// <typeparam name="TItem">Item type parameter</typeparam>
public sealed class Bag<TItem> : IEnumerable<TItem>
{
   private Node<TItem>? _first; // beginning of bag

   /// <summary>
   ///    Initialize an empty Bag
   /// </summary>
   public Bag()
   {
      _first = null;
      Size = 0;
   }

   /// <summary>
   ///    Is this bag empty?
   ///    <returns>true if this bag is empty; false otherwise</returns>
   /// </summary>
   public bool IsEmpty => _first == null;

   /// <summary>
   ///    Returns the number of items in this bag
   ///    <returns>The number of items in this bag</returns>
   /// </summary>
   public int Size { get; private set; }

   /// <summary>
   ///    Gets an iterator that iterates over the items in the bag
   /// </summary>
   /// <returns>Returns an iterator that iterates over the items in the bag</returns>
   public IEnumerator<TItem> GetEnumerator() => new ListEnumerator<TItem>(_first);

   IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

   /// <summary>
   ///    Adds an item to this bag
   /// </summary>
   /// <param name="anItem">An item to add to this bag</param>
   public void Add(TItem anItem)
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
   ///    Helper linked list class
   /// </summary>
   /// <typeparam name="T"></typeparam>
   private sealed class Node<T>
   {
      internal T? Item;

      internal Node<T>? Next;
   }

   /// <summary>
   ///    Iterator, doesn't implement remove() since it's optional
   /// </summary>
   /// <typeparam name="T">Item type</typeparam>
   private sealed class ListEnumerator<T> : IEnumerator<T>
   {
      private readonly Node<T>? _initialNode;
      private Node<T>? _currentNode;

      public ListEnumerator(Node<T>? currentNode)
      {
         _initialNode = _currentNode = currentNode;
         Debug.Assert(_currentNode != null, $"{nameof(_currentNode)} != null");
         Current = _currentNode.Item ?? throw new InvalidOperationException();
      }

      public bool MoveNext()
      {
         if (_currentNode == null)
         {
            return false;
         }

         Current = _currentNode.Item ?? throw new InvalidOperationException();
         _currentNode = _currentNode.Next;

         return true;
      }

      public void Reset() => _currentNode = _initialNode;

      public T Current { get; private set; }

      object IEnumerator.Current => Current ?? throw new InvalidOperationException();

      public void Dispose() => Reset();
   }
}