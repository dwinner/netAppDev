using System.Text;

namespace StringHandling.Prefix;

/// <summary>
///    The <see cref="TernarySearchTrie{TValue}" /> class represents a symbol table of key-value
///    pairs, with string keys and generic values.
///    It supports the usual <em>put</em>, <em>get</em>, <em>contains</em>,
///    <em>delete</em>, <em>size</em>, and <em>is-empty</em> methods.
///    It also provides character-based methods for finding the string
///    in the symbol table that is the <em>longest prefix</em> of a given prefix,
///    finding all strings in the symbol table that <em>start with</em> a given prefix,
///    and finding all strings in the symbol table that <em>match</em> a given pattern.
///    A symbol table implements the <em>associative array</em> abstraction:
///    when associating a value with a key that is already in the symbol table,
///    the convention is to replace the old value with the new value.
///    Unlike <see cref="Dictionary{TKey,TValue}" />, this class uses the convention that
///    values cannot be null, the value associated with a key to null is equivalent to deleting the key
///    from the symbol table.
/// </summary>
/// <typeparam name="TValue">Contained value type</typeparam>
public sealed class TernarySearchTrie<TValue>
{
   private static readonly EqualityComparer<TValue> ValComparer = EqualityComparer<TValue>.Default;
   private Node<TValue?>? _root; // root of TST

   /// <summary>
   ///    Returns the number of key-value pairs in this symbol table.
   /// </summary>
   public int Size { get; private set; }

   /// <summary>
   ///    Returns the value associated with the given key.
   /// </summary>
   /// <param name="key">The key</param>
   /// <returns>
   ///    The value associated with the given key if the key is in the symbol table and null if the key is not in the
   ///    symbol table
   /// </returns>
   /// <exception cref="ArgumentNullException">If <paramref name="key" /> is null</exception>
   /// <exception cref="ArgumentException">If length of <paramref name="key" /> is 0</exception>
   public TValue? this[string key]
   {
      get
      {
         if (key.Length == 0)
         {
            throw new ArgumentException($"'{key}' must have length >= 1", nameof(key));
         }

         var node = Get(_root, key, 0);
         return node == null ? default : node.Value;
      }
      set
      {
         if (key == null)
         {
            throw new ArgumentNullException(nameof(key));
         }

         if (!Contains(key))
         {
            Size++;
         }
         else if (ValComparer.Equals(value, default))
         {
            Size--; // delete existing key
         }

         _root = Put(_root, key, value, 0);
      }
   }

   /// <summary>
   ///    Does this symbol table contain the given key?
   /// </summary>
   /// <param name="key">The key</param>
   /// <returns>true if this symbol table contains <paramref name="key" />; false otherwise</returns>
   /// <exception cref="ArgumentNullException">If <paramref name="key" /> is null</exception>
   public bool Contains(string key)
   {
      if (key == null)
      {
         throw new ArgumentNullException(nameof(key));
      }

      var value = this[key];
      return !ValComparer.Equals(value, default);
   }

   /// <summary>
   ///    Returns the string in the symbol table that is the longest prefix of <paramref name="query" />, or null, if no such
   ///    string.
   /// </summary>
   /// <param name="query">The query string</param>
   /// <returns>
   ///    The string in the symbol table that is the longest prefix of <paramref name="query" />, or null, if no such
   ///    string.
   /// </returns>
   /// <exception cref="ArgumentNullException">If <paramref name="query" /> is null</exception>
   public string GetLongestPrefixOf(string query)
   {
      if (query == null)
      {
         throw new ArgumentNullException(nameof(query));
      }

      if (query.Length == 0)
      {
         return string.Empty;
      }

      var len = 0;
      var startNode = _root;
      var idx = 0;
      while (startNode != null && idx < query.Length)
      {
         var charAt = query[idx];
         if (charAt < startNode.Char)
         {
            startNode = startNode.Left;
         }
         else if (charAt > startNode.Char)
         {
            startNode = startNode.Right;
         }
         else
         {
            idx++;
            if (startNode.Value != null)
            {
               len = idx;
            }

            startNode = startNode.Middle;
         }
      }

      return query[..len];
   }

   /// <summary>
   ///    Returns all keys in the symbol table as an <see cref="IEnumerable{T}" />.
   /// </summary>
   /// <returns>All keys in the symbol table</returns>
   public IEnumerable<string> GetKeys()
   {
      var strQueue = new Queue<string>();
      Collect(_root, new StringBuilder(), strQueue);
      return strQueue;
   }

   /// <summary>
   ///    Returns all the keys in the set that start with <paramref name="prefix" />.
   /// </summary>
   /// <param name="prefix">The prefix</param>
   /// <returns>All the keys in the set that start with <paramref name="prefix" /></returns>
   /// <exception cref="ArgumentNullException">If <paramref name="prefix" /> is null</exception>
   public IEnumerable<string> GetKeysWithPrefix(string prefix)
   {
      if (prefix == null)
      {
         throw new ArgumentNullException(nameof(prefix));
      }

      var node = Get(_root, prefix, 0);
      if (node == null)
      {
         return Enumerable.Empty<string>();
      }

      var queue = new Queue<string>();
      if (!ValComparer.Equals(node.Value, default))
      {
         queue.Enqueue(prefix);
      }

      Collect(node.Middle, new StringBuilder(prefix), queue);

      return queue;
   }

   /// <summary>
   ///    Returns all the keys in the symbol table that match <paramref name="pattern" />, where the character '.' is
   ///    interpreted as a wildcard character.
   /// </summary>
   /// <param name="pattern">The pattern</param>
   /// <returns>
   ///    All the keys in the symbol table that match <paramref name="pattern" />, where . is treated as a wildcard
   ///    character.
   /// </returns>
   public IEnumerable<string> GetKeysThatMatch(string pattern)
   {
      var queue = new Queue<string>();
      Collect(_root, new StringBuilder(), 0, pattern, queue);
      return queue;
   }

   // all keys in subtrie rooted at x with given prefix
   private static void Collect(Node<TValue?>? node, StringBuilder prefix, Queue<string> queue)
   {
      if (node == null)
      {
         return;
      }

      Collect(node.Left, prefix, queue);
      if (!ValComparer.Equals(node.Value, default))
      {
         queue.Enqueue($"{prefix}{node.Char}");
      }

      Collect(node.Middle, prefix.Append(node.Char), queue);
      prefix.Remove(prefix.Length - 1, 1);
      Collect(node.Right, prefix, queue);
   }

   private void Collect(Node<TValue?>? node, StringBuilder prefix, int idx, string pattern, Queue<string> queue)
   {
      if (node == null)
      {
         return;
      }

      var charAt = pattern[idx];
      if (charAt == '.' || charAt < node.Char)
      {
         Collect(node.Left, prefix, idx, pattern, queue);
      }

      if (charAt == '.' || charAt == node.Char)
      {
         if (idx == pattern.Length - 1 && node.Value != null)
         {
            queue.Enqueue($"{prefix}{node.Char}");
         }

         if (idx < pattern.Length - 1)
         {
            Collect(node.Middle, prefix.Append(node.Char), idx + 1, pattern, queue);
            prefix.Remove(prefix.Length - 1, 1);
         }
      }

      if (charAt == '.' || charAt > node.Char)
      {
         Collect(node.Right, prefix, idx, pattern, queue);
      }
   }

   // return subtrie corresponding to given key
   private static Node<TValue?>? Get(Node<TValue?>? node, string key, int idx)
   {
      if (node == null)
      {
         return null;
      }

      var charAtIdx = key[idx];
      return charAtIdx < node.Char
         ? Get(node.Left, key, idx)
         : charAtIdx > node.Char
            ? Get(node.Right, key, idx)
            : idx < key.Length - 1
               ? Get(node.Middle, key, idx + 1)
               : node;
   }

   private static Node<TValue?> Put(Node<TValue?>? node, string key, TValue? value, int idx)
   {
      var charAt = key[idx];
      node ??= new Node<TValue?> { Char = charAt };
      if (charAt < node.Char)
      {
         node.Left = Put(node.Left, key, value, idx);
      }
      else if (charAt > node.Char)
      {
         node.Right = Put(node.Right, key, value, idx);
      }
      else if (idx < key.Length - 1)
      {
         node.Middle = Put(node.Middle, key, value, idx + 1);
      }
      else
      {
         node.Value = value;
      }

      return node;
   }

   private sealed class Node<TVal>
   {
      internal char Char; // character

      // left, middle, and right subtries
      internal Node<TVal?>? Left;
      internal Node<TVal?>? Middle;
      internal Node<TVal?>? Right;
      internal TVal? Value; // value associated with string
   }
}