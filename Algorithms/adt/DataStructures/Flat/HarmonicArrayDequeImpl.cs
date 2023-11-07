using System.Collections;
using static DataStructures.DequeUtils;

namespace DataStructures.Flat;

/// <summary>
///    Array based deque with harmonic behavior inside
/// </summary>
public sealed class HarmonicArrayDequeImpl<T> : IDeque<T>
{
   private const int DefaultSize = 4;
   private int _capacity;
   private T[] _items;
   private int _nextFreeHead; // the next free head index
   private int _nextFreeTail; // the next free tail index

   public HarmonicArrayDequeImpl(int capacity = DefaultSize)
   {
      _capacity = capacity <= 0 ? DefaultSize : capacity;
      _capacity = capacity;
      _items = new T[_capacity];
      Count = 0;
      _nextFreeHead = _capacity / 2 - 1;
      _nextFreeTail = _capacity / 2;
   }

   private bool IsEmpty => Count == 0;

   public int Count { get; private set; }

   public void EnqueueFirst(T item)
   {
      GrowIfNecessary();
      _items[_nextFreeHead] = item;
      _nextFreeHead--;
      Count++;
   }

   public void EnqueueLast(T item)
   {
      GrowIfNecessary();
      _items[_nextFreeTail] = item;
      _nextFreeTail++;
      Count++;
   }

   public T DequeueFirst()
   {
      if (IsEmpty)
      {
         throw new InvalidOperationException("Deque underflow occured");
      }

      T itemToDequeue;
      if (!SingleOrEmpty(_nextFreeTail, _nextFreeHead))
      {
         itemToDequeue = _items[++_nextFreeHead];
         _items[_nextFreeHead] = default!;
      }
      else
      {
         // There is only one element left
         itemToDequeue = FetchSingleItem();
      }

      Count--;

      return itemToDequeue;
   }

   public T DequeueLast()
   {
      if (IsEmpty)
      {
         throw new InvalidOperationException("Deque underflow occured");
      }

      T itemToDequeue;
      if (!SingleOrEmpty(_nextFreeTail, _nextFreeHead))
      {
         itemToDequeue = _items[--_nextFreeTail];
         _items[_nextFreeTail] = default!;
      }
      else
      {
         // There is only one element left
         itemToDequeue = FetchSingleItem();
      }

      Count--;

      return itemToDequeue;
   }

   public T PeekFirst()
   {
      if (IsEmpty)
      {
         throw new InvalidOperationException("Deque underflow occured");
      }

      var itemToPeek = !SingleOrEmpty(_nextFreeTail, _nextFreeHead)
         ? _items[ValidHead(_nextFreeHead)]
         : PeekSingleItem(); // There is only one element left

      return itemToPeek;
   }

   public T PeekLast()
   {
      if (IsEmpty)
      {
         throw new InvalidOperationException("Deque underflow occured");
      }

      var itemToPeek = !SingleOrEmpty(_nextFreeTail, _nextFreeHead)
         ? _items[ValidTail(_nextFreeTail)] // Have elements
         : PeekSingleItem(); // There is only one element left

      return itemToPeek;
   }

   public IEnumerator<T> GetEnumerator() =>
      _items.Where(item => item != null).GetEnumerator();

   IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

   private void GrowIfNecessary()
   {
      if (!NeedToGrow())
      {
         return;
      }

      var newCapacity = _capacity * 2;
      var newArray = new T[newCapacity];
      var shiftIdx = ShiftIndexFunc(newCapacity, Count);
      var validHead = ValidHead(_nextFreeHead);
      Array.Copy(_items, validHead, newArray, shiftIdx, Count);

      _items = newArray;
      _capacity = newCapacity;
      _nextFreeHead = shiftIdx - 1;
      _nextFreeTail = shiftIdx + Count;
   }

   private bool NeedToGrow() =>
      _nextFreeHead == 0 || _nextFreeTail == _capacity - 1;

   private T FetchSingleItem()
   {
      var validHead = ValidHead(_nextFreeHead);
      var itemFromHead = _items[validHead];
      var validTail = ValidTail(_nextFreeTail);
      var itemFromTail = _items[validTail];
      var itemToDequeue = itemFromHead != null
         ? _items[++_nextFreeHead]
         : itemFromTail != null
            ? _items[--_nextFreeTail]
            : throw new InvalidOperationException("Deque in corruption state");

      return itemToDequeue;
   }

   private T PeekSingleItem()
   {
      var validHead = ValidHead(_nextFreeHead);
      var validTail = ValidTail(_nextFreeTail);
      var itemFromHead = _items[validHead];
      var itemFromTail = _items[validTail];
      var itemToDequeue = itemFromHead != null
         ? _items[validHead]
         : itemFromTail != null
            ? _items[validTail]
            : throw new InvalidOperationException("Deque in corruption state");

      return itemToDequeue;
   }
}