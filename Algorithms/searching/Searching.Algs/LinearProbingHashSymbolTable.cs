// Symbol-table implementation with linear-probing hash table.

namespace Searching.Algs;

/// <summary>
///    The <see cref="LinearProbingHashSymbolTable{TKey,TValue}" /> class represents a symbol table of generic
///    key-value pairs.
///    It supports the usual <em>put</em>, <em>get</em>, <em>contains</em>,
///    <em>delete</em>, <em>size</em>, and <em>is-empty</em> methods.
///    It also provides a <em>keys</em> method for iterating over all of the keys.
///    A symbol table implements the <em>associative array</em> abstraction:
///    when associating a value with a key that is already in the symbol table,
///    the convention is to replace the old value with the new value.
///    Unlike {@link java.util.Map}, this class uses the convention that
///    values cannot be {@code null}—setting the
///    value associated with a key to {@code null} is equivalent to deleting the key
///    from the symbol table.
///    <p>
///       This implementation uses a linear probing hash table. It requires that
///       the key type overrides the {@code equals()} and {@code hashCode()} methods.
///       The expected time per <em>put</em>, <em>contains</em>, or <em>remove</em>
///       operation is constant, subject to the uniform hashing assumption.
///       The <em>size</em>, and <em>is-empty</em> operations take constant time.
///       Construction takes constant time.
///    </p>
/// </summary>
/// <typeparam name="TKey">Key</typeparam>
/// <typeparam name="TValue">Value</typeparam>
public sealed class LinearProbingHashSymbolTable<TKey, TValue>
{
   private const int InitCapacity = 0x4; // must be a power of 2
   private static readonly EqualityComparer<TKey?> KeyComparer = EqualityComparer<TKey?>.Default;
   private static readonly EqualityComparer<TValue?> ValueComparer = EqualityComparer<TValue?>.Default;
   private int _capacity; // size of linear probing table
   private TKey?[] _keys; // the keys
   private TValue?[] _values; // the values

   /// <summary>
   ///    Initializes an empty symbol table with the specified initial capacity.
   /// </summary>
   /// <param name="capacity">the initial capacity</param>
   public LinearProbingHashSymbolTable(int capacity)
   {
      _capacity = capacity;
      Size = 0;
      _keys = new TKey?[_capacity];
      _values = new TValue?[_capacity];
   }

   /// <summary>
   ///    Initializes an empty symbol table.
   /// </summary>
   public LinearProbingHashSymbolTable()
      : this(InitCapacity)
   {
   }

   /// <summary>
   ///    The number of key-value pairs in this symbol table
   /// </summary>
   public int Size { get; private set; }

   /// <summary>
   ///    Returns true if this symbol table is empty.
   /// </summary>
   public bool IsEmpty => Size == 0;

   /// <summary>
   ///    Returns the value associated with the specified key.
   /// </summary>
   /// <param name="key">The key</param>
   /// <returns>The value associated with the specified key.</returns>
   /// <exception cref="ArgumentNullException">If <paramref name="key" /> is null</exception>
   public TValue? this[TKey? key]
   {
      get
      {
         if (key == null)
         {
            throw new ArgumentNullException(nameof(key));
         }

         for (var hashIdx = ComputeHash(key);
              _keys[hashIdx] != null;
              hashIdx = (hashIdx + 1) % _capacity)
         {
            if (KeyComparer.Equals(_keys[hashIdx], key))
            {
               return _values[hashIdx];
            }
         }

         return default;
      }
      set
      {
         if (key == null)
         {
            throw new ArgumentNullException(nameof(key));
         }

         if (ValueComparer.Equals(value, default))
         {
            Delete(key);
            return;
         }

         // double table size if 50% full
         if (Size >= _capacity / 2)
         {
            Resize(2 * _capacity);
         }

         int freeIndex;
         for (freeIndex = ComputeHash(key);
              _keys[freeIndex] != null;
              freeIndex = (freeIndex + 1) % _capacity)
         {
            if (KeyComparer.Equals(_keys[freeIndex], key))
            {
               _values[freeIndex] = value;
               return;
            }
         }

         _keys[freeIndex] = key;
         _values[freeIndex] = value;
         Size++;
      }
   }

   /// <summary>
   ///    Returns all keys in this symbol table as an <see cref="IEnumerable{T}" />
   /// </summary>
   public IEnumerable<TKey?> Keys
   {
      get
      {
         for (var i = 0; i < _capacity; i++)
         {
            if (_keys[i] != null)
            {
               yield return _keys[i];
            }
         }
      }
   }

   /// <summary>
   ///    Returns true if this symbol table contains the specified key.
   /// </summary>
   /// <param name="key">Key</param>
   /// <returns>true if this symbol table contains the specified key.</returns>
   /// <exception cref="ArgumentNullException">If key is null</exception>
   public bool Contains(TKey? key)
   {
      if (key == null)
      {
         throw new ArgumentNullException(nameof(key));
      }

      return !ValueComparer.Equals(this[key], default);
   }

   // hash function for keys - returns value between 0 and m-1
   // private int GetHashTextbook(TKey key) => (key.GetHashCode() & 0x7fffffff) % m;

   // hash function for keys - returns value between 0 and m-1 (assumes m is a power of 2)
   private int ComputeHash(TKey? key)
   {
      var hashCode = key?.GetHashCode()
                     ?? throw new ArgumentNullException(nameof(key));
      hashCode ^= (hashCode >>> 20) ^ (hashCode >>> 12) ^ (hashCode >>> 7) ^ (hashCode >>> 4);

      return hashCode & (_capacity - 1);
   }

   // resizes the hash table to the given capacity by re-hashing all of the keys
   private void Resize(int capacity)
   {
      var tmpSymTab = new LinearProbingHashSymbolTable<TKey?, TValue?>(capacity);
      for (var i = 0; i < _capacity; i++)
      {
         if (_keys[i] != null)
         {
            tmpSymTab[_keys[i]] = _values[i];
         }
      }

      _keys = tmpSymTab._keys;
      _values = tmpSymTab._values;
      _capacity = tmpSymTab._capacity;
   }

   /// <summary>
   ///    Removes the specified key and its associated value from this symbol table (if the key is in this symbol table).
   /// </summary>
   /// <param name="key">The key</param>
   /// <exception cref="ArgumentNullException">If <paramref name="key" /> is null</exception>
   public void Delete(TKey? key)
   {
      if (key == null)
      {
         throw new ArgumentNullException(nameof(key));
      }

      if (!Contains(key))
      {
         return;
      }

      // find position of key
      var hashIdx = ComputeHash(key);
      while (!KeyComparer.Equals(key, _keys[hashIdx]))
      {
         hashIdx = (hashIdx + 1) % _capacity;
      }

      // delete key and associated value
      _keys[hashIdx] = default;
      _values[hashIdx] = default;

      // rehash all keys in same cluster
      hashIdx = (hashIdx + 1) % _capacity;
      while (_keys[hashIdx] != null)
      {
         // delete keys[i] and vals[i] and reinsert
         var keyToRehash = _keys[hashIdx];
         var valToRehash = _values[hashIdx];
         _keys[hashIdx] = default;
         _values[hashIdx] = default;
         Size--;
         this[keyToRehash] = valToRehash;
         hashIdx = (hashIdx + 1) % _capacity;
      }

      Size--;

      // halves size of array if it's 12.5% full or less
      if (Size > 0 && Size <= _capacity / 8)
      {
         Resize(_capacity / 2);
      }
   }

   #region Integrity check

#if DEBUG
   // don't check after each put() because integrity is not maintained during a call to delete()
   public bool Check(out string errorMsg)
   {
      // check that hash table is at most 50% full
      if (_capacity < 2 * Size)
      {
         errorMsg = $"Hash table size = {_capacity}; Array size = {Size}";
         return false;
      }

      // check that each key in table can be found by get()
      for (var i = 0; i < _capacity; i++)
      {
         if (_keys[i] == null)
         {
            continue;
         }

         if (!ValueComparer.Equals(this[_keys[i]], _values[i]))
         {
            errorMsg = $"Get[{_keys[i]}] = {this[_keys[i]]}; vals[i] = {_values[i]}";
            return false;
         }
      }

      errorMsg = string.Empty;
      return true;
   }
#endif

   #endregion
}