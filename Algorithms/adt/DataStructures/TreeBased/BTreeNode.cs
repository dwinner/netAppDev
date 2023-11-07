using System.Diagnostics;

namespace DataStructures.TreeBased;

internal class BTreeNode<T> where T : IComparable<T>
{
   private readonly List<BTreeNode<T>> _children;
   private readonly List<T> _values;

   internal BTreeNode(BTreeNode<T>? parent, bool leaf, int minimumDegree, T[] values, BTreeNode<T>[] children)
   {
      ValidatePotentialState(parent, leaf, minimumDegree, values, children);

      Parent = parent;
      Leaf = leaf;
      MinimumDegree = minimumDegree;

      _values = new List<T>(values);
      _children = new List<BTreeNode<T>>(children);
   }

   /// <summary>
   ///    True if the node is a leaf node, false otherwise.
   /// </summary>
   internal bool Leaf { get; }

   /// <summary>
   ///    The node's values
   /// </summary>
   internal IList<T> Values => _values;

   /// <summary>
   ///    The node's children
   /// </summary>
   internal IList<BTreeNode<T>> Children => _children;

   /// <summary>
   ///    The minimum degree of the node is the minimum degree of the tree.
   ///    If the minimum degree is T then the node must have at least (T-1)
   ///    values but no more than (2*T-1).
   /// </summary>
   internal int MinimumDegree { get; }

   /// <summary>
   ///    The parent of the current node (or null if the root node)
   /// </summary>
   internal BTreeNode<T>? Parent { get; }

   // Validates the ctor parameters 
   private static void ValidatePotentialState(
      BTreeNode<T>? parent, bool leaf, int minimumDegree, T[] values, BTreeNode<T>[] children)
   {
      var root = parent == null;
      if (values == null)
      {
         throw new ArgumentNullException(nameof(values));
      }

      if (children == null)
      {
         throw new ArgumentNullException(nameof(children));
      }

      if (minimumDegree < 2)
      {
         throw new ArgumentOutOfRangeException(
            nameof(minimumDegree),
            "The minimum degree must be greater than or equal to 2");
      }

      if (values.Length == 0)
      {
         if (children.Length != 0)
         {
            throw new ArgumentException("An empty node cannot have children");
         }
      }
      else
      {
         if (values.Length > 2 * minimumDegree - 1)
         {
            throw new ArgumentException("There are too many values");
         }

         if (!root)
         {
            if (values.Length < minimumDegree - 1)
            {
               throw new ArgumentException("Each non-root node must have at least degree - 1 children");
            }
         }

         if (!leaf && !root)
         {
            if (values.Length + 1 != children.Length)
            {
               throw new ArgumentException("There should be one more child than values");
            }
         }
      }
   }

   [Conditional("DEBUG")]
   private void ValidateValues()
   {
      if (_values.Count > 1)
      {
         for (var i = 1; i < _values.Count; i++)
         {
            Debug.Assert(_values[i - 1].CompareTo(_values[i]) < 0);
         }
      }
   }

   /// <summary>
   ///    Splits a full child node, pulling the split value into the current node
   /// </summary>
   /// <param name="indexOfChildToSplit">The child to split</param>
   internal void SplitFullChild(int indexOfChildToSplit)
   {
      // Splits a child node by pulling the middle node up from it
      // into the current (parent) node.

      //     [3          9]
      // [1 2] [4 5 6 7 8] [10 11] 
      //
      // splitting [4 5 6 7 8] would pull 6 up to its parent
      //
      //     [3     6     9]
      // [1 2] [4 5] [7 8] [10 11] 
      var medianIndex = Children[indexOfChildToSplit].Values.Count / 2;

      var isChildLeaf = Children[indexOfChildToSplit].Leaf;

      // get the value 6
      var valueToPullUp = Children[indexOfChildToSplit].Values[medianIndex];

      // build node [4 5]
      var newLeftSide = new BTreeNode<T>(this, isChildLeaf, MinimumDegree,
         Children[indexOfChildToSplit].Values.Take(medianIndex).ToArray(),
         Children[indexOfChildToSplit].Children.Take(medianIndex + 1).ToArray());

      // build node [7 8]
      var newRightSide = new BTreeNode<T>(this, isChildLeaf, MinimumDegree,
         Children[indexOfChildToSplit].Values.Skip(medianIndex + 1).ToArray(),
         Children[indexOfChildToSplit].Children.Skip(medianIndex + 1).ToArray());

      // add 6 to [3 9] making [3 6 9]
      _values.Insert(indexOfChildToSplit, valueToPullUp);

      // sanity check
      ValidateValues();

      // remove the child that pointed to the old node [4 5 6 7 8]
      _children.RemoveAt(indexOfChildToSplit);

      // add the child pointing to [4 5] and [7 8]
      _children.InsertRange(indexOfChildToSplit, new[] { newLeftSide, newRightSide });
   }

   /// <summary>
   ///    Splits the full root node into a new root and two children
   /// </summary>
   /// <returns>The new root node</returns>
   internal BTreeNode<T> SplitFullRootNode()
   {
      // The root of the tree, and in fact the entire tree, is
      //
      // [1 2 3 4 5]
      //
      // So pull out 3 and split the left and right side
      //
      //     [3]
      // [1 2] [4 5]

      // find the index of the value to pull up: 3
      var medianIndex = Values.Count / 2;

      // now get the 3
      var rootValue = Values[medianIndex];

      // Build the new root node (empty)
      var result = new BTreeNode<T>(
         Parent, false, MinimumDegree, Array.Empty<T>(), Array.Empty<BTreeNode<T>>());

      // build the left noed [1 2]
      var newLeftSide = new BTreeNode<T>(result, Leaf, MinimumDegree,
         Values.Take(medianIndex).ToArray(),
         Children.Take(medianIndex + 1).ToArray());

      // build the right node [4 5]
      var newRightSide = new BTreeNode<T>(result, Leaf, MinimumDegree,
         Values.Skip(medianIndex + 1).ToArray(),
         Children.Skip(medianIndex + 1).ToArray());

      // add the 3 to the root node
      result._values.Add(rootValue);

      // Add the left child [1 2]
      result._children.Add(newLeftSide);

      // Add the right child [4 5]
      result._children.Add(newRightSide);

      return result;
   }

   /// <summary>
   ///    Insert the specified value into the non-Full leaf node
   /// </summary>
   /// <param name="value">The value to insert</param>
   internal void InsertKeyToLeafNode(T value)
   {
      // Leaf validation is done by caller
      if (!Leaf)
      {
         throw new InvalidOperationException("Unable to insert into a non-leaf node");
      }

      // Find the index to insert at
      var index = 0;
      while (index < Values.Count && value.CompareTo(Values[index]) > 0)
      {
         index++;
      }

      // Insert
      _values.Insert(index, value);

      // Sanity check
      ValidateValues();
   }

   /// <summary>
   ///    If it exists, removes the specified value from the leaf node.
   /// </summary>
   /// <param name="value">The value to remove</param>
   /// <returns>True if a value was removed, false otherwise</returns>
   internal bool DeleteKeyFromLeafNode(T value)
   {
      if (!Leaf)
      {
         throw new InvalidOperationException("Unable to leaf-delete from a non-leaf node");
      }

      return _values.Remove(value);
   }

   /// <summary>
   ///    Replaces the value at the specified index with the new value
   /// </summary>
   /// <param name="valueIndex">The index of the value to replace</param>
   /// <param name="newValue">The new value</param>
   internal void ReplaceValue(int valueIndex, T newValue)
   {
      _values[valueIndex] = newValue;
      ValidateValues();
   }

   //     [3     6]
   // [1 2] [4 5] [7 8]
   // becomes
   //           [6]
   // [1 2 3 4 5] [7 8]
   internal BTreeNode<T> PushDown(int valueIndex)
   {
      var values = new List<T>();
      // [1 2] -> [1 2]
      values.AddRange(Children[valueIndex].Values);
      // [3]   -> [1 2 3]
      values.Add(Values[valueIndex]);
      // [4 5] -> [1 2 3 4 5]
      values.AddRange(Children[valueIndex + 1].Values);

      var children = new List<BTreeNode<T>>();
      children.AddRange(Children[valueIndex].Children);
      children.AddRange(Children[valueIndex + 1].Children);

      var newNode = new BTreeNode<T>(this,
         Children[valueIndex].Leaf,
         MinimumDegree,
         values.ToArray(),
         children.ToArray());

      // [3 6] -> [6]
      _values.RemoveAt(valueIndex);
      // [c1 c2 c3] -> [c2 c3]
      _children.RemoveAt(valueIndex);
      // [c2 c3] -> [newNode c3]
      _children[valueIndex] = newNode;

      return newNode;
   }

   /// <summary>
   ///    Adds the specified value to the node and, if the specified node is non-null,
   ///    adds the node to the children
   /// </summary>
   internal void AddEnd(T valueToPushDown, BTreeNode<T>? bTreeNode)
   {
      _values.Add(valueToPushDown);
      ValidateValues();
      if (bTreeNode != null)
      {
         _children.Add(bTreeNode);
      }
   }

   /// <summary>
   ///    Removes the first value and child (if applicable)
   /// </summary>
   internal void RemoveFirst()
   {
      _values.RemoveAt(0);
      if (!Leaf)
      {
         _children.RemoveAt(0);
      }
   }

   /// <summary>
   ///    Removes the last value and child (if applicable)
   /// </summary>
   internal void RemoveLast()
   {
      _values.RemoveAt(_values.Count - 1);
      if (!Leaf)
      {
         _children.RemoveAt(_children.Count - 1);
      }
   }

   /// <summary>
   ///    Adds the specified value to the front of the values and adds, if non-null,
   ///    the specified value to the children
   /// </summary>
   internal void AddFront(T newValue, BTreeNode<T>? bTreeNode)
   {
      _values.Insert(0, newValue);
      ValidateValues();
      if (bTreeNode != null)
      {
         _children.Insert(0, bTreeNode);
      }
   }
}