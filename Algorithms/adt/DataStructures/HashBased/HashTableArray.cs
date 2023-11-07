namespace DataStructures.HashBased;

internal sealed class HashTableArray<TKey, TValue>
{
   private readonly HashTableArrayNode<TKey, TValue>?[] _array;

   /// <summary>
   ///    Constructs a new hash table array with the specified capacity
   /// </summary>
   /// <param name="capacity">The capacity of the array</param>
   public HashTableArray(int capacity) => _array = new HashTableArrayNode<TKey, TValue>[capacity];

   /// <summary>
   ///    The capacity of the hash table array
   /// </summary>
   public int Capacity => _array.Length;

   /// <summary>
   ///    Returns an enumerator for all of the values in the node array
   /// </summary>
   public IEnumerable<TValue> Values => _array.Where(node => node != null).SelectMany(node => node!.Values);

   /// <summary>
   ///    Returns an enumerator for all of the keys in the node array
   /// </summary>
   public IEnumerable<TKey> Keys => _array.Where(node => node != null).SelectMany(node => node!.Keys);

   /// <summary>
   ///    Returns an enumerator for all of the Items in the node array
   /// </summary>
   public IEnumerable<HashTableNodePair<TKey, TValue>> Items =>
      _array.Where(node => node != null).SelectMany(node => node!.Items);

   /// <summary>
   ///    Adds the key/value pair to the node. If the key already exists in the
   ///    node array an ArgumentException will be thrown
   /// </summary>
   /// <param name="key">The key of the item being added</param>
   /// <param name="value">The value of the item being added</param>
   public void Add(TKey key, TValue value)
   {
      var index = GetIndex(key);
      var nodes = _array[index];
      if (nodes == null)
      {
         nodes = new HashTableArrayNode<TKey, TValue>();
         _array[index] = nodes;
      }

      nodes.Add(key, value);
   }

   /// <summary>
   ///    Updates the value of the existing key/value pair in the node array.
   ///    If the key does not exist in the array an ArgumentException
   ///    will be thrown.
   /// </summary>
   /// <param name="key">The key of the item being updated</param>
   /// <param name="value">The updated value</param>
   public void Update(TKey key, TValue value)
   {
      var nodes = _array[GetIndex(key)];
      if (nodes == null)
      {
         throw new ArgumentException("The key does not exist in the hash table", nameof(key));
      }

      nodes.Update(key, value);
   }

   /// <summary>
   ///    Removes the item from the node array whose key matches
   ///    the specified key.
   /// </summary>
   /// <param name="key">The key of the item to remove</param>
   /// <returns>True if the item was removed, false otherwise.</returns>
   public bool Remove(TKey key)
   {
      var nodes = _array[GetIndex(key)];
      if (nodes != null)
      {
         return nodes.Remove(key);
      }

      return false;
   }

   /// <summary>
   ///    Finds and returns the value for the specified key.
   /// </summary>
   /// <param name="key">The key whose value is sought</param>
   /// <param name="value">The value associated with the specified key</param>
   /// <returns>True if the value was found, false otherwise</returns>
   public bool TryGetValue(TKey key, out TValue value)
   {
      var nodes = _array[GetIndex(key)];
      if (nodes != null)
      {
         return nodes.TryGetValue(key, out value);
      }

      value = default!;
      return false;
   }

   /// <summary>
   ///    Removes every item from the hash table array
   /// </summary>
   public void Clear()
   {
      foreach (var node in _array.Where(node => node != null))
      {
         node!.Clear();
      }
   }

   // Maps a key to the array index based on hash code
   private int GetIndex(TKey key)
   {
      if (key != null)
      {
         return Math.Abs(key.GetHashCode() % Capacity);
      }

      throw new ArgumentNullException(nameof(key));
   }
}