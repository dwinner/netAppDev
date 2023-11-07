using System.Collections;

namespace Sorting.Algs.PrioQ;

/// <summary>
///    The <tt>IndexMaxPQ</tt> class represents an indexed priority queue of generic keys.
///    It supports the usual <em>insert</em> and <em>delete-the-maximum</em>
///    operations, along with <em>delete</em> and <em>change-the-key</em>
///    methods. In order to let the client refer to items on the priority queue,
///    an integer between 0 and NMAX-1 is associated with each key; the client
///    uses this integer to specify which key to delete or change.
///    It also supports methods for peeking at a maximum key,
///    testing if the priority queue is empty, and iterating through
///    the keys.
/// </summary>
/// <remarks>
///    <p>
///       This implementation uses a binary heap along with an array to associate
///       keys with integers in the given range.
///       The <em>insert</em>, <em>delete-the-maximum</em>, <em>delete</em>,
///       <em>change-key</em>, <em>decrease-key</em>, and <em>increase-key</em>
///       operations take logarithmic time.
///       The <em>is-empty</em>, <em>size</em>, <em>max-index</em>, <em>max-key</em>, and <em>key-of</em>
///       operations take constant time.
///       Construction takes time proportional to the specified capacity
///    </p>
/// </remarks>
public sealed class IndexMaxPrioQueue<TKey> : IEnumerable<int>
   where TKey : IComparable<TKey>
{
   private readonly int[] _inversePrioQ; // inverse of pq - qp[pq[i]] = pq[qp[i]] = i
   private readonly TKey[] _keys; // keys[i] = priority of i
   private readonly int[] _prioQ; // binary heap using 1-based indexing
   private int _size; // the number of keys on the priority queue

   /// <summary>
   ///    Initializes an empty indexed priority queue with indices between 0 and NMAX-1
   /// </summary>
   /// <param name="nMax">The keys on the priority queue are index from 0 to <paramref name="nMax" /> - 1</param>
   public IndexMaxPrioQueue(int nMax)
   {
      _keys = new TKey[nMax + 1];
      _prioQ = new int[nMax + 1];
      _inversePrioQ = new int[nMax + 1];
      for (var i = 0; i <= nMax; i++)
      {
         _inversePrioQ[i] = -1;
      }
   }

   /// <summary>
   ///    true if the priority queue is empty; false otherwise
   /// </summary>
   public bool IsEmpty => _size == 0;

   /// <summary>
   ///    Return a maximum key
   /// </summary>
   /// <value>A maximum key</value>
   /// <exception cref="InvalidOperationException">If priority queue is empty</exception>
   public TKey MaxKey
   {
      get
      {
         if (_size == 0)
         {
            throw new InvalidOperationException("Priority queue underflow");
         }

         return _keys[_prioQ[1]];
      }
   }

   public IEnumerator<int> GetEnumerator() => new HeapIterator(this);

   IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

   /// <summary>
   ///    Is i an index on the priority queue?
   /// </summary>
   /// <param name="idx">An index</param>
   /// <returns>true is it's priority queue index</returns>
   private bool Contains(int idx) => _inversePrioQ[idx] != -1;

   /// <summary>
   ///    Associate key with index i
   /// </summary>
   /// <param name="idx">An index</param>
   /// <param name="key">the key to associate with index <paramref name="idx" /></param>
   /// <exception cref="InvalidOperationException">
   ///    if there already is an item associated with index <paramref name="idx" />
   /// </exception>
   public void Insert(int idx, TKey key)
   {
      if (Contains(idx))
      {
         throw new InvalidOperationException($"index {idx} is already in the priority queue");
      }

      _size++;
      _inversePrioQ[idx] = _size;
      _prioQ[_size] = idx;
      _keys[idx] = key;
      Swim(_size);
   }

   /// <summary>
   ///    Removes a maximum key and returns its associated index
   /// </summary>
   /// <returns>An index associated with a maximum key</returns>
   /// <exception cref="InvalidOperationException">If priority queue is empty</exception>
   public int DelMax()
   {
      if (_size == 0)
      {
         throw new InvalidOperationException("Priority queue underflow");
      }

      var min = _prioQ[1];
      Exchange(1, _size--);
      Sink(1);
      _inversePrioQ[min] = -1; // mark as deleted
      _keys[_prioQ[_size + 1]] = default!;
      _prioQ[_size + 1] = -1;

      return min;
   }

   /// <summary>
   ///    Returns the key associated with index <paramref name="idx" />
   /// </summary>
   /// <param name="idx">The index of the key to return</param>
   /// <returns>The key associated with index <paramref name="idx" /></returns>
   /// <exception cref="InvalidOperationException">no key is associated with index <paramref name="idx" /></exception>
   public TKey GetKeyOf(int idx)
   {
      if (!Contains(idx))
      {
         throw new InvalidOperationException("index is not in the priority queue");
      }

      return _keys[idx];
   }

   /// <summary>
   ///    Change the key associated with index <paramref name="idx" /> to the specified value
   /// </summary>
   /// <param name="idx">The index of the key to change</param>
   /// <param name="key">The key assocated with index <paramref name="idx" /> to this key</param>
   /// <exception cref="InvalidOperationException">If index <paramref name="idx" /> is not in the priority queue</exception>
   public void ChangeKey(int idx, TKey key)
   {
      if (!Contains(idx))
      {
         throw new InvalidOperationException($"index {idx} is not in the priority queue");
      }

      _keys[idx] = key;
      Swim(_inversePrioQ[idx]);
      Sink(_inversePrioQ[idx]);
   }

   /// <summary>
   ///    Increase the key associated with index <paramref name="idx" /> to the specified value
   /// </summary>
   /// <param name="idx">The index of the key to increase</param>
   /// <param name="key">The key assocated with index <paramref name="idx" /> to this key</param>
   /// <exception cref="ArgumentException">If index <paramref name="idx" /> is not in the priority queue</exception>
   /// <exception cref="InvalidOperationException">If the <paramref name="key" /> will not strictly increase the key</exception>
   public void IncreaseKey(int idx, TKey key)
   {
      if (!Contains(idx))
      {
         throw new ArgumentException($"index {idx} is not in the priority queue", nameof(idx));
      }

      if (_keys[idx].CompareTo(key) >= 0)
      {
         throw new InvalidOperationException(
            $"Calling {nameof(IncreaseKey)} with given argument {key} would not strictly increase the key");
      }

      _keys[idx] = key;
      Swim(_inversePrioQ[idx]);
   }

   /// <summary>
   ///    Decrease the key associated with index i to the specified value
   /// </summary>
   /// <param name="idx">The index of the key to decrease</param>
   /// <param name="key">Decrease the key assocated with index <paramref name="idx" /> to this key</param>
   /// <exception cref="ArgumentException">If index <paramref name="idx" /> is not in the priority queue</exception>
   /// <exception cref="InvalidOperationException">If the <paramref name="key" /> will not strictly decrease the key</exception>
   public void DecreaseKey(int idx, TKey key)
   {
      if (!Contains(idx))
      {
         throw new ArgumentException($"index {idx} is not in the priority queue", nameof(idx));
      }

      if (_keys[idx].CompareTo(key) <= 0)
      {
         throw new InvalidOperationException(
            $"Calling {nameof(DecreaseKey)} with given argument {key} would not strictly decrease the key");
      }

      _keys[idx] = key;
      Sink(_inversePrioQ[idx]);
   }

   /// <summary>
   ///    Remove the key associated with index <paramref name="i" />
   /// </summary>
   /// <param name="i">An index of the key to remove</param>
   /// <exception cref="ArgumentException">If index <paramref name="i" /> is not in the priority queue</exception>
   public void Delete(int i)
   {
      if (!Contains(i))
      {
         throw new ArgumentException($"index {i} is not in the priority queue", nameof(i));
      }

      var index = _inversePrioQ[i];
      Exchange(index, _size--);
      Swim(index);
      Sink(index);
      _keys[i] = default!;
      _inversePrioQ[i] = -1; // mark as removed
   }

   private bool Less(int idx1, int idx2) =>
      _keys[_prioQ[idx1]].CompareTo(_keys[_prioQ[idx2]]) < 0;

   private void Exchange(int idx1, int idx2)
   {
      (_prioQ[idx1], _prioQ[idx2]) = (_prioQ[idx2], _prioQ[idx1]);
      _inversePrioQ[_prioQ[idx1]] = idx1;
      _inversePrioQ[_prioQ[idx2]] = idx2;
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

   private sealed class HeapIterator : IEnumerator<int>
   {
      private readonly IndexMaxPrioQueue<TKey> _copy;

      public HeapIterator(IndexMaxPrioQueue<TKey> self)
      {
         _copy = new IndexMaxPrioQueue<TKey>(self._prioQ.Length - 1);
         for (var i = 1; i <= self._size; i++)
         {
            _copy.Insert(self._prioQ[i], self._keys[self._prioQ[i]]);
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

      public int Current { get; private set; }

      object IEnumerator.Current => Current;

      public void Dispose()
      {
      }
   }
}