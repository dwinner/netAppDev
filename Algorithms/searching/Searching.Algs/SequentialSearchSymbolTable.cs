using System.Diagnostics;

namespace Searching.Algs;

/// <summary>
///    Symbol table implementation with sequential search in an unordered linked list of key-value pairs
/// </summary>
/// <typeparam name="TKey">Key type</typeparam>
/// <typeparam name="TValue">Value type</typeparam>
public sealed class SequentialSearchSymbolTable<TKey, TValue>
{
   private Node? _first; // the linked list of key-value pairs

   /// <summary>
   ///    Number of key-value pairs
   /// </summary>
   public int Size { get; private set; }

   /// <summary>
   ///    Is the symbol table empty?
   /// </summary>
   public bool IsEmpty => Size == 0;

   /// <summary>
   ///    All the keys
   /// </summary>
   public IEnumerable<TKey> Keys
   {
      get
      {
         for (var x = _first; x != default; x = x.Next)
         {
            yield return x.Key;
         }
      }
   }

   /// <summary>
   ///    return the value associated with the key, or default if the key is not present
   /// </summary>
   /// <param name="key">Key</param>
   public TValue? this[TKey key]
   {
      get
      {
         Debug.Assert(key != null, nameof(key) + " != null");

         for (var x = _first; x != default; x = x.Next)
         {
            if (key.Equals(x.Key))
            {
               return x.Value;
            }
         }

         return default;
      }
      set
      {
         Debug.Assert(key != null, nameof(key) + " != null");

         if (ReferenceEquals(value, default))
         {
            Delete(key);
            return;
         }

         for (var x = _first; x != default; x = x.Next)
         {
            if (key.Equals(x.Key))
            {
               x.Value = value;
               return;
            }
         }

         _first = new Node(key, value, _first);
         Size++;
      }
   }

   /// <summary>
   ///    Remove key-value pair with given key (if it's in the table)
   /// </summary>
   /// <param name="key">Key</param>
   public void Delete(TKey key) =>
      _first = Delete(_first, key);

   /// <summary>
   ///    Does this symbol table contain the given key?
   /// </summary>
   /// <param name="key">Key</param>
   /// <returns>true if the value is present, false - otherwise</returns>
   public bool Contains(TKey key) =>
      !ReferenceEquals(this[key], default);

   private Node? Delete(Node? x, TKey key)
   {
      Debug.Assert(key != null, nameof(key) + " != null");

      if (x == default)
      {
         return default;
      }

      if (key.Equals(x.Key))
      {
         Size--;
         return x.Next;
      }

      x.Next = Delete(x.Next, key);

      return x;
   }

   // a helper linked list data type
   private record Node(TKey Key, TValue? Value, Node? Next)
   {
      public TKey Key { get; } = Key;

      public TValue? Value { get; internal set; } = Value;

      public Node? Next { get; internal set; } = Next;
   }
}