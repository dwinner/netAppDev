/*
 * A symbol table implemented with a separate-chaining hash table.
 */

using System.Diagnostics;

namespace Searching.Algs;

/// <summary>
///    The <see cref="SeparateChainingHashSymbolTable{TKey,TValue}" /> class represents a symbol table of generic
///    key-value pairs.
///    It supports the usual <em>put</em>, <em>get</em>, <em>contains</em>,
///    <em>delete</em>, <em>size</em>, and <em>is-empty</em> methods.
///    It also provides a <em>keys</em> method for iterating over all of the keys.
///    A symbol table implements the <em>associative array</em> abstraction:
///    when associating a value with a key that is already in the symbol table,
///    the convention is to replace the old value with the new value.
///    Unlike {@link java.util.Map}, this class uses the convention that
///    values cannot be null: setting the
///    value associated with a key to null is equivalent to deleting the key
///    from the symbol table.
///    <p>
///       This implementation uses a separate chaining hash table. It requires that
///       the key type overrides the {@code equals()} and {@code hashCode()} methods.
///       The expected time per <em>put</em>, <em>contains</em>, or <em>remove</em>
///       operation is constant, subject to the uniform hashing assumption.
///       The <em>size</em>, and <em>is-empty</em> operations take constant time.
///       Construction takes constant time.
///    </p>
/// </summary>
/// <typeparam name="TKey">Key type</typeparam>
/// <typeparam name="TValue">Value type</typeparam>
public sealed class SeparateChainingHashSymbolTable<TKey, TValue>
{
   private const int InitCapacity = 0x4;
   private int _capacity; // hash table size
   private SequentialSearchSymbolTable<TKey, TValue?>[] _linkedSymTabs; // array of linked-list symbol tables
   // TOREFACTOR: why can't _linkedSymTabs just use LinkedList<T> instead of custom SequentialSearchSymbolTable?

   /// <summary>
   ///    Initializes an empty symbol table with <paramref name="capacity" /> chains.
   /// </summary>
   /// <param name="capacity">The initial number of chains</param>
   public SeparateChainingHashSymbolTable(int capacity)
   {
      _capacity = capacity;
      _linkedSymTabs = new SequentialSearchSymbolTable<TKey, TValue?>[capacity];
      for (var i = 0; i < capacity; i++)
      {
         _linkedSymTabs[i] = new SequentialSearchSymbolTable<TKey, TValue?>();
      }
   }

   /// <summary>
   ///    Initializes an empty symbol table
   /// </summary>
   public SeparateChainingHashSymbolTable()
      : this(InitCapacity)
   {
   }

   /// <summary>
   ///    The number of key-value pairs in this symbol table
   /// </summary>
   public int Size { get; private set; }

   /// <summary>
   ///    true if this symbol table is empty, false - otherwise
   /// </summary>
   public bool IsEmpty => Size == 0;

   /// <summary>
   ///    keys in symbol table as an <see cref="IEnumerable{TKey}" />
   /// </summary>
   public IEnumerable<TKey> Keys => _linkedSymTabs.SelectMany(table => table.Keys);

   /// <summary>
   ///    Inserts the specified key-value pair into the symbol table, overwriting the old
   ///    value with the new value if the symbol table already contains the specified key.
   ///    Deletes the specified key (and its associated value) from this symbol table if the specified value is null.
   /// </summary>
   /// <param name="key">Key</param>
   /// <param name="value">Value</param>
   /// <exception cref="ArgumentNullException">If <paramref name="key" /> is null</exception>
   public TValue? this[TKey key]
   {
      set
      {
         if (key == null)
         {
            throw new ArgumentNullException(nameof(key));
         }

         if (value == null)
         {
            Delete(key);
            return;
         }

         // double table size if average length of list >= 10
         if (Size >= 10 * _capacity)
         {
            Resize(2 * _capacity);
         }

         var index = GetHash(key);
         if (!_linkedSymTabs[index].Contains(key))
         {
            Size++;
         }

         _linkedSymTabs[index][key] = value;
      }
      get
      {
         if (key == null)
         {
            throw new ArgumentNullException(nameof(key));
         }

         var index = GetHash(key);

         return _linkedSymTabs[index][key];
      }
   }

   // resize the hash table to have the given number of chains,
   // rehashing all of the keys
   private void Resize(int chains)
   {
      var tmpSymTab = new SeparateChainingHashSymbolTable<TKey, TValue?>(chains);
      for (var i = 0; i < _capacity; i++)
      {
         foreach (var key in _linkedSymTabs[i].Keys)
         {
            tmpSymTab[key] = _linkedSymTabs[i][key];
         }
      }

      _capacity = tmpSymTab._capacity;
      Size = tmpSymTab.Size;
      _linkedSymTabs = tmpSymTab._linkedSymTabs;
   }

   // hash function for keys - returns value between 0 and m-1
   // private int GetHashTextBook(TKey key) => (key.GetHashCode() & 0x7fffffff) % _capacity;

   // hash function for keys - returns value between 0 and m-1 (assumes m is a power of 2)
   // (protects against poor quality hashCode() implementations)
   private int GetHash(TKey key)
   {
      Debug.Assert(key != null, nameof(key) + " != null");

      var hashCode = key.GetHashCode();
      hashCode ^= (hashCode >>> 20) ^ (hashCode >>> 12) ^ (hashCode >>> 7) ^ (hashCode >>> 4);

      return hashCode & (_capacity - 1);
   }

   /// <summary>
   ///    Returns true if this symbol table contains the specified key.
   /// </summary>
   /// <param name="key">The key</param>
   /// <returns>true if this symbol table contains the specified key.</returns>
   /// <exception cref="ArgumentNullException">If <paramref name="key" /> is null</exception>
   public bool Contains(TKey key)
   {
      if (key == null)
      {
         throw new ArgumentNullException(nameof(key));
      }

      var value = this[key];
      return !EqualityComparer<TValue>.Default.Equals(value, default); // return value != null;
   }

   /// <summary>
   ///    Removes the specified key and its associated value from this symbol table (if the key is in this symbol table).
   /// </summary>
   /// <param name="key">The key</param>
   /// <exception cref="ArgumentNullException">If <paramref name="key" /> is null</exception>
   public void Delete(TKey key)
   {
      if (key == null)
      {
         throw new ArgumentNullException(nameof(key));
      }

      var index = GetHash(key);
      if (_linkedSymTabs[index].Contains(key))
      {
         Size--;
      }

      _linkedSymTabs[index].Delete(key);

      // halve table size if average length of list <= 2
      if (_capacity > InitCapacity && Size <= 2 * _capacity)
      {
         Resize(_capacity / 2);
      }
   }
}