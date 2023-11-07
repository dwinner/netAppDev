using System.Collections;
using System.Diagnostics;

namespace Sorting.Algs.PrioQ;

/// <summary>
///    The <tt>IndexMinPQ</tt> class represents an indexed priority queue of generic keys.
///    It supports the usual <em>insert</em> and <em>delete-the-minimum</em>
///    operations, along with <em>delete</em> and <em>change-the-key</em>
///    methods. In order to let the client refer to keys on the priority queue,
///    an integer between 0 and NMAX-1 is associated with each key; the client
///    uses this integer to specify which key to delete or change.
///    It also supports methods for peeking at the minimum key,
///    testing if the priority queue is empty, and iterating through
///    the keys.
/// </summary>
/// <remarks>
///    <p>
///       This implementation uses a binary heap along with an array to associate
///       keys with integers in the given range.
///       The <em>insert</em>, <em>delete-the-minimum</em>, <em>delete</em>,
///       <em>change-key</em>, <em>decrease-key</em>, and <em>increase-key</em>
///       operations take logarithmic time.
///       The <em>is-empty</em>, <em>size</em>, <em>min-index</em>, <em>min-key</em>, and <em>key-of</em>
///       operations take constant time.
///       Construction takes time proportional to the specified capacity.
///    </p>
/// </remarks>
/// <typeparam name="TKey">Key type</typeparam>
public sealed class IndexMinPrioQueue<TKey> : IEnumerable<int>
   where TKey : IComparable<TKey>
{
   private readonly int[] _inversePrioQ; // inverse of pq: qp[pq[i]] = pq[qp[i]] = i
   private readonly TKey[] _keys; // keys[i] = priority of i
   private readonly int _nMax; // maximum number of elements on PQ
   private readonly int[] _prioQ; // binary heap using 1-based indexing
   private int _size; // The number of keys on the priority queue

   /// <summary>
   ///    Initializes an empty indexed priority queue with indices between 0 and <paramref name="nMax" /> - 1
   /// </summary>
   /// <param name="nMax">The keys on the priority queue are index from 0 to <paramref name="nMax" /> - 1</param>
   public IndexMinPrioQueue(int nMax)
   {
      Debug.Assert(nMax >= 0);

      _nMax = nMax;
      _keys = new TKey[nMax + 1]; // make this of length NMAX??
      _prioQ = new int[nMax + 1];
      _inversePrioQ = new int[nMax + 1]; // make this of length NMAX??
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
   ///    Returns an iterator that iterates over the keys on the  priority queue in ascending order
   /// </summary>
   /// <returns>Iterator that iterates over the keys on the  priority queue in ascending order</returns>
   public IEnumerator<int> GetEnumerator() => new HeapIterator(this);

   IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

   /// <summary>
   ///    Is i an index on the priority queue?
   /// </summary>
   /// <param name="idx">An index</param>
   /// <exception cref="ArgumentOutOfRangeException">If an index <paramref name="idx" /> is out of range</exception>
   /// <returns>true if contains</returns>
   public bool Contains(int idx)
   {
      if (idx < 0 || idx >= _nMax)
      {
         throw new ArgumentOutOfRangeException(nameof(idx));
      }

      return _inversePrioQ[idx] != -1;
   }

   /// <summary>
   ///    Associates key with index i
   /// </summary>
   /// <param name="idx">An index</param>
   /// <param name="key">The key to associate with index i</param>
   /// <exception cref="ArgumentException">If there already is an item associated with index i</exception>
   public void Insert(int idx, TKey key)
   {
      if (Contains(idx))
      {
         throw new ArgumentException($"{nameof(idx)} already there", nameof(idx));
      }

      _size++;
      _inversePrioQ[idx] = _size;
      _prioQ[_size] = idx;
      _keys[idx] = key;
      Swim(_size);
   }

   /// <summary>
   ///    Removes a minimum key and returns its associated index
   /// </summary>
   /// <returns>An index associated with a minimum key</returns>
   /// <exception cref="InvalidOperationException">If priority queue is empty</exception>
   public int DelMin()
   {
      if (IsEmpty)
      {
         throw new InvalidOperationException("Queue is empty");
      }

      var min = _prioQ[1];
      Exchange(1, _size--);
      Sink(1);
      _inversePrioQ[min] = -1; // mark as deleted
      _keys[_prioQ[_size + 1]] = default!;
      _prioQ[_size + 1] = -1; // not needed

      return min;
   }

   /// <summary>
   ///    Returns the key associated with index <paramref name="idx" />
   /// </summary>
   /// <param name="idx">The index of the key to return</param>
   /// <returns>The key associated with index <paramref name="idx" /></returns>
   /// <exception cref="ArgumentException">no key is associated with index <paramref name="idx" /></exception>
   public TKey GetKeyOf(int idx)
   {
      if (!Contains(idx))
      {
         throw new ArgumentException($"index {idx} is not in the priority queue", nameof(idx));
      }

      return _keys[idx];
   }

   /// <summary>
   ///    Change the key associated with index i to the specified value
   /// </summary>
   /// <param name="idx">The index of the key to change</param>
   /// <param name="key">Key assocated with index i to this key</param>
   /// <exception cref="ArgumentException">No key is associated with index i</exception>
   public void ChangeKey(int idx, TKey key)
   {
      if (!Contains(idx))
      {
         throw new ArgumentException($"index {idx} is not in the priority queue", nameof(idx));
      }

      _keys[idx] = key;
      Swim(_inversePrioQ[idx]);
      Sink(_inversePrioQ[idx]);
   }

   /// <summary>
   ///    Decrease the key associated with index i to the specified value
   /// </summary>
   /// <param name="idx">The index of the key to decrease</param>
   /// <param name="key">Decrease the key assocated with index i to this key</param>
   /// <exception cref="ArgumentException">No key is associated with index <paramref name="idx" /></exception>
   /// <exception cref="InvalidOperationException">If cannot decrease the key for index <paramref name="idx" /></exception>
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
      Swim(_inversePrioQ[idx]);
   }

   /// <summary>
   ///    Increase the key associated with index i to the specified value
   /// </summary>
   /// <param name="idx">The index of the key to increase</param>
   /// <param name="key">key assocated with index <paramref name="idx" /> to this key</param>
   /// <exception cref="ArgumentException">No key is associated with index <paramref name="idx" /></exception>
   /// <exception cref="InvalidOperationException">If cannot increase the key for index <paramref name="idx" /></exception>
   public void IncreaseKey(int idx, TKey key)
   {
      if (!Contains(idx))
      {
         throw new ArgumentException($"index {idx} is not in the priority queue", nameof(idx));
      }

      if (_keys[idx].CompareTo(key) >= 0)
      {
         throw new InvalidOperationException(
            $"Calling {nameof(DecreaseKey)} with given argument {key} would not strictly decrease the key");
      }

      _keys[idx] = key;
      Sink(_inversePrioQ[idx]);
   }

   /// <summary>
   ///    Remove the key associated with index <paramref name="idx" />
   /// </summary>
   /// <param name="idx">The index of the key to remove</param>
   /// <exception cref="ArgumentException">No key is associated with index <paramref name="idx" /></exception>
   public void Delete(int idx)
   {
      if (!Contains(idx))
      {
         throw new ArgumentException($"index {idx} is not in the priority queue", nameof(idx));
      }

      var index = _inversePrioQ[idx];
      Exchange(index, _size--);
      Swim(index);
      Sink(index);
      _keys[idx] = default!;
      _inversePrioQ[idx] = -1;
   }

   private void Swim(int idx)
   {
      while (idx > 1 && Greater(idx / 2, idx))
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
         if (j < _size && Greater(j, j + 1))
         {
            j++;
         }

         if (!Greater(idx, j))
         {
            break;
         }

         Exchange(idx, j);
         idx = j;
      }
   }

   private void Exchange(int idx1, int idx2)
   {
      (_prioQ[idx1], _prioQ[idx2]) = (_prioQ[idx2], _prioQ[idx1]);
      _inversePrioQ[_prioQ[idx1]] = idx1;
      _inversePrioQ[_prioQ[idx2]] = idx2;
   }

   private bool Greater(int i, int j) =>
      _keys[_prioQ[i]].CompareTo(_keys[_prioQ[j]]) > 0;

   private sealed class HeapIterator : IEnumerator<int>
   {
      private readonly IndexMinPrioQueue<TKey> _copy;

      public HeapIterator(IndexMinPrioQueue<TKey> self)
      {
         _copy = new IndexMinPrioQueue<TKey>(self._prioQ.Length - 1);
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

         Current = _copy.DelMin();

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