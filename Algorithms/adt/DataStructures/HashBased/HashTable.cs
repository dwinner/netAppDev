namespace DataStructures.HashBased;

public class HashTable<TKey, TValue>
{
   private const double FillFactor = 0.75; // If the array exceeds this fill percentage it will grow
   private const int DefaultCapacity = 1000;
   private HashTableArray<TKey, TValue> _array; // The array where the items are stored.

   // The maximum number of items to store before growing.
   // This is just a cached value of the fill factor calculation
   private int _maxItemsAtCurrentSize;

   /// <summary>
   ///    Constructs a hash table with the default capacity
   /// </summary>
   public HashTable()
      : this(DefaultCapacity)
   {
   }

   /// <summary>
   ///    Constructs a hash table with the specified capacity
   /// </summary>
   public HashTable(int initialCapacity)
   {
      if (initialCapacity < 1)
      {
         throw new ArgumentOutOfRangeException(nameof(initialCapacity));
      }

      _array = new HashTableArray<TKey, TValue>(initialCapacity);

      // when the count exceeds this value, the next Add will cause the array to grow
      _maxItemsAtCurrentSize = (int)(initialCapacity * FillFactor) + 1;
   }

   /// <summary>
   ///    Gets and sets the value with the specified key. ArgumentException is
   ///    thrown if the key does not already exist in the hash table.
   /// </summary>
   /// <param name="key">The key of the value to retrieve</param>
   /// <returns>The value associated with the specified key</returns>
   public TValue this[TKey key]
   {
      get => !_array.TryGetValue(key, out var value)
         ? throw new ArgumentException($"key {key} isn't found", nameof(key))
         : value;
      set => _array.Update(key, value);
   }

   /// <summary>
   ///    Returns an enumerator for all of the keys in the hash table
   /// </summary>
   public IEnumerable<TKey> Keys => _array.Keys;

   /// <summary>
   ///    Returns an enumerator for all of the values in the hash table
   /// </summary>
   public IEnumerable<TValue> Values => _array.Values;

   /// <summary>
   ///    The number of items currently in the hash table
   /// </summary>
   public int Count { get; private set; }

   /// <summary>
   ///    Adds the key/value pair to the hash table. If the key already exists in the
   ///    hash table an ArgumentException will be thrown
   /// </summary>
   /// <param name="key">The key of the item being added</param>
   /// <param name="value">The value of the item being added</param>
   public void Add(TKey key, TValue value)
   {
      // if we are at capacity, the array needs to grow
      if (Count >= _maxItemsAtCurrentSize)
      {
         // allocate a larger array
         var largerArray = new HashTableArray<TKey, TValue>(_array.Capacity * 2);

         // and re-add each item to the new array
         foreach (var node in _array.Items)
         {
            largerArray.Add(node.Key, node.Value);
         }

         // the larger array is now the hash table storage
         _array = largerArray;

         // update the new max items cached value
         _maxItemsAtCurrentSize = (int)(_array.Capacity * FillFactor) + 1;
      }

      _array.Add(key, value);
      Count++;
   }

   /// <summary>
   ///    Removes the item from the hash table whose key matches
   ///    the specified key.
   /// </summary>
   /// <param name="key">The key of the item to remove</param>
   /// <returns>True if the item was removed, false otherwise.</returns>
   public bool Remove(TKey key)
   {
      var removed = _array.Remove(key);
      if (removed)
      {
         Count--;
      }

      return removed;
   }

   /// <summary>
   ///    Finds and returns the value for the specified key.
   /// </summary>
   /// <param name="key">The key whose value is sought</param>
   /// <param name="value">The value associated with the specified key</param>
   /// <returns>True if the value was found, false otherwise</returns>
   public bool TryGetValue(TKey key, out TValue value) => _array.TryGetValue(key, out value);

   /// <summary>
   ///    Returns a Boolean indicating if the hash table contains the specified key.
   /// </summary>
   /// <param name="key">The key whose existence is being tested</param>
   /// <returns>True if the value exists in the hash table, false otherwise.</returns>
   public bool ContainsKey(TKey key) => _array.TryGetValue(key, out _);

   /// <summary>
   ///    Returns a Boolean indicating if the hash table contains the specified value.
   /// </summary>
   /// <param name="value">The value whose existence is being tested</param>
   /// <returns>True if the value exists in the hash table, false otherwise.</returns>
   public bool ContainsValue(TValue value)
   {
      if (value == null)
      {
         throw new ArgumentNullException(nameof(value));
      }

      return _array.Values.Contains(value);
   }

   /// <summary>
   ///    Removes all items from the hash table
   /// </summary>
   public void Clear()
   {
      _array.Clear();
      Count = 0;
   }
}