using System.Diagnostics;

namespace Searching.Algs;

/// <summary>
///    The BST class represents an ordered symbol table of generic
///    key-value pairs.
///    It supports the usual <em>put</em>, <em>get</em>, <em>contains</em>,
///    <em>delete</em>, <em>size</em>, and <em>is-empty</em> methods.
///    It also provides ordered methods for finding the <em>minimum</em>,
///    <em>maximum</em>, <em>floor</em>, and <em>ceiling</em>.
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
///       Construction takes Theta (1) time.
///    </p>
/// </summary>
public sealed class RedBlackTree<TKey, TValue>
   where TKey : IComparable<TKey>
{
   private const bool Red = true;
   private const bool Black = false;
   private Node? _root; // root of the BST

   /// <summary>
   ///    Returns the value associated with the given key.
   /// </summary>
   /// <param name="key">Key</param>
   /// <returns>
   ///    the value associated with the given key if the key is in the symbol table and null if the key is not in the
   ///    symbol table
   /// </returns>
   /// <exception cref="ArgumentNullException">if <paramref name="key" /> is null</exception>
   public TValue? this[TKey key]
   {
      get
      {
         if (key == null)
         {
            throw new ArgumentNullException(nameof(key));
         }

         return Get(_root, key);
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
         _root.Color = Black;
      }
   }

   /// <summary>
   ///    Returns the smallest key in the symbol table.
   /// </summary>
   /// <value>The smallest key in the symbol table</value>
   /// <exception cref="InvalidOperationException">If the symbol table is empty</exception>
   public TKey Min
   {
      get
      {
         if (IsEmpty())
         {
            throw new InvalidOperationException($"Calling {nameof(Min)} for empty symbol table");
         }

         return GetMin(_root).Key;
      }
   }

   /// <summary>
   ///    Returns the largest key in the symbol table.
   /// </summary>
   /// <value>The largest key in the symbol table</value>
   /// <exception cref="InvalidOperationException">If the symbol table is empty</exception>
   public TKey Max
   {
      get
      {
         if (IsEmpty())
         {
            throw new InvalidOperationException($"Calling {nameof(GetMax)} for empty symbol table");
         }

         return GetMax(_root).Key;
      }
   }

   /// <summary>
   ///    Return the key in the symbol table of a given <paramref name="rank" />
   ///    This key has the property that there are <paramref name="rank" /> keys in
   ///    the symbol table that are smaller. In other words, this key is the
   ///    (<paramref name="rank" />+1)st smallest key in the symbol table.
   /// </summary>
   /// <param name="rank">The order statistic</param>
   /// <returns>The key in the symbol table of given <paramref name="rank" /></returns>
   /// <exception cref="ArgumentOutOfRangeException">Unless <paramref name="rank" /> is between 0 and <em>n</em>–1</exception>
   public TKey? this[int rank]
   {
      get
      {
         if (rank < 0 || rank >= GetSize())
         {
            throw new ArgumentOutOfRangeException(nameof(rank));
         }

         return Select(_root, rank);
      }
   }

   /// <summary>
   ///    Returns all keys in the symbol table in ascending order as an <see cref="IEnumerable{T}" />
   ///    To iterate over all of the keys in the symbol table
   /// </summary>
   /// <value>All keys in the symbol table in ascending order</value>
   public IEnumerable<TKey> Keys =>
      IsEmpty()
         ? Enumerable.Empty<TKey>()
         : GetKeys(Min, Max);

   // is node x red; false if x is null ?
   private static bool IsRed(Node? node) =>
      node is { Color: Red };

   // number of node in subtree rooted at x; 0 if x is null
   private static int GetSize(Node? node) =>
      node?.Size ?? 0;

   /// <summary>
   ///    Returns the number of key-value pairs in this symbol table.
   /// </summary>
   /// <returns>the number of key-value pairs in this symbol table</returns>
   public int GetSize() => GetSize(_root);

   /// <summary>
   ///    Is this symbol table empty?
   /// </summary>
   /// <returns>true if this symbol table is empty, false - otherwise</returns>
   public bool IsEmpty() => _root == null;

   // value associated with the given key in subtree rooted at x; null if no such key
   private static TValue? Get(Node? node, TKey key)
   {
      while (node != null)
      {
         var cmpRes = key.CompareTo(node.Key);
         switch (cmpRes)
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

      return default;
   }

   /// <summary>
   ///    Does this symbol table contain the given key?
   /// </summary>
   /// <param name="key">Key</param>
   /// <returns>true if this symbol table contains <paramref name="key" /> and false otherwise</returns>
   public bool Contains(TKey key) => this[key] != null;

   // insert the key-value pair in the subtree rooted at h
   private static Node Put(Node? node, TKey key, TValue value)
   {
      if (node == null)
      {
         return new Node(key, value, Red, 1);
      }

      var cmpRes = key.CompareTo(node.Key);
      switch (cmpRes)
      {
         case < 0:
            node.Left = Put(node.Left, key, value);
            break;
         case > 0:
            node.Right = Put(node.Right, key, value);
            break;
         default:
            node.Value = value;
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

   /// <summary>
   ///    Removes the smallest key and associated value from the symbol table.
   /// </summary>
   /// <exception cref="InvalidOperationException">If the tree is empty</exception>
   public void DeleteMin()
   {
      if (IsEmpty())
      {
         throw new InvalidOperationException("BST underflow");
      }

      // if both children of root are black, set root to red
      if (_root != null && !IsRed(_root.Left) && !IsRed(_root.Right))
      {
         _root.Color = Red;
      }

      _root = DeleteMin(_root);
      if (!IsEmpty() && _root != null)
      {
         _root.Color = Black;
      }
   }

   // delete the key-value pair with the minimum key rooted at h
   private Node? DeleteMin(Node? node)
   {
      if (node?.Left == null)
      {
         return null;
      }

      if (!IsRed(node.Left) && !IsRed(node.Left.Left))
      {
         node = MoveRedLeft(node);
      }

      node.Left = DeleteMin(node.Left)
                  ?? throw new InvalidOperationException("node is null");

      return MakeBalanced(node);
   }

   /// <summary>
   ///    Removes the largest key and associated value from the symbol table.
   /// </summary>
   /// <exception cref="InvalidOperationException">If the tree is empty</exception>
   public void DeleteMax()
   {
      if (IsEmpty())
      {
         throw new InvalidOperationException("BST underflow");
      }

      // if both children of root are black, set root to red
      if (_root != null && !IsRed(_root.Left) && !IsRed(_root.Right))
      {
         _root.Color = Red;
      }

      _root = DeleteMax(_root);
      if (!IsEmpty() && _root != null)
      {
         _root.Color = Black;
      }
   }

   // delete the key-value pair with the maximum key rooted at h
   private static Node? DeleteMax(Node? node)
   {
      if (node != null && IsRed(node.Left))
      {
         node = RotateRight(node);
      }

      if (node?.Right == null)
      {
         return null;
      }

      if (!IsRed(node.Right) && !IsRed(node.Right.Left))
      {
         node = MoveRedRight(node);
      }

      node.Right = DeleteMax(node.Right)
                   ?? throw new InvalidOperationException("node is null");

      return MakeBalanced(node);
   }

   /// <summary>
   ///    Removes the specified key and its associated value from this symbol table
   /// </summary>
   /// <param name="key">Key</param>
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

      // if both children of root are black, set root to red
      if (_root != null && !IsRed(_root.Left) && !IsRed(_root.Right))
      {
         _root.Color = Red;
      }

      if (_root == null)
      {
         return;
      }

      _root = Delete(_root, key);
      if (!IsEmpty() && _root != null)
      {
         _root.Color = Black;
      }
   }

   // delete the key-value pair with the given key rooted at h
   private Node? Delete(Node node, TKey key)
   {
      if (key.CompareTo(node.Key) < 0)
      {
         if (!IsRed(node.Left) && !IsRed(node.Left?.Left))
         {
            node = MoveRedLeft(node);
         }

         if (node.Left != null)
         {
            node.Left = Delete(node.Left, key) ?? default!;
         }
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

         if (!IsRed(node.Right) && !IsRed(node.Right?.Left))
         {
            node = MoveRedRight(node);
         }

         if (key.CompareTo(node.Key) == 0)
         {
            var x = GetMin(node.Right);
            node.Key = x.Key;
            node.Value = x.Value;
            node.Right = DeleteMin(node.Right) ?? default!;
         }
         else
         {
            if (node.Right != null)
            {
               node.Right = Delete(node.Right, key) ?? default!;
            }
         }
      }

      return MakeBalanced(node);
   }

   // make a left-leaning link lean to the right
   private static Node RotateRight(Node node)
   {
      var leftNode = node.Left;
      Debug.Assert(leftNode != null, nameof(leftNode) + " != null");

      node.Left = leftNode.Right;
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
      Debug.Assert(rightNode != null, nameof(rightNode) + " != null");

      node.Right = rightNode.Left;
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
      if (node.Left != null)
      {
         node.Left.Color = !node.Left.Color;
      }

      if (node.Right != null)
      {
         node.Right.Color = !node.Right.Color;
      }
   }

   // Assuming that h is red and both h.left and h.left.left
   // are black, make h.left or one of its children red.
   private static Node MoveRedLeft(Node node)
   {
      FlipColors(node);
      if (IsRed(node.Right?.Left))
      {
         node.Right = RotateRight(node.Right
                                  ?? throw new InvalidOperationException("right node is null"));
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
      if (IsRed(node.Left?.Left))
      {
         node = RotateRight(node);
         FlipColors(node);
      }

      return node;
   }

   // restore red-black tree invariant
   private static Node MakeBalanced(Node node)
   {
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

   /// <summary>
   ///    Returns the height of the BST (for debugging).
   /// </summary>
   /// <returns>The height of the BST (a 1-node tree has height 0)</returns>
   public int GetHeight() => GetHeight(_root);

   private static int GetHeight(Node? node)
   {
      if (node == null)
      {
         return -1;
      }

      return 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
   }

   // the smallest key in subtree rooted at x; null if no such key
   private static Node GetMin(Node? node) =>
      node?.Left == null
         ? node ?? throw new ArgumentNullException(nameof(node))
         : GetMin(node.Left);

   // the largest key in the subtree rooted at x; null if no such key
   private Node GetMax(Node? node) =>
      node?.Right == null
         ? node ?? throw new ArgumentNullException(nameof(node))
         : GetMax(node.Right);

   /// <summary>
   ///    Returns the largest key in the symbol table less than or equal to <paramref name="key" />
   /// </summary>
   /// <param name="key">Key</param>
   /// <returns>the largest key in the symbol table less than or equal to <paramref name="key" /></returns>
   /// <exception cref="ArgumentNullException"><paramref name="key" /> is null</exception>
   /// <exception cref="InvalidOperationException">If symbol table is empty</exception>
   /// <exception cref="ArgumentException">Arg. for <paramref name="key" /> is too small</exception>
   public TKey GetFloor(TKey key)
   {
      if (key == null)
      {
         throw new ArgumentNullException(nameof(key));
      }

      if (IsEmpty())
      {
         throw new InvalidOperationException($"Calling {nameof(GetFloor)} for empty symbol table");
      }

      var node = GetFloor(_root, key);
      if (node == null)
      {
         throw new ArgumentException($"Arg for {key} is too small", nameof(key));
      }

      return node.Key;
   }

   // the largest key in the subtree rooted at x less than or equal to the given key
   private Node? GetFloor(Node? node, TKey key)
   {
      if (node == null)
      {
         return null;
      }

      var cmpRes = key.CompareTo(node.Key);
      switch (cmpRes)
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
   /// <returns>The smallest key in the symbol table greater than or equal to <paramref name="key" /></returns>
   /// <exception cref="ArgumentNullException"><paramref name="key" /> is null</exception>
   /// <exception cref="InvalidOperationException">If symbol table is empty</exception>
   /// <exception cref="ArgumentException">Arg. for <paramref name="key" /> is too large</exception>
   public TKey Ceiling(TKey key)
   {
      if (key == null)
      {
         throw new ArgumentNullException(nameof(key));
      }

      if (IsEmpty())
      {
         throw new InvalidOperationException($"Calling {nameof(Ceiling)} for empty symbol table");
      }

      var ceilingNode = Ceiling(_root, key);
      if (ceilingNode == null)
      {
         throw new ArgumentException($"Arg for {key} is too large", nameof(key));
      }

      return ceilingNode.Key;
   }

   // the smallest key in the subtree rooted at x greater than or equal to the given key
   private Node? Ceiling(Node? node, TKey key)
   {
      if (node == null)
      {
         return null;
      }

      var cmpRes = key.CompareTo(node.Key);
      switch (cmpRes)
      {
         case 0:
            return node;
         case > 0:
            return Ceiling(node.Right, key);
      }

      var ceilingNode = Ceiling(node.Left, key);
      return ceilingNode ?? node;
   }

   // Return key in BST rooted at x of given rank.
   // Precondition: rank is in legal range.
   private static TKey? Select(Node? node, int rank)
   {
      if (node == null)
      {
         return default;
      }

      var leftSize = GetSize(node.Left);
      return leftSize > rank
         ? Select(node.Left, rank)
         : leftSize < rank
            ? Select(node.Right, rank - leftSize - 1)
            : node.Key;
   }

   /// <summary>
   ///    Return the number of keys in the symbol table strictly less than <paramref name="key" />
   /// </summary>
   /// <param name="key">The key</param>
   /// <returns>The number of keys in the symbol table strictly less than <paramref name="key" /></returns>
   /// <exception cref="ArgumentNullException">Key is null</exception>
   public int GetRank(TKey key)
   {
      if (key == null)
      {
         throw new ArgumentNullException(nameof(key));
      }

      return GetRank(key, _root);
   }

   // number of keys less than key in the subtree rooted at x
   private static int GetRank(TKey key, Node? node)
   {
      if (node == null)
      {
         return 0;
      }

      var cmpRes = key.CompareTo(node.Key);
      return cmpRes switch
      {
         < 0 => GetRank(key, node.Left),
         > 0 => 1 + GetSize(node.Left) + GetRank(key, node.Right),
         _ => GetSize(node.Left)
      };
   }

   /// <summary>
   ///    Returns all keys in the symbol table in the given range in ascending order, as an <see cref="IEnumerable{T}" />
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

   // add the keys between lo and hi in the subtree rooted at x to the queue
   private static void GetKeys(Node? node, Queue<TKey> queue, TKey lowKey, TKey highKey)
   {
      if (node == null)
      {
         return;
      }

      var cmpLo = lowKey.CompareTo(node.Key);
      var cmpHi = highKey.CompareTo(node.Key);
      if (cmpLo < 0)
      {
         GetKeys(node.Left, queue, lowKey, highKey);
      }

      if (cmpLo <= 0 && cmpHi >= 0)
      {
         queue.Enqueue(node.Key);
      }

      if (cmpHi > 0)
      {
         GetKeys(node.Right, queue, lowKey, highKey);
      }
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
         throw new ArgumentNullException(nameof(lowKey));
      }

      if (highKey == null)
      {
         throw new ArgumentNullException(nameof(highKey));
      }

      if (lowKey.CompareTo(highKey) > 0)
      {
         return 0;
      }

      var calculatedRank = GetRank(highKey) - GetRank(lowKey);
      return Contains(highKey)
         ? calculatedRank + 1
         : calculatedRank;
   }

   /// <summary>
   ///    BST helper node data type
   /// </summary>
   private sealed class Node
   {
      public Node(TKey key, TValue value, bool color, int size)
      {
         Key = key;
         Value = value;
         Left = default;
         Right = default;
         Color = color;
         Size = size;
      }

      /// <summary>
      ///    Key
      /// </summary>
      public TKey Key { get; set; }

      /// <summary>
      ///    Value
      /// </summary>
      public TValue Value { get; set; }

      /// <summary>
      ///    Link to left node
      /// </summary>
      public Node? Left { get; set; }

      /// <summary>
      ///    Link to right node
      /// </summary>
      public Node? Right { get; set; }

      /// <summary>
      ///    Color
      /// </summary>
      public bool Color { get; set; }

      /// <summary>
      ///    Size
      /// </summary>
      public int Size { get; set; }
   }

   #region Check integrity of red-black tree data structure.

   public bool Check(out string errorMessage)
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
   private static bool IsBst(Node? node, TKey? min, TKey? max)
   {
      if (node == null)
      {
         return true;
      }

      if (min?.CompareTo(default) != 0 && node.Key.CompareTo(min) <= 0)
      {
         return false;
      }

      if (max?.CompareTo(default) != 0 && node.Key.CompareTo(max) >= 0)
      {
         return false;
      }

      return IsBst(node.Left, min, node.Key) && IsBst(node.Right, node.Key, max);
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

      return IsSizeConsistent(node.Left) && IsSizeConsistent(node.Right);
   }

   // check that ranks are consistent
   private bool IsRankConsistent()
   {
      for (var i = 0; i < GetSize(); i++)
      {
         var key = this[i];
         if (key != null && i != GetRank(key))
         {
            return false;
         }
      }

      return Keys.All(key => key.CompareTo(this[GetRank(key)]) == 0);
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

      return Is23(node.Left) && Is23(node.Right);
   }

   // do all paths from root to leaf have same number of black edges?
   private bool IsBalanced()
   {
      var black = 0; // number of black links on path from root to min
      var lRoot = _root;
      while (lRoot != null)
      {
         if (!IsRed(lRoot))
         {
            black++;
         }

         lRoot = lRoot.Left;
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

      return IsBalanced(node.Left, black) && IsBalanced(node.Right, black);
   }

   #endregion
}