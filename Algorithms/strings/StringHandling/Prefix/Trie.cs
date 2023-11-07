using System.Text;

namespace StringHandling.Prefix;

/// <summary>
///    The <see cref="Trie{TValue}" /> class represents a symbol table of key-value
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
///    values cannot be null—setting the
///    value associated with a key to null is equivalent to deleting the key
///    from the symbol table.
///    <p>
///       This implementation uses a 256-way trie.
///       The <em>put</em>, <em>contains</em>, <em>delete</em>, and
///       <em>longest prefix</em> operations take time proportional to the length
///       of the key (in the worst case). Construction takes constant time.
///       The <em>size</em>, and <em>is-empty</em> operations take constant time.
///       Construction takes constant time.
///    </p>
/// </summary>
public sealed class Trie<TValue>
{
   private const int Radix = 0x100; // extended ASCII
   private static readonly EqualityComparer<TValue> ValueComparer = EqualityComparer<TValue>.Default;
   private Node<TValue?>? _rootNode; // root of trie

   /// <summary>
   ///    the number of key-value pairs in this symbol table.
   /// </summary>
   public int Size { get; private set; }

   /// <summary>
   ///    Is this symbol table empty?
   /// </summary>
   public bool IsEmpty => Size == 0;

   /// <summary>
   ///    All keys in the symbol table
   /// </summary>
   public IEnumerable<string> Keys => GetKeysWithPrefix(string.Empty);

   /// <summary>
   ///    Returns the value associated with the given key.
   /// </summary>
   /// <param name="key">The key</param>
   /// <returns>
   ///    The value associated with the given key if the key is in the symbol table and null if the key is not in the
   ///    symbol table
   /// </returns>
   /// <exception cref="ArgumentNullException">If <paramref name="key" /> is null</exception>
   public TValue? this[string key]
   {
      get
      {
         if (key == null)
         {
            throw new ArgumentNullException(nameof(key));
         }

         var node = Get(_rootNode, key, 0);

         // NOTEME: What about null for value types?!
         return node == null ? default : node.Val;
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
         }
         else
         {
            _rootNode = Put(_rootNode, key, value!, 0);
         }
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

      return !ValueComparer.Equals(this[key], default);
   }

   /// <summary>
   ///    Returns all of the keys in the set that start with <paramref name="prefix" />.
   /// </summary>
   /// <param name="prefix">The prefix</param>
   /// <returns>All of the keys in the set that start with <paramref name="prefix" /></returns>
   public IEnumerable<string> GetKeysWithPrefix(string prefix)
   {
      var resultQueue = new Queue<string>();
      var node = Get(_rootNode, prefix, 0);
      Collect(node, new StringBuilder(prefix), resultQueue);

      return resultQueue;
   }

   /// <summary>
   ///    Returns all of the keys in the symbol table that match <paramref name="pattern" />,
   ///    where the character '.' is interpreted as a wildcard character.
   /// </summary>
   /// <param name="pattern">The pattern</param>
   /// <returns>
   ///    All of the keys in the symbol table that match <paramref name="pattern" />, as an iterable, where . is treated
   ///    as a wildcard character.
   /// </returns>
   public IEnumerable<string> GetMatchedKeys(string pattern)
   {
      var resultQueue = new Queue<string>();
      Collect(_rootNode, new StringBuilder(), pattern, resultQueue);

      return resultQueue;
   }

   /// <summary>
   ///    Returns the string in the symbol table that is the longest prefix of <paramref name="query" />,
   ///    or null, if no such string.
   /// </summary>
   /// <param name="query">The query string</param>
   /// <returns>
   ///    The string in the symbol table that is the longest prefix of <paramref name="query" />, or null if no such
   ///    string
   /// </returns>
   /// <exception cref="ArgumentNullException">
   ///    If
   ///    <param name="query"></param>
   ///    is null
   /// </exception>
   public string GetLongestPrefixOf(string query)
   {
      if (query == null)
      {
         throw new ArgumentNullException(nameof(query));
      }

      var length = GetLongestPrefixOf(_rootNode, query, 0, -1);
      return length == -1 ? string.Empty : query[..length];
   }

   /// <summary>
   ///    Removes the key from the set if the key is present.
   /// </summary>
   /// <param name="key">The key</param>
   /// <exception cref="ArgumentNullException">If <paramref name="key" /> is null</exception>
   public void Delete(string key)
   {
      if (key == null)
      {
         throw new ArgumentNullException(nameof(key));
      }

      _rootNode = Delete(_rootNode, key, 0);
   }

   private Node<TValue?> Put(Node<TValue?>? node, string key, TValue value, int idx)
   {
      node ??= new Node<TValue?>();
      if (idx == key.Length)
      {
         if (ValueComparer.Equals(node.Val, default))
         {
            Size++;
         }

         node.Val = value;
         return node;
      }

      var charAtIdx = key[idx];
      node.Next[charAtIdx] = Put(node.Next[charAtIdx], key, value, idx + 1);

      return node;
   }

   private static Node<TValue?>? Get(Node<TValue?>? node, string key, int idx)
   {
      if (node == null)
      {
         return null;
      }

      if (idx == key.Length)
      {
         return node;
      }

      var charAtIdx = key[idx];
      return Get(node.Next[charAtIdx], key, idx + 1);
   }

   private static void Collect(Node<TValue?>? node, StringBuilder prefix, Queue<string> resultQueue)
   {
      if (node == null)
      {
         return;
      }

      // NOTEME: Since we gather all valid prefixes, we skip checking for equality there
      if (node.Val != null)
      {
         resultQueue.Enqueue(prefix.ToString());
      }

      for (var chr = (char)0; chr < Radix; chr++)
      {
         prefix.Append(chr);
         Collect(node.Next[chr], prefix, resultQueue);
         prefix.Remove(prefix.Length - 1, 1);
      }
   }

   private static void Collect(Node<TValue?>? node, StringBuilder prefix, string pattern, Queue<string> resultQueue)
   {
      if (node == null)
      {
         return;
      }

      var idx = prefix.Length;
      if (idx == pattern.Length && !ValueComparer.Equals(node.Val, default))
      {
         resultQueue.Enqueue(prefix.ToString());
      }

      if (idx == pattern.Length)
      {
         return;
      }

      var charAtIdx = pattern[idx];
      if (charAtIdx == '.')
      {
         for (var @char = (char)0; @char < Radix; @char++)
         {
            prefix.Append(@char);
            Collect(node.Next[@char], prefix, pattern, resultQueue);
            prefix.Remove(prefix.Length - 1, 1);
         }
      }
      else
      {
         prefix.Append(charAtIdx);
         Collect(node.Next[charAtIdx], prefix, pattern, resultQueue);
         prefix.Remove(prefix.Length - 1, 1);
      }
   }

   // returns the length of the longest string key in the subtrie
   // rooted at x that is a prefix of the query string,
   // assuming the first d character match and we have already
   // found a prefix match of given length (-1 if no such match)
   private static int GetLongestPrefixOf(Node<TValue?>? node, string query, int idx, int length)
   {
      if (node == null)
      {
         return length;
      }

      if (!ValueComparer.Equals(node.Val, default))
      {
         length = idx;
      }

      if (idx == query.Length)
      {
         return length;
      }

      var charAtIdx = query[idx];
      return GetLongestPrefixOf(node.Next[charAtIdx], query, idx + 1, length);
   }

   private Node<TValue?>? Delete(Node<TValue?>? node, string key, int idx)
   {
      if (node == null)
      {
         return null;
      }

      if (idx == key.Length)
      {
         if (!ValueComparer.Equals(node.Val, default))
         {
            Size--;
         }

         node.Val = default;
      }
      else
      {
         var charAtIdx = key[idx];
         node.Next[charAtIdx] = Delete(node.Next[charAtIdx], key, idx + 1);
      }

      // remove subtrie rooted at x if it is completely empty
      if (!ValueComparer.Equals(node.Val, default))
      {
         return node;
      }

      for (var @char = 0; @char < Radix; @char++)
      {
         if (node.Next[@char] != null)
         {
            return node;
         }
      }

      return null;
   }

   /// <summary>
   ///    R-way trie node
   /// </summary>
   /// <typeparam name="TVal">Value type</typeparam>
   private sealed class Node<TVal>
   {
      internal readonly Node<TVal?>?[] Next;

      public Node()
      {
         Next = new Node<TVal?>?[Radix];
         Array.Fill(Next, default);
      }

      internal TVal? Val { get; set; }
   }
}