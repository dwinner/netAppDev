using System.Collections;

namespace Sorting.Algs.PrioQ;

/// <summary>
///    The <tt>MinPQ</tt> class represents a priority queue of generic keys.
///    It supports the usual <em>insert</em> and <em>delete-the-minimum</em>
///    operations, along with methods for peeking at the minimum key,
///    testing if the priority queue is empty, and iterating through the keys.
///    <remarks>
///       <p>
///          This implementation uses a binary heap.
///          The <em>insert</em> and <em>delete-the-minimum</em> operations take
///          logarithmic amortized time.
///          The <em>min</em>, <em>size</em>, and <em>is-empty</em> operations take constant time.
///          Construction takes time proportional to the specified capacity or the number of
///          items used to initialize the data structure
///       </p>
///    </remarks>
/// </summary>
public sealed class MinPrioQueue<TKey> : IEnumerable<TKey>
   where TKey : IComparable<TKey>
{
   private const int DefaultCapacity = 1;
   private readonly IComparer<TKey> _comparer; // optional comparator
   private TKey[] _items; // store items at indices 1 to N

   /// <summary>
   ///    Initializes an empty priority queue with the given initial capacity
   /// </summary>
   /// <param name="initCapacity">The initial capacity of the priority queue</param>
   public MinPrioQueue(int initCapacity = DefaultCapacity)
   {
      _items = new TKey[initCapacity + 1];
      Count = 0;
      _comparer = Comparer<TKey>.Default;
   }

   /// <summary>
   ///    Initializes an empty priority queue with the given initial capacity, using the given comparator
   /// </summary>
   /// <param name="initCapacity">The initial capacity of the priority queue</param>
   /// <param name="comparer">The order to use when comparing keys</param>
   public MinPrioQueue(int initCapacity, IComparer<TKey> comparer)
      : this(initCapacity) =>
      _comparer = comparer;

   /// <summary>
   ///    Initializes an empty priority queue using the given comparator
   /// </summary>
   /// <param name="comparer">comparator the order to use when comparing keys</param>
   public MinPrioQueue(IComparer<TKey> comparer)
      : this(DefaultCapacity, comparer)
   {
   }

   /// <summary>
   ///    Initializes a priority queue from the array of keys.
   ///    Takes time proportional to the number of keys, using sink-based heap construction.
   /// </summary>
   /// <param name="keys">The array of keys</param>
   public MinPrioQueue(IReadOnlyList<TKey> keys)
   {
      Count = keys.Count;
      _items = new TKey[keys.Count + 1];
      _comparer = Comparer<TKey>.Default;

      for (var i = 0; i < Count; i++)
      {
         _items[i + 1] = keys[i];
      }

      for (var k = Count / 2; k >= 1; k--)
      {
         Sink(k);
      }
   }

   public int Count { get; private set; }

   /// <summary>
   ///    Is the priority queue empty?
   /// </summary>
   /// <value>true if the priority queue is empty; false otherwise</value>
   public bool IsEmpty => Count == 0;

   /// <summary>
   ///    Returns an iterator that iterates over the keys on the priority queue in ascending order
   /// </summary>
   /// <returns>The iterator that iterates over the keys in ascending order</returns>
   public IEnumerator<TKey> GetEnumerator() => new HeapEnumerator(this);

   IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

   /// <summary>
   ///    Adds a new key to the priority queue
   /// </summary>
   /// <param name="aKey">The key to add to the priority queue</param>
   public void Insert(TKey aKey)
   {
      // double size of array if necessary
      if (Count == _items.Length - 1)
      {
         Array.Resize(ref _items, 2 * _items.Length);
      }

      // add x, and percolate it up to maintain heap invariant
      _items[++Count] = aKey;
      Swim(Count);
   }

   /// <summary>
   ///    Removes and returns a smallest key on the priority queue
   /// </summary>
   /// <returns>The smallest key on the priority queue</returns>
   /// <exception cref="InvalidOperationException">if the priority queue is empty</exception>
   public TKey DelMin()
   {
      if (IsEmpty)
      {
         throw new InvalidOperationException("Priority queue underflow");
      }

      _items.Exchange(1, Count);
      var min = _items[Count--];
      Sink(1);
      _items[Count + 1] = default!; // avoid loitering and help with garbage collection
      if (Count > 0 && Count == _items.Length / 4)
      {
         Array.Resize(ref _items, _items.Length / 2);
      }

      return min;
   }

   private void Sink(int idx)
   {
      while (2 * idx <= Count)
      {
         var j = 2 * idx;
         if (j < Count && Greater(j, j + 1))
         {
            j++;
         }

         if (!Greater(idx, j))
         {
            break;
         }

         _items.Exchange(idx, j);
         idx = j;
      }
   }

   private void Swim(int idx)
   {
      while (idx > 1 && Greater(idx / 2, idx))
      {
         _items.Exchange(idx, idx / 2);
         idx /= 2;
      }
   }

   private bool Greater(int idx1, int idx2) =>
      _comparer.Compare(_items[idx1], _items[idx2]) > 0;

   // is subtree of pq[1..N] rooted at k a min heap?
   public bool IsMinHeap(int idx = 1)
   {
      if (idx > Count)
      {
         return true;
      }

      var left = 2 * idx;
      var right = 2 * idx + 1;
      if (left <= Count && Greater(idx, left))
      {
         return false;
      }

      if (right <= Count && Greater(idx, right))
      {
         return false;
      }

      return IsMinHeap(left) && IsMinHeap(right);
   }

   private sealed class HeapEnumerator : IEnumerator<TKey>
   {
      private readonly MinPrioQueue<TKey> _copy;

      public HeapEnumerator(MinPrioQueue<TKey> self)
      {
         Current = default!;
         _copy = new MinPrioQueue<TKey>(self.Count, self._comparer);
         for (var i = 1; i <= self.Count; i++)
         {
            _copy.Insert(self._items[i]);
         }
      }

      public bool MoveNext()
      {
         if (_copy.IsEmpty)
         {
            return false;
         }

         Current = _copy.DelMin();

         return true;
      }

      public void Reset()
      {
      }

      public TKey Current { get; private set; }

      object IEnumerator.Current => Current;

      public void Dispose()
      {
      }
   }
}