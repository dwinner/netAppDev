using System.Diagnostics;
using System.Runtime.CompilerServices;

#if DEBUG
[assembly: InternalsVisibleTo("MiscThings.Tests")]
#endif

namespace MiscThings.Trees;

/// <summary>
///    The <see cref="RedBlackBinarySearchTree{TKey,TVal}" /> class represents an ordered symbol table of generic
///    key-value pairs.
///    It supports the usual <em>put</em>, <em>get</em>, <em>contains</em>,
///    <em>delete</em>, <em>size</em>, and <em>is-empty</em> methods.
///    It also provides ordered methods for finding the <em>minimum</em>,
///    <em>maximum</em>, <em>floor</em>, and <em>ceiling</em>.
///    It also provides a <em>keys</em> method for iterating over all of the keys.
///    A symbol table implements the <em>associative array</em> abstraction:
///    when associating a value with a key that is already in the symbol table,
///    the convention is to replace the old value with the new value.
///    Unlike Map, this class uses the convention that
///    values cannot be null-setting the
///    value associated with a key to null is equivalent to deleting the key
///    from the symbol table.
///    <p>
///       It requires that
///       the key type implements the IComparable interface and calls the
///       compareTo() and method to compare two keys. It does not call either
///       equals() or hashCode()
///    </p>
///    .
///    <p>
///       This implementation uses a <em>left-leaning red-black BST</em>.
///       The <em>put</em>, <em>get</em>, <em>contains</em>, <em>remove</em>,
///       <em>minimum</em>, <em>maximum</em>, <em>ceiling</em>, <em>floor</em>,
///       <em>rank</em>, and <em>select</em> operations each take
///       Theta(log <em>n</em>) time in the worst case, where <em>n</em> is the
///       number of key-value pairs in the symbol table.
///       The <em>size</em>, and <em>is-empty</em> operations take Theta(1) time.
///       The <em>keys</em> methods take
///       <em>O</em>(log <em>n</em> + <em>m</em>) time, where <em>m</em> is
///       the number of keys returned by the iterator.
///       Construction takes Theta(1) time.
///    </p>
/// </summary>
public sealed class RedBlackBinarySearchTree<TKey, TVal>
   where TKey : IComparable<TKey>
   where TVal : class
{
   private const bool Red = true;
   private const bool Black = !Red;

   // root of the BST
   private Node? _root;

   /// <summary>
   ///    The height of the BST (for debugging).
   /// </summary>
   public int Height => GetHeight(_root);

   /// <summary>
   ///    The number of key-value pairs in this symbol table.
   /// </summary>
   public int Size => GetSize(_root);

   /// <summary>
   ///    true if this symbol table is empty and false otherwise
   /// </summary>
   public bool IsEmpty => _root == null;

   /// <summary>
   ///    The value associated with the given key.
   /// </summary>
   /// <param name="key">The key</param>
   public TVal? this[TKey key]
   {
      get => Get(_root, key);
      set
      {
         if (value == null)
         {
            Delete(key);
            return;
         }

         _root = Put(_root, key, value);
         _root.Color = Black;
      }
   }

   /// <summary>
   ///    All keys in the symbol table in ascending order
   /// </summary>
   public IEnumerable<TKey> Keys => IsEmpty
      ? Enumerable.Empty<TKey>()
      : GetKeys(GetMin(), GetMax());

   /// <summary>
   ///    Does this symbol table contain the given key?
   /// </summary>
   /// <param name="key">The key</param>
   /// <returns>true if this symbol table contains <paramref name="key" /> and false otherwise</returns>
   public bool Contains(TKey key) => this[key] != null;

   /// <summary>
   ///    Removes the smallest key and associated value from the symbol table.
   /// </summary>
   /// <exception cref="InvalidOperationException">If the symbol table is empty</exception>
   public void DeleteMin()
   {
      if (IsEmpty)
      {
         throw new InvalidOperationException("BST underflow");
      }

      // if both children of root are black, set root to red
      if (!IsRed(_root!.Left) && !IsRed(_root.Right))
      {
         _root.Color = Red;
      }

      _root = DeleteMin(_root);
      if (!IsEmpty)
      {
         _root!.Color = Black;
      }
   }

   /// <summary>
   ///    Removes the largest key and associated value from the symbol table.
   /// </summary>
   /// <exception cref="InvalidOperationException">If the symbol table is empty</exception>
   public void DeleteMax()
   {
      if (IsEmpty)
      {
         throw new InvalidOperationException("BST underflow");
      }

      // if both children of root are black, set root to red
      if (!IsRed(_root!.Left) && !IsRed(_root.Right))
      {
         _root.Color = Red;
      }

      _root = DeleteMax(_root);
      if (!IsEmpty)
      {
         _root!.Color = Black;
      }
   }

   /// <summary>
   ///    Removes the specified key and its associated value from this symbol table
   /// </summary>
   /// <param name="key">The key</param>
   public void Delete(TKey key)
   {
      if (!Contains(key))
      {
         return;
      }

      // if both children of root are black, set root to red
      if (!IsRed(_root!.Left) && !IsRed(_root.Right))
      {
         _root.Color = Red;
      }

      _root = Delete(_root, key);
      if (!IsEmpty)
      {
         _root!.Color = Black;
      }
   }

   /// <summary>
   ///    Returns the smallest key in the symbol table.
   /// </summary>
   /// <returns>The smallest key in the symbol table</returns>
   /// <exception cref="InvalidOperationException">If the symbol table is empty</exception>
   public TKey GetMin()
   {
      if (IsEmpty)
      {
         throw new InvalidOperationException("Calls min() with empty symbol table");
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
         throw new InvalidOperationException("calls max() with empty symbol table");
      }

      return GetMax(_root!).Key;
   }

   /// <summary>
   ///    Returns the largest key in the symbol table less than or equal to <paramref name="key" />.
   /// </summary>
   /// <param name="key">The key</param>
   /// <returns>The largest key in the symbol table less than or equal to <paramref name="key" /></returns>
   /// <exception cref="InvalidOperationException">If <paramref name="key" /> is null</exception>
   public TKey GetFloor(TKey key)
   {
      if (IsEmpty)
      {
         throw new InvalidOperationException("calls floor() with empty symbol table");
      }

      var floorNode = GetFloor(_root, key);
      return floorNode == null
         ? throw new InvalidOperationException("argument to floor() is too small")
         : floorNode.Key;
   }

   /// <summary>
   ///    Returns the smallest key in the symbol table greater than or equal to <paramref name="key" />.
   /// </summary>
   /// <param name="key">The key</param>
   /// <returns>The smallest key in the symbol table greater than or equal to <paramref name="key" /></returns>
   /// <exception cref="InvalidOperationException">If there is no such key</exception>
   public TKey GetCeiling(TKey key)
   {
      if (IsEmpty)
      {
         throw new InvalidOperationException("calls ceiling() with empty symbol table");
      }

      var ceilingNode = GetCeiling(_root, key);
      return ceilingNode == null
         ? throw new InvalidOperationException("argument to ceiling() is too large")
         : ceilingNode.Key;
   }

   /// <summary>
   ///    Return the key in the symbol table of a given <paramref name="rank" />.
   /// </summary>
   /// <remarks>
   ///    This key has the property that there are <paramref name="rank" /> keys in
   ///    the symbol table that are smaller. In other words, this key is the (<paramref name="rank" /> + 1)st smallest
   ///    key in the symbol table.
   /// </remarks>
   /// <param name="rank">The order statistic</param>
   /// <returns>The key in the symbol table of given <paramref name="rank" /></returns>
   /// <exception cref="InvalidOperationException">Unless <paramref name="rank" /> is between 0 and <em>Size</em>–1</exception>
   public TKey? Select(int rank)
   {
      if (rank < 0 || rank >= Size)
      {
         throw new InvalidOperationException($"argument to select() is invalid: {rank}");
      }

      return Select(_root, rank);
   }

   /// <summary>
   ///    Return the number of keys in the symbol table strictly less than <paramref name="key" />.
   /// </summary>
   /// <param name="key">The key</param>
   /// <returns>The number of keys in the symbol table strictly less than <paramref name="key" /></returns>
   /// <exception cref="InvalidOperationException">If <paramref name="key" /> is null</exception>
   public int GetRank(TKey? key)
   {
      if (key == null)
      {
         throw new InvalidOperationException("argument to rank() is null");
      }

      return GetRank(key, _root);
   }

   /// <summary>
   ///    Returns all keys in the symbol table in the given range in ascending order, as an <see cref="IEnumerable{TKey}" />.
   /// </summary>
   /// <param name="lowKey">Minimum endpoint</param>
   /// <param name="highKey">Maximum endpoint</param>
   /// <returns>
   ///    All keys in the symbol table between <paramref name="lowKey" /> (inclusive) and <paramref name="highKey" />
   ///    (inclusive)
   ///    in ascending order
   /// </returns>
   /// <exception cref="ArgumentNullException">If either <paramref name="lowKey" /> or <paramref name="highKey" /> is null</exception>
   public IEnumerable<TKey> GetKeys(TKey lowKey, TKey highKey)
   {
      if (lowKey == null)
      {
         throw new ArgumentNullException(nameof(lowKey), "first argument to keys() is null");
      }

      if (highKey == null)
      {
         throw new ArgumentNullException(nameof(highKey), "second argument to keys() is null");
      }

      Queue<TKey> queue = new();
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
   ///    (inclusive)
   /// </returns>
   /// <exception cref="ArgumentNullException">If either <paramref name="lowKey" /> or <paramref name="highKey" /> is null</exception>
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

      if (lowKey.CompareTo(highKey) > 0)
      {
         return 0;
      }

      if (Contains(highKey))
      {
         return GetRank(highKey) - GetRank(lowKey) + 1;
      }

      return GetRank(highKey) - GetRank(lowKey);
   }

   // add the keys between lo and hi in the subtree rooted at x
   // to the queue
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

   // the smallest key in subtree rooted at node; null if no such key
   private static Node GetMin(Node node) => node.Left == null
      ? node
      : GetMin(node.Left);

   // the largest key in the subtree rooted at x; null if no such key
   private static Node GetMax(Node node) => node.Right == null
      ? node
      : GetMax(node.Right);

   // the largest key in the subtree rooted at x less than or equal to the given key
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

   // the smallest key in the subtree rooted at x greater than or equal to the given key
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

   // Return key in BST rooted at x of given rank.
   // Precondition: rank is in legal range.
   private static TKey? Select(Node? node, int rank)
   {
      if (node == null)
      {
         // NOTEME: what about legal struct values like int's 0?!
         return default;
      }

      var leftSize = GetSize(node.Left);
      return leftSize > rank
         ? Select(node.Left, rank)
         : leftSize < rank
            ? Select(node.Right, rank - leftSize - 1)
            : node.Key;
   }

   // number of keys less than key in the subtree rooted at x
   private static int GetRank(TKey key, Node? node)
   {
      if (node == null)
      {
         return 0;
      }

      var cmp = key.CompareTo(node.Key);
      return cmp < 0
         ? GetRank(key, node.Left)
         : cmp > 0
            ? 1 + GetSize(node.Left) + GetRank(key, node.Right)
            : GetSize(node.Left);
   }

   // delete the key-value pair with the minimum key rooted at node
   private static Node? DeleteMin(Node node)
   {
      if (node.Left == null)
      {
         return null;
      }

      if (!IsRed(node.Left) && !IsRed(node.Left.Left))
      {
         node = MoveRedLeft(node);
      }

      node.Left = DeleteMin(node.Left!);

      return Balance(node);
   }

   // delete the key-value pair with the maximum key rooted at node
   private static Node? DeleteMax(Node node)
   {
      if (IsRed(node.Left))
      {
         node = RotateRight(node);
      }

      if (node.Right == null)
      {
         return null;
      }

      if (!IsRed(node.Right) && !IsRed(node.Right.Left))
      {
         node = MoveRedRight(node);
      }

      node.Right = DeleteMax(node.Right!);

      return Balance(node);
   }

   // delete the key-value pair with the given key rooted at node
   private Node? Delete(Node node, TKey key)
   {
      if (key.CompareTo(node.Key) < 0)
      {
         if (!IsRed(node.Left) && !IsRed(node.Left!.Left))
         {
            node = MoveRedLeft(node);
         }

         node.Left = Delete(node.Left!, key);
      }
      else
      {
         if (IsRed(node.Left))
         {
            node = RotateRight(node);
         }

         if (key.CompareTo(node.Key) == 0 && node.Right == null)
         {
            return null;
         }

         if (!IsRed(node.Right) && !IsRed(node.Right!.Left))
         {
            node = MoveRedRight(node);
         }

         if (key.CompareTo(node.Key) == 0)
         {
            var minNode = GetMin(node.Right!);
            node.Key = minNode.Key;
            node.Value = minNode.Value;
            node.Right = DeleteMin(node.Right!);
         }
         else
         {
            node.Right = Delete(node.Right!, key);
         }
      }

      return Balance(node);
   }

   // insert the key-value pair in the subtree rooted at node
   private Node Put(Node? node, TKey key, TVal val)
   {
      if (node == null)
      {
         return new Node(key, val, Red, 1);
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
            break;
      }

      // fix-up any right-leaning links
      if (IsRed(node.Right) && !IsRed(node.Left))
      {
         node = RotateLeft(node);
      }

      if (IsRed(node.Left) && IsRed(node.Left?.Left))
      {
         node = RotateRight(node);
      }

      if (IsRed(node.Left) && IsRed(node.Right))
      {
         FlipColors(node);
      }

      node.Size = GetSize(node.Left) + GetSize(node.Right) + 1;

      return node;
   }

   private static int GetHeight(Node? node) => node == null
      ? -1
      : 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

   // is node x red; false if x is null ?
   private static bool IsRed(Node? node) => node is { Color: Red };

   // number of node in subtree rooted at x; 0 if x is null
   private static int GetSize(Node? node) => node?.Size ?? 0;

   // value associated with the given key in subtree rooted at x; default if no such key
   private static TVal? Get(Node? node, TKey key)
   {
      while (node != null)
      {
         var cmp = key.CompareTo(node.Key);
         switch (cmp)
         {
            case < 0:
               node = node.Left;
               break;
            case > 0:
               node = node.Right;
               break;
            default:
               return node.Value;
         }
      }

      return null;
   }

   // make a left-leaning link lean to the right
   private static Node RotateRight(Node node)
   {
      var leftNode = node.Left;
#if DEBUG
      Debug.Assert(IsRed(leftNode));
#endif
      node.Left = leftNode!.Right;
      leftNode.Right = node;
      leftNode.Color = node.Color;
      node.Color = Red;
      leftNode.Size = node.Size;
      node.Size = GetSize(node.Left) + GetSize(node.Right) + 1;

      return leftNode;
   }

   // make a right-leaning link lean to the left
   private static Node RotateLeft(Node node)
   {
      var rightNode = node.Right;
#if DEBUG
      Debug.Assert(IsRed(rightNode));
#endif
      node.Right = rightNode!.Left;
      rightNode.Left = node;
      rightNode.Color = node.Color;
      node.Color = Red;
      rightNode.Size = node.Size;
      node.Size = GetSize(node.Left) + GetSize(node.Right) + 1;

      return rightNode;
   }

   // flip the colors of a node and its two children
   private static void FlipColors(Node node)
   {
      node.Color = !node.Color;
      node.Left!.Color = !node.Left.Color;
      node.Right!.Color = !node.Right.Color;
   }

   // Assuming that h is red and both h.left and h.left.left
   // are black, make h.left or one of its children red.
   private static Node MoveRedLeft(Node node)
   {
      FlipColors(node);
      if (IsRed(node.Right!.Left))
      {
         node.Right = RotateRight(node.Right);
         node = RotateLeft(node);
         FlipColors(node);
      }

      return node;
   }

   // Assuming that h is red and both h.right and h.right.left
   // are black, make h.right or one of its children red.
   private static Node MoveRedRight(Node node)
   {
      FlipColors(node);
      if (IsRed(node.Left!.Left))
      {
         node = RotateRight(node);
         FlipColors(node);
      }

      return node;
   }

   // restore red-black tree invariant
   private static Node Balance(Node node)
   {
      if (IsRed(node.Right) && !IsRed(node.Left))
      {
         node = RotateLeft(node);
      }

      if (IsRed(node.Left) && IsRed(node.Left!.Left))
      {
         node = RotateRight(node);
      }

      if (IsRed(node.Left) && IsRed(node.Right))
      {
         FlipColors(node);
      }

      node.Size = GetSize(node.Left) + GetSize(node.Right) + 1;

      return node;
   }

   /// <summary>
   ///    BST helper node data type
   /// </summary>
   private sealed class Node
   {
      public Node(TKey key, TVal? value, bool color, int size)
      {
         Key = key;
         Value = value;
         Color = color;
         Size = size;
      }

      /// <summary>
      ///    Key
      /// </summary>
      public TKey Key { get; set; }

      /// <summary>
      ///    Associated data
      /// </summary>
      public TVal? Value { get; set; }

      /// <summary>
      ///    Left subtree
      /// </summary>
      public Node? Left { get; set; }

      /// <summary>
      ///    Right subtree
      /// </summary>
      public Node? Right { get; set; }

      /// <summary>
      ///    Color of parent link
      /// </summary>
      public bool Color { get; set; }

      /// <summary>
      ///    Subtree count
      /// </summary>
      public int Size { get; set; }
   }

#if DEBUG

   #region Check integrity of red-black tree data structure.

   internal bool Check(out string errorMessage)
   {
      if (!IsBst())
      {
         errorMessage = "Not in symmetric order";
         return false;
      }

      if (!IsSizeConsistent())
      {
         errorMessage = "Subtree counts not consistent";
         return false;
      }

      if (!IsRankConsistent())
      {
         errorMessage = "Ranks not consistent";
         return false;
      }

      if (!Is23())
      {
         errorMessage = "Not a 2-3 tree";
         return false;
      }

      if (!IsBalanced())
      {
         errorMessage = "Not balanced";
         return false;
      }

      errorMessage = string.Empty;
      return true;
   }

   // does this binary tree satisfy symmetric order?
   // Note: this test also ensures that data structure is a binary tree since order is strict
   private bool IsBst() => IsBst(_root, default, default);

   // is the tree rooted at x a BST with all keys strictly between min and max
   // (if min or max is null, treat as empty constraint)
   // Credit: elegant solution due to Bob Dondero
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

      return IsBst(node.Left, minKey, node.Key)
             && IsBst(node.Right, node.Key, maxKey);
   }

   // are the size fields correct?
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

      return IsSizeConsistent(node.Left)
             && IsSizeConsistent(node.Right);
   }

   // check that ranks are consistent
   private bool IsRankConsistent()
   {
      for (var i = 0; i < Size; i++)
      {
         if (i != GetRank(Select(i)))
         {
            return false;
         }
      }

      return Keys.All(key => key.CompareTo(Select(GetRank(key))) == 0);
   }

   // Does the tree have no red right links, and at most one (left)
   // red links in a row on any path?
   private bool Is23() => Is23(_root);

   private bool Is23(Node? node)
   {
      if (node == null)
      {
         return true;
      }

      if (IsRed(node.Right))
      {
         return false;
      }

      if (node != _root && IsRed(node) && IsRed(node.Left))
      {
         return false;
      }

      return Is23(node.Left)
             && Is23(node.Right);
   }

   // do all paths from root to leaf have same number of black edges?
   private bool IsBalanced()
   {
      var black = 0; // number of black links on path from root to min
      var current = _root;
      while (current != null)
      {
         if (!IsRed(current))
         {
            black++;
         }

         current = current.Left;
      }

      return IsBalanced(_root, black);
   }

   // does every path from the root to a leaf have the given number of black links?
   private static bool IsBalanced(Node? node, int black)
   {
      if (node == null)
      {
         return black == 0;
      }

      if (!IsRed(node))
      {
         black--;
      }

      return IsBalanced(node.Left, black)
             && IsBalanced(node.Right, black);
   }

   #endregion

#endif
}