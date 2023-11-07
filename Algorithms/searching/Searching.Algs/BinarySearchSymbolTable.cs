using System.Diagnostics;

namespace Searching.Algs;

/// <summary>
///    Symbol table implementation with binary search in an ordered array
/// </summary>
/// <typeparam name="TKey">Key type</typeparam>
/// <typeparam name="TValue">Value type</typeparam>
public sealed class BinarySearchSymbolTable<TKey, TValue>
   where TKey : IComparable<TKey>
{
   private const int InitCapacity = 2;
   private static readonly EqualityComparer<TKey> KeyComparer = EqualityComparer<TKey>.Default;
   private static readonly EqualityComparer<TValue> ValueComparer = EqualityComparer<TValue>.Default;
   private TKey?[] _keys;
   private TValue?[] _values;

   /// <summary>
   ///    create an empty symbol table with given initial capacity
   /// </summary>
   /// <param name="capacity">Capacity</param>
   public BinarySearchSymbolTable(int capacity)
   {
      _keys = new TKey?[capacity];
      _values = new TValue?[capacity];
   }

   /// <summary>
   ///    Create an empty symbol table with default initial capacity
   /// </summary>
   public BinarySearchSymbolTable()
      : this(InitCapacity)
   {
   }

   /// <summary>
   ///    Size
   /// </summary>
   public int Size { get; private set; }

   /// <summary>
   ///    Is the symbol table empty?
   /// </summary>
   public bool IsEmpty => Size == 0;

   /// <summary>
   ///    The value associated with the given key, or null if no such key
   /// </summary>
   /// <param name="key">Key</param>
   public TValue? this[TKey? key]
   {
      get
      {
         if (IsEmpty)
         {
            return default;
         }

         var rankIdx = Rank(key);
         return InTable(key, rankIdx)
            ? _values[rankIdx]
            : default;
      }
   }

   public TKey? Max => IsEmpty ? default : _keys[Size - 1];

   public TKey? Min => IsEmpty ? default : _keys[0];

   public TKey? this[int index] =>
      index < 0 || index >= Size ? default : _keys[index];

   public IEnumerable<TKey?> Keys => GetKeys(Min, Max);

   /// <summary>
   ///    Compute the number of keys in the table that are smaller than given key
   /// </summary>
   /// <param name="key">Key</param>
   /// <returns>The number of keys in the table that are smaller than given key</returns>
   public int Rank(TKey? key)
   {
      var lowIdx = 0;
      var highIdx = Size - 1;
      while (lowIdx <= highIdx)
      {
         var middleIdx = lowIdx + (highIdx - lowIdx) / 2;
         var compareRes = key?.CompareTo(_keys[middleIdx]);
         switch (compareRes)
         {
            case < 0:
               highIdx = middleIdx - 1;
               break;

            case > 0:
               lowIdx = middleIdx + 1;
               break;

            default:
               return middleIdx;
         }
      }

      return lowIdx;
   }

   /// <summary>
   ///    Is the key in the table?
   /// </summary>
   /// <param name="key">Key</param>
   /// <returns>true if the key is in the table, false - otherwise</returns>
   public bool Contains(TKey? key) =>
      !ValueComparer.Equals(this[key], default);

   /// <summary>
   ///    Search for key. Update value if found; grow table if new
   /// </summary>
   /// <param name="key">Key</param>
   /// <param name="value">Value</param>
   public void Put(TKey? key, TValue? value)
   {
      if (ValueComparer.Equals(value, default))
      {
         Delete(key);
         return;
      }

      var rankIdx = Rank(key);

      // key is in the table
      if (InTable(key, rankIdx))
      {
         _values[rankIdx] = value;
         return;
      }

      // insert a new key-value pair
      if (Size == _keys.Length)
      {
         Resize(2 * _keys.Length);
      }

      for (var j = Size; j > rankIdx; j--)
      {
         _keys[j] = _keys[j - 1];
         _values[j] = _values[j - 1];
      }

      _keys[rankIdx] = key;
      _values[rankIdx] = value;
      Size++;
   }

   /// <summary>
   ///    Remove the key-value pair if present
   /// </summary>
   /// <param name="key">Key</param>
   public void Delete(TKey? key)
   {
      if (IsEmpty)
      {
         return;
      }

      // compute rank
      var rankIdx = Rank(key);

      // key is not in the table
      if (NotInTable(key, rankIdx))
      {
         return;
      }

      for (var j = rankIdx; j < Size - 1; j++)
      {
         _keys[j] = _keys[j + 1];
         _values[j] = _values[j + 1];
      }

      Size--;
      _keys[Size] = default;
      _values[Size] = default;

      // resize if 1/4 full
      if (Size > 0 && Size == _keys.Length / 4)
      {
         Resize(_keys.Length / 2);
      }
   }

   /// <summary>
   ///    Delete the minimum key and its associated value
   /// </summary>
   /// <exception cref="InvalidOperationException">Symbol table underflow</exception>
   public void DeleteMin()
   {
      if (IsEmpty)
      {
         throw new InvalidOperationException("Symbol table underflow error");
      }

      Delete(Min);
   }

   /// <summary>
   ///    Delete the maximum key and its associated value
   /// </summary>
   /// <exception cref="InvalidOperationException">Symbol table underflow</exception>
   public void DeleteMax()
   {
      if (IsEmpty)
      {
         throw new InvalidOperationException("Symbol table underflow error");
      }

      Delete(Max);
   }

   public TKey? Floor(TKey key)
   {
      var rankIdx = Rank(key);
      return InTable(key, rankIdx)
         ? _keys[rankIdx]
         : rankIdx == 0
            ? default
            : _keys[rankIdx - 1];
   }

   public TKey? Ceiling(TKey key)
   {
      var rankIdx = Rank(key);
      return rankIdx == Size
         ? default
         : _keys[rankIdx];
   }

   public int GetKeySize(TKey lowKey, TKey highKey)
   {
      if (lowKey.CompareTo(highKey) > 0)
      {
         return default;
      }

      var keySize = Rank(highKey) - Rank(lowKey);
      if (Contains(highKey))
      {
         keySize += 1;
      }

      return keySize;
   }

   public IEnumerable<TKey?> GetKeys(TKey? lowIdx, TKey? highIdx)
   {
      if (KeyComparer.Equals(lowIdx, default)
          && KeyComparer.Equals(highIdx, default))
      {
         return Enumerable.Empty<TKey>();
      }

      if (KeyComparer.Equals(lowIdx, default))
      {
         throw new ArgumentNullException(nameof(lowIdx));
      }

      if (KeyComparer.Equals(highIdx, default))
      {
         throw new ArgumentNullException(nameof(highIdx));
      }

      if (lowIdx?.CompareTo(highIdx) > 0)
      {
         return Enumerable.Empty<TKey>();
      }

      var queue = new Queue<TKey?>();
      for (var i = Rank(lowIdx); i < Rank(highIdx); i++)
      {
         queue.Enqueue(_keys[i]);
      }

      if (Contains(highIdx))
      {
         queue.Enqueue(_keys[Rank(highIdx)]);
      }

      return queue;
   }

   private bool NotInTable(TKey? key, int rankIdx) =>
      rankIdx == Size || _keys[rankIdx]?.CompareTo(key) != 0;

   private bool InTable(TKey? key, int rankIdx) =>
      rankIdx < Size && _keys[rankIdx]?.CompareTo(key) == 0;

   // resize the underlying arrays
   private void Resize(int capacity)
   {
      Debug.Assert(capacity >= Size);

      Array.Resize(ref _keys, capacity);
      Array.Resize(ref _values, capacity);
   }

   #region Integrity checking

   public bool Check() =>
      IsSorted() && RankCheck();

   private bool IsSorted()
   {
      for (var i = 1; i < Size; i++)
      {
         if (_keys[i]?.CompareTo(_keys[i - 1]) < 0)
         {
            return false;
         }
      }

      return true;
   }

   private bool RankCheck()
   {
      for (var i = 0; i < Size; i++)
      {
         if (i != Rank(this[i]))
         {
            return false;
         }
      }

      for (var i = 0; i < Size; i++)
      {
         if (_keys[i]?.CompareTo(this[Rank(_keys[i])]) != 0)
         {
            return false;
         }
      }

      return true;
   }

   #endregion
}