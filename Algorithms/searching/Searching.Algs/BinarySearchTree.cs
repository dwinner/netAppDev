using System.Diagnostics;

namespace Searching.Algs;

/// <summary>
///    The {@code BST} class represents an ordered symbol table of generic
///    key-value pairs.
///    It supports the usual <em>put</em>, <em>get</em>, <em>contains</em>,
///    <em>delete</em>, <em>size</em>, and <em>is-empty</em> methods.
///    It also provides ordered methods for finding the <em>minimum</em>,
///    <em>maximum</em>, <em>floor</em>, <em>select</em>, <em>ceiling</em>.
///    It also provides a <em>keys</em> method for iterating over all of the keys.
///    A symbol table implements the <em>associative array</em> abstraction:
///    when associating a value with a key that is already in the symbol table,
///    the convention is to replace the old value with the new value.
///    Unlike {@link java.util.Map}, this class uses the convention that
///    values cannot be {@code null}—setting the
///    value associated with a key to {@code null} is equivalent to deleting the key
///    from the symbol table.
///    <p>
///       It requires that
///       the key type implements the {@code Comparable} interface and calls the
///       {@code compareTo()} and method to compare two keys. It does not call either
///       {@code equals()} or {@code hashCode()}.
///    </p>
///    <p>
///       This implementation uses an (unbalanced) <em>binary search tree</em>.
///       The <em>put</em>, <em>contains</em>, <em>remove</em>, <em>minimum</em>,
///       <em>maximum</em>, <em>ceiling</em>, <em>floor</em>, <em>select</em>, and
///       <em>rank</em>  operations each take Theta (<em>n</em>) time in the worst
///       case, where <em>n</em> is the number of key-value pairs.
///    </p>
///    The <em>size</em> and <em>is-empty</em> operations take Theta (1) time.
///    The keys method takes Theta (<em>n</em>) time in the worst case.
///    Construction takes Theta (1) time.
/// </summary>
/// <typeparam name="TKey">Key type</typeparam>
/// <typeparam name="TValue">Value type</typeparam>
public sealed class BinarySearchTree<TKey, TValue>
   where TKey : IComparable<TKey>
{
   private static readonly EqualityComparer<TKey> KeyComparer = EqualityComparer<TKey>.Default;
   private Node? _root; // root of BST

   /// <summary>
   ///    Number of key-value pairs in BST
   /// </summary>
   public int Size => GetSize(_root);

   /// <summary>
   ///    Is the symbol table empty?
   /// </summary>
   public bool IsEmpty => Size == 0;

   /// <summary>
   ///    Returns all keys in the symbol table in ascending order
   /// </summary>
   public IEnumerable<TKey> Keys
   {
      get
      {
         if (IsEmpty)
         {
            return Enumerable.Empty<TKey>();
         }

         var min = Min;
         var max = Max;

         return GetKeys(min, max);
      }
   }

   /// <summary>
   ///    Return value associated with the given key, or null if no such key exists
   /// </summary>
   /// <param name="key">Key</param>
   /// <returns>Value associated with the given key, or null if no such key exists</returns>
   public TValue? this[TKey? key] => Get(_root, key);

   /// <summary>
   ///    Returns the smallest key in the symbol table
   /// </summary>
   /// <value>The smallest key in the symbol table</value>
   public TKey? Min
   {
      get
      {
         if (IsEmpty)
         {
            return default;
         }

         var min = GetMin(_root);
         return min != null ? min.Key : default;
      }
   }

   /// <summary>
   ///    Returns the largest key in the symbol table
   /// </summary>
   /// <value>The largest key in the symbol table</value>
   /// <exception cref="InvalidOperationException">Empty symbol table</exception>
   public TKey? Max
   {
      get
      {
         if (IsEmpty)
         {
            throw new InvalidOperationException("Empty symbol table");
         }

         var max = GetMax(_root);
         return max != null ? max.Key : default;
      }
   }

   /// <summary>
   ///    Return the key in the symbol table of a given <paramref name="rank" />.
   ///    This key has the property that there are <paramref name="rank" /> keys in
   ///    the symbol table that are smaller. In other words, this key is the
   ///    (<paramref name="rank" />+1)st smallest key in the symbol table.
   /// </summary>
   /// <param name="rank">Index</param>
   /// <returns>See the upper</returns>
   /// <exception cref="ArgumentOutOfRangeException">Invalid <paramref name="rank" /> value</exception>
   public TKey? this[int rank]
   {
      get
      {
         if (rank < 0 || rank >= Size)
         {
            throw new ArgumentOutOfRangeException(nameof(rank));
         }

         var x = Select(_root, rank);
         return x != null ? x.Key : default;
      }
   }

   /// <summary>
   ///    Height of this BST (one-node tree has height 0)
   /// </summary>
   /// <value>BST's Height</value>
   public int Height => GetHeight(_root);

   // Number of key-value pairs in BST rooted at x
   private static int GetSize(Node? node) => node?.Size ?? 0;

   /// <summary>
   ///    Does there exist a key-value pair with given key?
   /// </summary>
   /// <param name="key">Key</param>
   /// <returns>true if the value exists by key, false otherwise</returns>
   public bool Contains(TKey? key)
   {
      if (key == null)
      {
         throw new ArgumentNullException(nameof(key));
      }

      return this[key] != null;
   }

   private static TValue? Get(Node? node, TKey? key)
   {
      if (key == null)
      {
         throw new ArgumentNullException(nameof(key));
      }

      if (node == null)
      {
         return default;
      }

      var compareRes = key.CompareTo(node.Key);
      return compareRes switch
      {
         < 0 => Get(node.Left, key),
         > 0 => Get(node.Right, key),
         _ => node.Val
      };
   }

   /// <summary>
   ///    Inserts the specified key-value pair into the symbol table, overwriting the old
   ///    value with the new value if the symbol table already contains the specified key.
   ///    Deletes the specified key (and its associated value) from this symbol table
   ///    if the specified value is <code>null</code>.
   /// </summary>
   /// <param name="key">Key</param>
   /// <param name="value">Value</param>
   /// <exception cref="ArgumentNullException">If key is null</exception>
   public void Put(TKey? key, TValue? value)
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

      _root = Put(_root, key, value);
   }

   private static Node Put(Node? node, TKey key, TValue val)
   {
      if (node == null)
      {
         return new Node(key, val, 1);
      }

      var compareRes = key.CompareTo(node.Key);
      switch (compareRes)
      {
         case < 0:
            node.Left = Put(node.Left, key, val);
            break;

         case > 0:
            node.Right = Put(node.Right, key, val);
            break;

         default:
            node.Val = val;
            break;
      }

      node.Size = 1 + GetSize(node.Left) + GetSize(node.Right);

      return node;
   }

   /// <summary>
   ///    Removes the smallest key and associated value from the symbol table
   /// </summary>
   /// <exception cref="InvalidOperationException">Symbol table underflow</exception>
   public void DeleteMin()
   {
      if (IsEmpty)
      {
         throw new InvalidOperationException("Symbol table underflow");
      }

      _root = DeleteMin(_root);
   }

   private static Node? DeleteMin(Node? node)
   {
      if (node?.Left == null)
      {
         return node?.Right;
      }

      node.Left = DeleteMin(node.Left);
      node.Size = GetSize(node.Left) + GetSize(node.Right) + 1;

      return node;
   }

   /// <summary>
   ///    Removes the largest key and associated value from the symbol table
   /// </summary>
   /// <exception cref="InvalidOperationException">Symbol table underflow</exception>
   public void DeleteMax()
   {
      if (IsEmpty)
      {
         throw new InvalidOperationException("Symbol table underflow");
      }

      _root = DeleteMax(_root);
   }

   private static Node? DeleteMax(Node? node)
   {
      if (node?.Right == null)
      {
         return node?.Left;
      }

      node.Right = DeleteMax(node.Right);
      node.Size = GetSize(node.Left) + GetSize(node.Right) + 1;

      return node;
   }

   /// <summary>
   ///    Removes the specified key and its associated value from this symbol table
   ///    (if the key is in this symbol table).
   /// </summary>
   /// <param name="key">Key</param>
   /// <exception cref="ArgumentNullException">If a key is null</exception>
   public void Delete(TKey? key)
   {
      if (key == null)
      {
         throw new ArgumentNullException(nameof(key));
      }

      _root = Delete(_root, key);
   }

   private static Node? Delete(Node? node, TKey key)
   {
      if (node == null)
      {
         return null;
      }

      var compareRes = key.CompareTo(node.Key);
      switch (compareRes)
      {
         case < 0:
            node.Left = Delete(node.Left, key);
            break;

         case > 0:
            node.Right = Delete(node.Right, key);
            break;

         default:
         {
            if (node.Right == null)
            {
               return node.Left;
            }

            if (node.Left == null)
            {
               return node.Right;
            }

            var tmpNode = node;
            node = GetMin(tmpNode.Right);
            if (node != null)
            {
               node.Right = DeleteMin(tmpNode.Right);
               node.Left = tmpNode.Left;
            }

            break;
         }
      }

      Debug.Assert(node != null, nameof(node) + " != null");
      node.Size = GetSize(node.Left) + GetSize(node.Right) + 1;

      return node;
   }

   private static Node? GetMin(Node? node) =>
      node?.Left == null ? node : GetMin(node.Left);

   private static Node? GetMax(Node? node) =>
      node?.Right == null ? node : GetMax(node.Right);

   /// <summary>
   ///    Returns the largest key in the symbol table less than or equal to <paramref name="key" />.
   /// </summary>
   /// <param name="key">The key</param>
   /// <returns>The largest key in the symbol table less than or equal to <paramref name="key" /></returns>
   /// <exception cref="ArgumentNullException">Key is null</exception>
   /// <exception cref="InvalidOperationException">Empty symbol table or too small arg</exception>
   public TKey? GetFloor(TKey? key)
   {
      if (key == null)
      {
         throw new ArgumentNullException(nameof(key));
      }

      if (IsEmpty)
      {
         throw new InvalidOperationException("Empty symbol table");
      }

      var x = GetFloor(_root, key);
      if (x == null)
      {
         throw new InvalidOperationException($"argument to {nameof(GetFloor)} is too small");
      }

      return x.Key;
   }

   private Node? GetFloor(Node? node, TKey key)
   {
      if (node == null)
      {
         return null;
      }

      var compareRes = key.CompareTo(node.Key);
      switch (compareRes)
      {
         case 0:
            return node;
         case < 0:
            return GetFloor(node.Left, key);
      }

      var floorNode = GetFloor(node.Right, key);
      return floorNode ?? node;
   }

   /// <summary>
   ///    Returns the smallest key in the symbol table greater than or equal to <paramref name="key" />
   /// </summary>
   /// <param name="key">Key</param>
   /// <returns></returns>
   /// <exception cref="ArgumentNullException">If a key is null</exception>
   /// <exception cref="InvalidOperationException">Empty symbol table or argument is too large</exception>
   public TKey? GetCeiling(TKey? key)
   {
      if (key == null)
      {
         throw new ArgumentNullException(nameof(key));
      }

      if (IsEmpty)
      {
         throw new InvalidOperationException("Empty symbol table");
      }

      var x = GetCeiling(_root, key);
      if (x == null)
      {
         throw new InvalidOperationException($"argument is too large in {nameof(GetCeiling)}");
      }

      return x.Key;
   }

   private Node? GetCeiling(Node? node, TKey key)
   {
      if (node == null)
      {
         return null;
      }

      var compareRes = key.CompareTo(node.Key);
      switch (compareRes)
      {
         case 0:
            return node;
         case < 0:
         {
            var ceilingNode = GetCeiling(node.Left, key);
            return ceilingNode ?? node;
         }
         default:
            return GetCeiling(node.Right, key);
      }
   }

   // Return key in BST rooted at x of given rank.
   // Precondition: rank is in legal range. 
   private static Node? Select(Node? node, int rank)
   {
      if (node == null)
      {
         return null;
      }

      var leftSize = GetSize(node.Left);
      if (leftSize > rank)
      {
         return Select(node.Left, rank);
      }

      return leftSize < rank
         ? Select(node.Right, rank - leftSize - 1)
         : node;
   }

   /// <summary>
   ///    Return the number of keys in the symbol table strictly less than <paramref name="key" />.
   /// </summary>
   /// <param name="key">Key</param>
   /// <returns>The number of keys in the symbol table</returns>
   /// <exception cref="ArgumentNullException"><paramref name="key" /> is null</exception>
   public int Rank(TKey? key)
   {
      if (key == null)
      {
         throw new ArgumentNullException(nameof(key));
      }

      return Rank(key, _root);
   }

   // Number of keys in the subtree less than key.
   private static int Rank(TKey? key, Node? node)
   {
      if (node == null)
      {
         return 0;
      }

      var compareRes = key?.CompareTo(node.Key);
      return compareRes switch
      {
         < 0 => Rank(key, node.Left),
         > 0 => 1 + GetSize(node.Left) + Rank(key, node.Right),
         _ => GetSize(node.Left)
      };
   }

   /// <summary>
   ///    Returns all keys in the symbol table in the given range
   /// </summary>
   /// <param name="lowIdx">Low border</param>
   /// <param name="highIdx">High border</param>
   /// <returns>All keys in the symbol table in the given range</returns>
   /// <exception cref="ArgumentNullException"><paramref name="lowIdx" /> or <paramref name="highIdx" /></exception>
   public IEnumerable<TKey> GetKeys(TKey? lowIdx, TKey? highIdx)
   {
      if (lowIdx == null)
      {
         throw new ArgumentNullException(nameof(lowIdx));
      }

      if (highIdx == null)
      {
         throw new ArgumentNullException(nameof(highIdx));
      }

      var queue = new Queue<TKey>();
      GetKeys(_root, queue, lowIdx, highIdx);

      return queue;
   }

   private static void GetKeys(Node? node, Queue<TKey> queue, TKey lowIdx, TKey highIdx)
   {
      if (node == null)
      {
         return;
      }

      var cmpLo = lowIdx.CompareTo(node.Key);
      var cmpHi = highIdx.CompareTo(node.Key);
      if (cmpLo < 0)
      {
         GetKeys(node.Left, queue, lowIdx, highIdx);
      }

      if (cmpLo <= 0 && cmpHi >= 0)
      {
         queue.Enqueue(node.Key
                       ?? throw new ArgumentNullException(nameof(node.Key)));
      }

      if (cmpHi > 0)
      {
         GetKeys(node.Right, queue, lowIdx, highIdx);
      }
   }

   /// <summary>
   ///    Returns the number of keys in the symbol table in the given range
   /// </summary>
   /// <param name="lowIdx">Low index</param>
   /// <param name="highIdx">High index</param>
   /// <returns>The number of keys in the symbol table in the given range</returns>
   public int GetSize(TKey lowIdx, TKey highIdx) =>
      lowIdx.CompareTo(highIdx) > 0
         ? 0
         : Contains(highIdx)
            ? Rank(highIdx) - Rank(lowIdx) + 1
            : Rank(highIdx) - Rank(lowIdx);

   private static int GetHeight(Node? node) =>
      node == null
         ? -1
         : 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

   /// <summary>
   ///    Level order traversal
   /// </summary>
   /// <returns>Level order's iterator</returns>
   public IEnumerable<TKey?> GetLevelOrdered()
   {
      var keyQueue = new Queue<TKey?>();
      var nodeQueue = new Queue<Node?>();
      nodeQueue.Enqueue(_root);
      while (nodeQueue.Count != 0)
      {
         var node = nodeQueue.Dequeue();
         if (node == null)
         {
            continue;
         }

         keyQueue.Enqueue(node.Key);
         nodeQueue.Enqueue(node.Left);
         nodeQueue.Enqueue(node.Right);
      }

      return keyQueue;
   }

   private sealed class Node
   {
      public Node(TKey? key, TValue? val, int size)
      {
         Key = key;
         Val = val;
         Size = size;
      }

      /// <summary>
      ///    Sorted by key
      /// </summary>
      public TKey? Key { get; }

      /// <summary>
      ///    Associated data
      /// </summary>
      public TValue? Val { get; set; }

      /// <summary>
      ///    Left subtree
      /// </summary>
      public Node? Left { get; set; }

      /// <summary>
      ///    Right subtree
      /// </summary>
      public Node? Right { get; set; }

      /// <summary>
      ///    Number of nodes in subtree
      /// </summary>
      public int Size { get; set; }
   }

   #region Integrity of BST data structure

   public bool Check() =>
      IsBst() && IsSizeConsistent() && IsRankConsistent();

   // does this binary tree satisfy symmetric order?
   // NOTE: this test also ensures that data structure is a binary tree since order is strict
   private bool IsBst() =>
      IsBst(_root, default, default);

   // is the tree rooted at x a BST with all keys strictly between min and max
   // (if min or max is null, treat as empty constraint)
   private static bool IsBst(Node? node, TKey? min, TKey? max)
   {
      if (node == null)
      {
         return true;
      }

      var nodeKey = node.Key;
      if (nodeKey == null)
      {
         throw new ArgumentException(nameof(nodeKey));
      }

      if (!KeyComparer.Equals(min, default) && nodeKey.CompareTo(min) <= 0)
      {
         return false;
      }

      if (!KeyComparer.Equals(max, default) && nodeKey.CompareTo(max) >= 0)
      {
         return false;
      }

      return IsBst(node.Left, min, nodeKey) && IsBst(node.Right, nodeKey, max);
   }

   // are the size fields correct?
   private bool IsSizeConsistent() => IsSizeConsistent(_root);

   private static bool IsSizeConsistent(Node? x)
   {
      if (x == null)
      {
         return true;
      }

      if (x.Size != GetSize(x.Left) + GetSize(x.Right) + 1)
      {
         return false;
      }

      return IsSizeConsistent(x.Left) && IsSizeConsistent(x.Right);
   }

   // check that ranks are consistent
   private bool IsRankConsistent()
   {
      for (var i = 0; i < Size; i++)
      {
         if (i != Rank(this[i]))
         {
            return false;
         }
      }

      return Keys.All(key => key.CompareTo(this[Rank(key)]) == 0);
   }

   #endregion
}