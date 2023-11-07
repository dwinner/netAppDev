using System.Diagnostics;
using System.Runtime.CompilerServices;

#if DEBUG
[assembly: InternalsVisibleTo("MiscThings.Tests")]
#endif

namespace MiscThings.Trees;

/// <summary>
///    The <see cref="AvlBinarySearchTree{TKey,TVal}" /> class represents an ordered symbol table of
///    generic key-value pairs. It supports the usual <em>put</em>, <em>get</em>,
///    <em>contains</em>, <em>delete</em>, <em>size</em>, and <em>is-empty</em>
///    methods. It also provides ordered methods for finding the <em>minimum</em>,
///    <em>maximum</em>, <em>floor</em>, and <em>ceiling</em>. It also provides a
///    <em>keys</em> method for iterating over all of the keys. A symbol table
///    implements the <em>associative array</em> abstraction: when associating a
///    value with a key that is already in the symbol table, the convention is to
///    replace the old value with the new value. Unlike <see cref="Dictionary{TKey,TValue}" />, this
///    class uses the convention that values cannot be null,
///    the value associated with a key to null is
///    equivalent to deleting the key from the symbol table.
///    <p>
///       This symbol table implementation uses internally an
///       <a href="https://en.wikipedia.org/wiki/AVL_tree"> AVL tree </a> (Georgy
///       Adelson-Velsky and Evgenii Landis' tree) which is a self-balancing BST.
///       In an AVL tree, the heights of the two child subtrees of any
///       node differ by at most one; if at any time they differ by more than one,
///       rebalancing is done to restore this property.
///    </p>
///    <p>
///       This implementation requires that the key type implements the
///       <see cref="IComparable{T}" /> interface and calls the <see cref="IComparable{T}.CompareTo" /> and
///       method to compare two keys. It does not call either equals() or
///       hashCode(). The <em>put</em>, <em>get</em>, <em>contains</em>,
///       <em>delete</em>, <em>minimum</em>, <em>maximum</em>, <em>ceiling</em>, and
///       <em>floor</em> operations each take logarithmic time in the worst case. The
///       <em>size</em>, and <em>is-empty</em> operations take constant time.
///       Construction also takes constant time.
///    </p>
/// </summary>
/// <typeparam name="TKey">Key type</typeparam>
/// <typeparam name="TVal">Stored value type</typeparam>
public sealed class AvlBinarySearchTree<TKey, TVal>
   where TKey : class, IComparable<TKey>
   where TVal : class
{
   private Node? _root; // The root node.

   /// <summary>
   ///    Checks if the symbol table is empty.
   /// </summary>
   public bool IsEmpty => _root == null;

   /// <summary>
   ///    the number key-value pairs in the symbol table.
   /// </summary>
   public int Size => GetSize(_root);

   /// <summary>
   ///    The height of the internal AVL tree.
   ///    <remarks>
   ///       It is assumed that the height of an empty tree is -1 and the height of a tree with just one node is 0.
   ///    </remarks>
   /// </summary>
   public int Height => GetHeight(_root);

   /// <summary>
   ///    Returns the value associated with the given key.
   /// </summary>
   /// <param name="key">The key</param>
   /// <returns>
   ///    The value associated with the given key if the key is in the symbol table and null if the key is not in the
   ///    symbol table
   /// </returns>
   /// <exception cref="ArgumentNullException">If <paramref name="key" /> is null</exception>
   public TVal? this[TKey key]
   {
      get
      {
         if (key == null)
         {
            throw new ArgumentNullException(nameof(key));
         }

         var node = Get(_root, key);
         return node?.Value;
      }
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

         _root = Put(_root, key, value);

#if TRACE
         Debug.WriteLineIf(Check(out var errorMsg), errorMsg);
#endif
      }
   }

   /// <summary>
   ///    Checks if the symbol table contains the given key.
   /// </summary>
   /// <param name="key">The key</param>
   /// <returns>true if the symbol table contains <paramref name="key" /></returns>
   public bool Contains(TKey key) => this[key] != null;

   /// <summary>
   ///    Removes the specified key and its associated value from the symbol table
   ///    (if the key is in the symbol table).
   /// </summary>
   /// <param name="key">The key</param>
   /// <exception cref="ArgumentNullException">If <paramref name="key" /> is null</exception>
   public void Delete(TKey key)
   {
      if (key == null)
      {
         throw new ArgumentNullException(nameof(key));
      }

      if (!Contains(key))
      {
         return;
      }

      _root = Delete(_root!, key);

#if TRACE
      Debug.WriteLineIf(Check(out var errorMsg), errorMsg);
#endif
   }

   /// <summary>
   ///    Removes the smallest key and associated value from the symbol table.
   /// </summary>
   /// <exception cref="InvalidOperationException">If the symbol table is empty</exception>
   public void DeleteMin()
   {
      if (IsEmpty)
      {
         throw new InvalidOperationException("called deleteMin() with empty symbol table");
      }

      _root = DeleteMin(_root!);

#if TRACE
      Debug.WriteLineIf(Check(out var errorMsg), errorMsg);
#endif
   }

   /// <summary>
   ///    Removes the largest key and associated value from the symbol table.
   /// </summary>
   /// <exception cref="InvalidOperationException">If the symbol table is empty</exception>
   public void DeleteMax()
   {
      if (IsEmpty)
      {
         throw new InvalidOperationException("called deleteMax() with empty symbol table");
      }

      _root = DeleteMax(_root!);

#if TRACE
      Debug.WriteLineIf(Check(out var errorMsg), errorMsg);
#endif
   }

   /// <summary>
   ///    The smallest key in the symbol table.
   /// </summary>
   /// <returns>The smallest key in the symbol table</returns>
   /// <exception cref="InvalidOperationException">If the symbol table is empty</exception>
   public TKey GetMin()
   {
      if (IsEmpty)
      {
         throw new InvalidOperationException("called min() with empty symbol table");
      }

      return GetMin(_root!).Key;
   }

   /// <summary>
   ///    Returns the largest key in the symbol table.
   /// </summary>
   /// <returns>The largest key in the symbol table</returns>
   /// <exception cref="InvalidOperationException">If the symbol table is empty</exception>
   public TKey GetMax()
   {
      if (IsEmpty)
      {
         throw new InvalidOperationException("called max() with empty symbol table");
      }

      return GetMax(_root!).Key;
   }

   /// <summary>
   ///    Returns the largest key in the symbol table less than or equal to <paramref name="key" />
   /// </summary>
   /// <param name="key">The key</param>
   /// <returns>The largest key in the symbol table less than or equal to <paramref name="key" /></returns>
   /// <exception cref="ArgumentNullException">If <paramref name="key" /> is null</exception>
   /// <exception cref="InvalidOperationException">If the symbol table is empty</exception>
   public TKey? GetFloor(TKey key)
   {
      if (key == null)
      {
         throw new ArgumentNullException(nameof(key), "argument to floor() is null");
      }

      if (IsEmpty)
      {
         throw new InvalidOperationException("called floor() with empty symbol table");
      }

      var floorNode = GetFloor(_root, key);
      return floorNode?.Key;
   }

   /// <summary>
   ///    Returns the smallest key in the symbol table greater than or equal to <paramref name="key" />
   /// </summary>
   /// <param name="key">The key</param>
   /// <returns>The smallest key in the symbol table greater than or equal to <paramref name="key" /></returns>
   /// <exception cref="ArgumentNullException">If <paramref name="key" /> is null</exception>
   /// <exception cref="InvalidOperationException">If the symbol table is empty</exception>
   public TKey? GetCeiling(TKey key)
   {
      if (key == null)
      {
         throw new ArgumentNullException(nameof(key));
      }

      if (IsEmpty)
      {
         throw new InvalidOperationException("called ceiling() with empty symbol table");
      }

      var ceilingNode = GetCeiling(_root, key);
      return ceilingNode?.Key;
   }

   /// <summary>
   ///    Returns the kth smallest key in the symbol table.
   /// </summary>
   /// <param name="orderedKey">The order statistic</param>
   /// <returns>The kth smallest key in the symbol table</returns>
   /// <exception cref="ArgumentOutOfRangeException"><paramref name="orderedKey" /> is out of range</exception>
   public TKey? Select(int orderedKey)
   {
      if (orderedKey < 0 || orderedKey >= Size)
      {
         throw new ArgumentOutOfRangeException(nameof(orderedKey), $"k is not in range 0-{Size - 1}");
      }

      var selectedNode = Select(_root, orderedKey);
      return selectedNode?.Key;
   }

   /// <summary>
   ///    Returns the number of keys in the symbol table strictly less than <paramref name="key" />
   /// </summary>
   /// <param name="key">The key</param>
   /// <returns>The number of keys in the symbol table strictly less than <paramref name="key" /></returns>
   /// <exception cref="ArgumentNullException">If <paramref name="key" /> is null</exception>
   public int GetRank(TKey key)
   {
      if (key == null)
      {
         throw new ArgumentNullException(nameof(key));
      }

      return GetRank(key, _root);
   }

   /// <summary>
   ///    Returns all keys in the symbol table.
   /// </summary>
   /// <returns>All keys in the symbol table.</returns>
   public IEnumerable<TKey> GetKeys() => GetKeysInOrder();

   /// <summary>
   ///    All keys in the symbol table following an in-order traversal.
   /// </summary>
   /// <returns>All keys in the symbol table following an in-order traversal</returns>
   private IEnumerable<TKey> GetKeysInOrder()
   {
      var queue = new Queue<TKey>();
      GetKeysInOrder(_root, queue);
      return queue;
   }

   /// <summary>
   ///    Returns all keys in the symbol table following a level-order traversal.
   /// </summary>
   /// <returns>All keys in the symbol table following a level-order traversal.</returns>
   public IEnumerable<TKey> GetKeysLevelOrder()
   {
      if (IsEmpty)
      {
         return Enumerable.Empty<TKey>();
      }

      var queue = new Queue<TKey>();
      var queue2 = new Queue<Node>();
      queue2.Enqueue(_root!);
      while (queue2.Count > 0)
      {
         var dequeued = queue2.Dequeue();
         queue.Enqueue(dequeued.Key);
         if (dequeued.Left != null)
         {
            queue2.Enqueue(dequeued.Left);
         }

         if (dequeued.Right != null)
         {
            queue2.Enqueue(dequeued.Right);
         }
      }

      return queue;
   }

   /// <summary>
   ///    Returns all keys in the symbol table in the given range.
   /// </summary>
   /// <param name="lowKey">The lowest key</param>
   /// <param name="highKey">The highest key</param>
   /// <returns>
   ///    All keys in the symbol table between <paramref name="lowKey" /> (inclusive) and <paramref name="highKey" />
   ///    (exclusive)
   /// </returns>
   /// <exception cref="ArgumentNullException">If either <paramref name="lowKey" /> or <paramref name="highKey" /> is null</exception>
   public IEnumerable<TKey> GetKeys(TKey lowKey, TKey highKey)
   {
      if (lowKey == null)
      {
         throw new ArgumentNullException(nameof(lowKey));
      }

      if (highKey == null)
      {
         throw new ArgumentNullException(nameof(highKey));
      }

      var queue = new Queue<TKey>();
      GetKeys(_root, queue, lowKey, highKey);

      return queue;
   }

   /// <summary>
   ///    Returns the number of keys in the symbol table in the given range.
   /// </summary>
   /// <param name="lowKey">Minimum endpoint</param>
   /// <param name="highKey">Maximum endpoint</param>
   /// <returns>
   ///    The number of keys in the symbol table between <paramref name="lowKey" /> (inclusive) and
   ///    <paramref name="highKey" />
   ///    (exclusive)
   /// </returns>
   /// <exception cref="ArgumentNullException">If one of parameters is null</exception>
   public int GetSize(TKey lowKey, TKey highKey)
   {
      if (lowKey == null)
      {
         throw new ArgumentNullException(nameof(lowKey), "first argument to size() is null");
      }

      if (highKey == null)
      {
         throw new ArgumentNullException(nameof(highKey), "second argument to size() is null");
      }

      return lowKey.CompareTo(highKey) > 0
         ? 0
         : Contains(highKey)
            ? GetRank(highKey) - GetRank(lowKey) + 1
            : GetRank(highKey) - GetRank(lowKey);
   }

   private static int GetSize(Node? node) => node?.Size ?? 0;

   private static int GetHeight(Node? x) => x?.Height ?? -1;

   private static Node? Get(Node? node, TKey key)
   {
      if (node == null)
      {
         return null;
      }

      var cmp = key.CompareTo(node.Key);
      return cmp switch
      {
         < 0 => Get(node.Left, key),
         > 0 => Get(node.Right, key),
         _ => node
      };
   }

   private static Node Put(Node? node, TKey key, TVal val)
   {
      if (node == null)
      {
         return new Node(key, val, 0, 1);
      }

      var cmp = key.CompareTo(node.Key);
      switch (cmp)
      {
         case < 0:
            node.Left = Put(node.Left, key, val);
            break;
         case > 0:
            node.Right = Put(node.Right, key, val);
            break;
         default:
            node.Value = val;
            return node;
      }

      node.Size = 1 + GetSize(node.Left) + GetSize(node.Right);
      node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

      return Balance(node);
   }

   /// <summary>
   ///    Restores the AVL tree property of the subtree.
   /// </summary>
   /// <param name="node">The subtree</param>
   /// <returns>The subtree with restored AVL property</returns>
   private static Node Balance(Node node)
   {
      var nodeBalance = BalanceFactor(node);
      switch (nodeBalance)
      {
         case < -1:
         {
            if (BalanceFactor(node.Right!) > 0)
            {
               node.Right = RotateRight(node.Right!);
            }

            node = RotateLeft(node);
            break;
         }
         case > 1:
         {
            if (BalanceFactor(node.Left!) < 0)
            {
               node.Left = RotateLeft(node.Left!);
            }

            node = RotateRight(node);
            break;
         }
      }

      return node;
   }

   /// <summary>
   ///    Returns the balance factor of the subtree. The balance factor is defined
   ///    as the difference in height of the left subtree and right subtree, in
   ///    this order. Therefore, a subtree with a balance factor of -1, 0 or 1 has
   ///    the AVL property since the heights of the two child subtrees differ by at
   ///    most one.
   /// </summary>
   /// <param name="node">The subtree</param>
   /// <returns>The balance factor of the subtree</returns>
   private static int BalanceFactor(Node node) => GetHeight(node.Left) - GetHeight(node.Right);

   /// <summary>
   ///    Rotates the given subtree to the right.
   /// </summary>
   /// <param name="node">The subtree</param>
   /// <returns>The right rotated subtree</returns>
   private static Node RotateRight(Node node)
   {
      var left = node.Left;
      node.Left = left!.Right;
      left.Right = node;
      left.Size = node.Size;
      node.Size = 1 + GetSize(node.Left) + GetSize(node.Right);
      node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
      left.Height = 1 + Math.Max(GetHeight(left.Left), GetHeight(left.Right));

      return left;
   }

   /// <summary>
   ///    Rotates the given subtree to the left.
   /// </summary>
   /// <param name="node">The subtree</param>
   /// <returns>Left rotated subtree</returns>
   private static Node RotateLeft(Node node)
   {
      var right = node.Right;
      node.Right = right!.Left;
      right.Left = node;
      right.Size = node.Size;
      node.Size = 1 + GetSize(node.Left) + GetSize(node.Right);
      node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
      right.Height = 1 + Math.Max(GetHeight(right.Left), GetHeight(right.Right));

      return right;
   }

   private static Node Delete(Node node, TKey key)
   {
      var cmp = key.CompareTo(node.Key);
      switch (cmp)
      {
         case < 0:
            node.Left = Delete(node.Left!, key);
            break;
         case > 0:
            node.Right = Delete(node.Right!, key);
            break;
         default:
         {
            if (node.Left == null)
            {
               return node.Right!;
            }

            if (node.Right == null)
            {
               return node.Left;
            }

            var current = node;
            node = GetMin(current.Right);
            node.Right = DeleteMin(current.Right);
            node.Left = current.Left;
            break;
         }
      }

      node.Size = 1 + GetSize(node.Left) + GetSize(node.Right);
      node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

      return Balance(node);
   }

   private static Node DeleteMin(Node node)
   {
      if (node.Left == null)
      {
         return node.Right!;
      }

      node.Left = DeleteMin(node.Left);
      node.Size = 1 + GetSize(node.Left) + GetSize(node.Right);
      node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

      return Balance(node);
   }

   private static Node DeleteMax(Node node)
   {
      if (node.Right == null)
      {
         return node.Left!;
      }

      node.Right = DeleteMax(node.Right);
      node.Size = 1 + GetSize(node.Left) + GetSize(node.Right);
      node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

      return Balance(node);
   }

   private static Node GetMin(Node node) => node.Left == null
      ? node
      : GetMin(node.Left);

   private static Node GetMax(Node node) => node.Right == null
      ? node
      : GetMax(node.Right);

   private static Node? GetFloor(Node? node, TKey key)
   {
      if (node == null)
      {
         return null;
      }

      var cmp = key.CompareTo(node.Key);
      switch (cmp)
      {
         case 0:
            return node;
         case < 0:
            return GetFloor(node.Left, key);
         default:
         {
            var floorNode = GetFloor(node.Right, key);
            return floorNode ?? node;
         }
      }
   }

   private static Node? GetCeiling(Node? node, TKey key)
   {
      if (node == null)
      {
         return null;
      }

      var cmp = key.CompareTo(node.Key);
      switch (cmp)
      {
         case 0:
            return node;
         case > 0:
            return GetCeiling(node.Right, key);
         default:
         {
            var ceilingNode = GetCeiling(node.Left, key);
            return ceilingNode ?? node;
         }
      }
   }

   private static Node? Select(Node? node, int orderedKey)
   {
      if (node == null)
      {
         return null;
      }

      var leftSize = GetSize(node.Left);
      return leftSize > orderedKey
         ? Select(node.Left, orderedKey)
         : leftSize < orderedKey
            ? Select(node.Right, orderedKey - leftSize - 1)
            : node;
   }

   private static int GetRank(TKey key, Node? node)
   {
      if (node == null)
      {
         return 0;
      }

      var cmp = key.CompareTo(node.Key);
      return cmp switch
      {
         < 0 => GetRank(key, node.Left),
         > 0 => 1 + GetSize(node.Left) + GetRank(key, node.Right),
         _ => GetSize(node.Left)
      };
   }

   private static void GetKeysInOrder(Node? node, Queue<TKey> queue)
   {
      if (node == null)
      {
         return;
      }

      GetKeysInOrder(node.Left, queue);
      queue.Enqueue(node.Key);
      GetKeysInOrder(node.Right, queue);
   }

   private static void GetKeys(Node? node, Queue<TKey> queue, TKey lowKey, TKey highKey)
   {
      if (node == null)
      {
         return;
      }

      var cmplo = lowKey.CompareTo(node.Key);
      var cmphi = highKey.CompareTo(node.Key);
      if (cmplo < 0)
      {
         GetKeys(node.Left, queue, lowKey, highKey);
      }

      if (cmplo <= 0 && cmphi >= 0)
      {
         queue.Enqueue(node.Key);
      }

      if (cmphi > 0)
      {
         GetKeys(node.Right, queue, lowKey, highKey);
      }
   }

   /// <summary>
   ///    This class represents an inner node of the AVL tree.
   /// </summary>
   private sealed class Node
   {
      public Node(TKey key, TVal value, int height, int size)
      {
         Key = key;
         Value = value;
         Height = height;
         Size = size;
      }

      /// <summary>
      ///    The key
      /// </summary>
      public TKey Key { get; }

      /// <summary>
      ///    The associated value
      /// </summary>
      public TVal Value { get; set; }

      /// <summary>
      ///    Height of the subtree
      /// </summary>
      public int Height { get; set; }

      /// <summary>
      ///    Number of nodes in subtree
      /// </summary>
      public int Size { get; set; }

      /// <summary>
      ///    Left subtree
      /// </summary>
      public Node? Left { get; set; }

      /// <summary>
      ///    Right subtree
      /// </summary>
      public Node? Right { get; set; }
   }

   #region AVL tree validation

#if DEBUG
   internal bool Check(out string errorMsg)
   {
      if (!IsBst())
      {
         errorMsg = "Symmetric order not consistent";
         return false;
      }

      if (!IsAvl())
      {
         errorMsg = "AVL property not consistent";
         return false;
      }

      if (!IsSizeConsistent())
      {
         errorMsg = "Subtree counts not consistent";
         return false;
      }

      if (!IsRankConsistent())
      {
         errorMsg = "Ranks not consistent";
         return false;
      }

      errorMsg = string.Empty;
      return true;
   }

   private bool IsAvl() => IsAvl(_root);

   private static bool IsAvl(Node? node)
   {
      if (node == null)
      {
         return true;
      }

      var balanceFactor = BalanceFactor(node);
      if (balanceFactor is > 1 or < -1)
      {
         return false;
      }

      return IsAvl(node.Left) && IsAvl(node.Right);
   }

   private bool IsBst() => IsBst(_root, null, null);

   private static bool IsBst(Node? node, TKey? minKey, TKey? maxKey)
   {
      if (node == null)
      {
         return true;
      }

      if (minKey != null && node.Key.CompareTo(minKey) <= 0)
      {
         return false;
      }

      if (maxKey != null && node.Key.CompareTo(maxKey) >= 0)
      {
         return false;
      }

      return IsBst(node.Left, minKey, node.Key) && IsBst(node.Right, node.Key, maxKey);
   }

   private bool IsSizeConsistent() => IsSizeConsistent(_root);

   private static bool IsSizeConsistent(Node? node)
   {
      if (node == null)
      {
         return true;
      }

      if (node.Size != GetSize(node.Left) + GetSize(node.Right) + 1)
      {
         return false;
      }

      return IsSizeConsistent(node.Left) && IsSizeConsistent(node.Right);
   }

   private bool IsRankConsistent()
   {
      for (var i = 0; i < Size; i++)
      {
         var selectedKey = Select(i);
         if (i != GetRank(selectedKey
                          ?? throw new InvalidOperationException($"{nameof(selectedKey)} is null")))
         {
            return false;
         }
      }

      return GetKeys().All(key => key.CompareTo(Select(GetRank(key))) == 0);
   }

#endif

   #endregion
}