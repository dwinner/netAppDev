using System.Collections;

namespace Sorting.Algs.PrioQ;

/// <summary>
///    The <tt>MaxPQ</tt> class represents a priority queue of generic keys.
///    It supports the usual <em>insert</em> and <em>delete-the-maximum</em>
///    operations, along with methods for peeking at the maximum key,
///    testing if the priority queue is empty, and iterating through
///    the keys.
/// </summary>
/// <remarks>
///    <p>
///       This implementation uses a binary heap.
///       The <em>insert</em> and <em>delete-the-maximum</em> operations take
///       logarithmic amortized time.
///       The <em>max</em>, <em>size</em>, and <em>is-empty</em> operations take constant time.
///       Construction takes time proportional to the specified capacity or the number of
///       items used to initialize the data structure
///    </p>
/// </remarks>
/// <typeparam name="TKey">Item type</typeparam>
public sealed class MaxPrioQueue<TKey> : IEnumerable<TKey>
{
   private const int DefaultCapacity = 1;
   private readonly IComparer<TKey> _comparer;
   private TKey[] _items; // store items at indices 1 to N
   private int _size; // The number of keys on the priority queue

   /// <summary>
   ///    Initializes an empty priority queue with the given initial capacity, using the given comparator
   /// </summary>
   /// <param name="anInitCapacity">The initial capacity of the priority queue</param>
   /// <param name="aComparer">the order in which to compare the keys</param>
   public MaxPrioQueue(int anInitCapacity, IComparer<TKey> aComparer)
   {
      _comparer = aComparer;
      _items = new TKey[anInitCapacity + 1];
      _size = default;
   }

   /// <summary>
   ///    Initializes an empty priority queue with the given initial capacity
   /// </summary>
   /// <param name="anInitCapacity">The initial capacity of the priority queue</param>
   public MaxPrioQueue(int anInitCapacity) : this(anInitCapacity, Comparer<TKey>.Default)
   {
   }

   /// <summary>
   ///    Initializes an empty priority queue
   /// </summary>
   public MaxPrioQueue() : this(DefaultCapacity)
   {
   }

   /// <summary>
   ///    Initializes an empty priority queue using the given comparator
   /// </summary>
   /// <param name="aComparer">The order in which to compare the keys</param>
   public MaxPrioQueue(IComparer<TKey> aComparer) : this(DefaultCapacity, aComparer)
   {
   }

   /// <summary>
   ///    Initializes a priority queue from the array of keys
   /// </summary>
   /// <remarks>Takes time proportional to the number of keys, using sink-based heap construction</remarks>
   /// <param name="keys">The array of keys</param>
   public MaxPrioQueue(IReadOnlyList<TKey> keys)
   {
      _comparer = Comparer<TKey>.Default;
      _size = keys.Count;
      _items = new TKey[_size + 1];
      for (var i = 0; i < _size; i++)
      {
         _items[i + 1] = keys[i];
      }

      for (var k = _size / 2; k >= 1; k--)
      {
         Sink(k);
      }
   }

   /// <summary>
   ///    Is the priority queue empty?
   /// </summary>
   public bool IsEmpty => _size == 0;

   public IEnumerator<TKey> GetEnumerator() => new HeapEnumerator(this);

   IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

   /// <summary>
   ///    Adds a new key to the priority queue
   /// </summary>
   /// <param name="key">The new key to be added to the priority queue</param>
   public void Insert(TKey key)
   {
      // double size of array if necessary
      if (_size >= _items.Length - 1)
      {
         Array.Resize(ref _items, 2 * _items.Length);
      }

      // add x, and percolate it up to maintain heap invariant
      _items[++_size] = key;
      Swim(_size);
   }

   /// <summary>
   ///    Removes and returns a largest key on the priority queue
   /// </summary>
   /// <returns>The largest key on the priority queue</returns>
   /// <exception cref="InvalidOperationException">If priority queue is empty</exception>
   public TKey DelMax()
   {
      if (IsEmpty)
      {
         throw new InvalidOperationException("Priority queue underflow");
      }

      var max = _items[1];
      _items.Exchange(1, _size--);
      Sink(1);
      _items[_size + 1] = default!;
      if (_size > 0 && _size == (_items.Length - 1) / 4)
      {
         Array.Resize(ref _items, _items.Length / 2);
      }

      return max;
   }

   private void Swim(int idx)
   {
      while (idx > 1 && Less(idx / 2, idx))
      {
         Exchange(idx, idx / 2);
         idx /= 2;
      }
   }

   private void Sink(int idx)
   {
      while (2 * idx <= _size)
      {
         var j = 2 * idx;
         if (j < _size && Less(j, j + 1))
         {
            j++;
         }

         if (!Less(idx, j))
         {
            break;
         }

         Exchange(idx, j);
         idx = j;
      }
   }

   private bool Less(int index1, int index2) =>
      _comparer.Compare(_items[index1], _items[index2]) < 0;

   private void Exchange(int index1, int index2) =>
      (_items[index1], _items[index2]) = (_items[index2], _items[index1]);

   public bool IsMaxHeap(int anIndex = 1)
   {
      if (anIndex > _size)
      {
         return true;
      }

      var left = 2 * anIndex;
      var right = 2 * anIndex + 1;
      if ((left <= _size && Less(anIndex, left)) || (right <= _size && Less(anIndex, right)))
      {
         return false;
      }

      return IsMaxHeap(left) && IsMaxHeap(right);
   }

   private sealed class HeapEnumerator : IEnumerator<TKey>
   {
      private readonly MaxPrioQueue<TKey> _copy;

      public HeapEnumerator(MaxPrioQueue<TKey> self)
      {
         Current = default!;
         _copy = new MaxPrioQueue<TKey>(self._size, self._comparer);
         for (var i = 1; i <= self._size; i++)
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

         Current = _copy.DelMax();

         return true;
      }

      public void Reset()
      {
      }

      public TKey Current { get; private set; }

      object IEnumerator.Current => Current ?? throw new InvalidOperationException($"{nameof(Current)} is null");

      public void Dispose()
      {
      }
   }
}