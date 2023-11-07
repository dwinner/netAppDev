using System.Collections;
using System.Diagnostics;

namespace DataStructures.Flat;

/// <summary>
///    The <tt>ResizingArrayStack</tt> class represents a last-in-first-out (LIFO) stack
///    of generic items.
///    It supports the usual <em>push</em> and <em>pop</em> operations, along with methods
///    for peeking at the top item, testing if the stack is empty, and iterating through
///    the items in LIFO order.
///    <p>
///       This implementation uses a resizing array, which double the underlying array
///       when it is full and halves the underlying array when it is one-quarter full.
///       The <em>push</em> and <em>pop</em> operations take constant amortized time.
///       The <em>size</em>, <em>peek</em>, and <em>is-empty</em> operations takes
///       constant time in the worst case.
///    </p>
/// </summary>
/// <typeparam name="TItem">Item type</typeparam>
public sealed class ResizingArrayStack<TItem> : IEnumerable<TItem>
{
   private const int DefaultInitSize = 2;
   private const int DefaultResizeFactor = 2;
   private TItem[] _items; // array of items

   /// <summary>
   ///    Initializes an empty stack
   /// </summary>
   public ResizingArrayStack() => _items = new TItem[DefaultInitSize];

   /// <summary>
   ///    true if this stack is empty; false otherwise
   /// </summary>
   public bool IsEmpty => Size == 0;

   /// <summary>
   ///    The number of items in the stack
   /// </summary>
   public int Size { get; private set; }

   public IEnumerator<TItem> GetEnumerator() => new ReverseArrayEnumerator<TItem>(Size, _items);

   IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

   private void Resize(int aCapacity)
   {
      Debug.Assert(aCapacity >= Size);

      // resize the underlying array holding the elements
      var tempItems = new TItem[aCapacity];
      Array.Copy(_items, tempItems, Size);
      _items = tempItems;
   }

   /// <summary>
   ///    Adds the item to this stack
   /// </summary>
   /// <param name="anItem">The item to add</param>
   public void Push(TItem anItem)
   {
      if (Size == _items.Length)
      {
         // resize array if necessary
         Resize(DefaultResizeFactor * _items.Length);
      }

      _items[Size++] = anItem;
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

      var itemToPop = _items[Size - 1];
      _items[Size - 1] = default!;
      Size--;

      // shrink size of array if necessary
      if (Size > 0 && Size == _items.Length / (DefaultResizeFactor * DefaultResizeFactor))
      {
         Resize(_items.Length / DefaultResizeFactor);
      }

      return itemToPop;
   }

   /// <summary>
   ///    Returns (but does not remove) the item most recently added to this stack
   /// </summary>
   /// <returns>The item most recently added to this stack</returns>
   /// <exception cref="InvalidOperationException">if this stack is empty</exception>
   public TItem Peek()
   {
      if (IsEmpty)
      {
         throw new InvalidOperationException("Stack underflow");
      }

      return _items[0];
   }

   /// <summary>
   ///    Reversible iterator
   /// </summary>
   /// <typeparam name="TElement">Element type</typeparam>
   private sealed class ReverseArrayEnumerator<TElement> : IEnumerator<TElement>
   {
      private readonly TElement[] _elements;
      private int _currentIndex;

      public ReverseArrayEnumerator(int currentIndex, TElement[] elements)
      {
         _currentIndex = currentIndex;
         _elements = elements;
      }

      public bool MoveNext()
      {
         if (_currentIndex == 0)
         {
            return false;
         }

         _currentIndex--;
         return true;
      }

      public void Reset()
      {
      }

      public TElement Current => _elements[_currentIndex];

      object IEnumerator.Current => Current ?? throw new InvalidOperationException("item is null");

      public void Dispose()
      {
      }
   }
}