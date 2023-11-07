using System.Collections;
using System.Diagnostics;

namespace DataStructures.TreeBased;

public class BTree<T> : ICollection<T> where T : IComparable<T>
{
   private const int MinimumDegree = 2;
   private BTreeNode<T>? _root;

   public void Add(T value)
   {
      if (_root == null)
      {
         _root = new BTreeNode<T>(
            null, true, MinimumDegree, new[] { value }, Array.Empty<BTreeNode<T>>());
      }
      else
      {
         InsertNonFull(_root, value);
      }

      Count++;
   }

   public bool Remove(T value)
   {
      var removed = false;
      if (Count <= 0)
      {
         return removed;
      }

      removed = RemoveValue(_root, value);
      if (!removed)
      {
         return removed;
      }

      Count--;
      if (Count == 0)
      {
         _root = null;
      }
      else if (_root?.Values.Count == 0)
      {
         _root = _root.Children[0];
      }

      return removed;
   }

   public bool Contains(T value) => TryFindNodeContainingValue(value);

   public void Clear()
   {
      _root = null;
      Count = 0;
   }

   public void CopyTo(T[] array, int arrayIndex)
   {
      foreach (var value in InOrderEnumerator(_root
                                              ?? throw new InvalidOperationException(
                                                 $"{nameof(_root)} cannot be null")))
      {
         array[arrayIndex++] = value;
      }
   }

   public int Count { get; private set; }

   public bool IsReadOnly => false;

   public IEnumerator<T> GetEnumerator() =>
      _root == null
         ? Enumerable.Empty<T>().GetEnumerator()
         : InOrderEnumerator(_root).GetEnumerator();

   IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

   private static void InsertNonFull(BTreeNode<T> node, T value)
   {
      if (node.Leaf)
      {
         node.InsertKeyToLeafNode(value);
      }
      else
      {
         var index = node.Values.Count - 1;
         while (index >= 0 && value.CompareTo(node.Values[index]) < 0)
         {
            index--;
         }

         index++;
         InsertNonFull(node.Children[index], value);
      }
   }

   internal static bool RemoveValue(BTreeNode<T>? node, T value)
   {
      Debug.Assert(node != null, nameof(node) + " != null");

      if (node.Leaf)
      {
         // Deleting case 1...

         // By the time we are in a leaf node we have either pushed down
         // values such that the leaf node has minimum degree children
         // and can therefore have one node removed OR the root node is
         // also a leaf node we can freely violate the minimum rule.
         return node.DeleteKeyFromLeafNode(value);
      }

      if (TryGetIndexOf(node, value, out var valueIndex))
      {
         // Deletion case 2...

         // We have found the non-leaf node the value is in - since we can only delete values
         // from a leaf node we need to push the value to delete down into a child.

         // If the child that precedes the value to delete (the "left" child) has
         // at least the minimum degree of children ...
         if (node.Children[valueIndex].Values.Count >= node.Children[valueIndex].MinimumDegree)
         {
            //     [3       6         10]
            // [1 2]  [4 5]   [7 8 9]    [11 12]

            // deleting 10

            // find the largest value in the child node that contains smaller values
            // than what is being deleted... (this is the value 9)
            var valuePrime = FindPredecessor(node, valueIndex);

            // and REPLACE the value to delete with the next largest value (the one
            // we just found)  (swapping 9 and 10)
            node.ReplaceValue(valueIndex, valuePrime);

            // after the swap...

            //     [3       6         9]
            // [1 2]  [4 5]   [7 8 9]    [11 12]

            // notice that 9 is in the tree twice. This is not a typo. We are about 
            // to delete it from the child we took it from.

            // delete the value we moved up (9) from the child (this may in turn
            // push it down to subsequent children until it is in a leaf
            return RemoveValue(node.Children[valueIndex], valuePrime);

            // final tree...

            //     [3       6        9]
            // [1 2]  [4 5]   [7 8 ]   [11 12]
         }

         // if the left child did not have enough values to move one of its values up,
         // check if the right child does
         if (node.Children[valueIndex + 1].Values.Count >= node.Children[valueIndex + 1].MinimumDegree)
         {
            // see the above algorithm and do the opposite...

            //     [3       6         10]
            // [1 2]  [4 5]   [7 8 9]    [11 12]

            // deleting 6

            // successor = 7
            var valuePrime = FindSuccessor(node, valueIndex);
            node.ReplaceValue(valueIndex, valuePrime);

            // after replace the tree is

            //     [3       7         10]
            // [1 2]  [4 5]   [7 8 9]    [11 12]

            // now remove 7 from the child
            return RemoveValue(node.Children[valueIndex + 1], valuePrime);

            // final tree...
            //     [3       7         10]
            // [1 2]  [4 5]   [8 9]    [11 12]
         }
         // If neither child has the minimum degree of children that means they both
         // have (minimum degree - 1) children. Since a node can have 
         // (2 * <minimum degree> - 1) children we can safely merge the two nodes
         // into a single child.

         //
         //     [3     6     9]
         // [1 2] [4 5] [7 8] [10 11]
         //
         // deleting 6
         // 
         // [4 5] and [7 8] are merged into a single node with [6] pushed down into it
         //
         //     [3          9]
         // [1 2] [4 5 6 7 8] [10 11]
         //
         var newChildNode = node.PushDown(valueIndex);

         // now that we've pushed the value down a level we can call remove
         // on the new child node [4 5 6 7 8]
         return RemoveValue(newChildNode, value);
      }
      // Deletion case 3...

      // We are at an internal node which does not contain the value we want to delete.
      // First find the child path that the value we want to delete would be in
      // if it does existing in the tree...
      FindPotentialPath(node, value, out valueIndex, out var childIndex);

      // now that we know where the value should be we need to ensure that the node
      // we are going to has the minimum number of values necessary to delete from.
      if (node.Children[childIndex].Values.Count == node.Children[childIndex].MinimumDegree - 1)
      {
         // since the node does not have enough values, what we want to do is borrow
         // a value from a sibling that has enough values to share.

         // determine if the left or right sibling has the most children
         var indexOfMaxSibling = GetIndexOfMaxSibling(childIndex, node);

         // if a sibling with values exists (maybe we're 
         // the root node and don't have one)
         // and that sibling has enough values...
         if (indexOfMaxSibling >= 0 &&
             node.Children[indexOfMaxSibling].Values.Count >= node.Children[indexOfMaxSibling].MinimumDegree)
         {
            // rotate the appropriate value from the sibling 
            // through the parent into the current node
            // so that we have enough values in the current 
            // node to push a value down into the 
            // child we are going to check next.

            //     [3      7]
            // [1 2] [4 5 6]  [8 9]
            //
            // the node we want to travel through is [1 2] but we 
            // need another node in it. So we rotate the 4 
            // up to the root and push the 3 down into the [1 2] 
            // node.
            //
            //       [4     7]
            // [1 2 3] [5 6]  [7 8]
            RotateAndPushDown(node, childIndex, indexOfMaxSibling);
         }
         else
         {
            // merge (which may push the only node in the root down - so new root)
            var pushedDownNode = node.PushDown(valueIndex);

            // now find the node we just pushed down
            childIndex = 0;
            while (pushedDownNode != node.Children[childIndex])
            {
               childIndex++;
            }
         }
      }

      return RemoveValue(node.Children[childIndex], value);
   }

   internal static void RotateAndPushDown(BTreeNode<T>? node, int childIndex, int indexOfMaxSibling)
   {
      Debug.Assert(node != null, nameof(node) + " != null");

      var valueIndex = childIndex < indexOfMaxSibling
         ? childIndex
         : childIndex - 1;

      if (indexOfMaxSibling > childIndex)
      {
         // we are moving the left-most key from the right sibling into the parent
         // and pushing the parent down into the child 
         //
         //     [6      10]
         //  [1]  [7 8 9] [11]
         //
         // deleting something less than 6
         // 
         //       [7   10]
         //    [1 6] [8 9] [11]

         // grab the 7
         var valueToMoveToX = node.Children[indexOfMaxSibling].Values.First();

         // get 7's left child if it has one (not a leaf)
         var childToMoveToNode = node.Children[indexOfMaxSibling].Leaf
            ? null
            : node.Children[indexOfMaxSibling].Children.First();

         // record the 6 (the push down value)
         var valueToMoveDown = node.Values[valueIndex];

         // move the 7 into the parent
         node.ReplaceValue(valueIndex, valueToMoveToX);

         // move the 6 into the child
         node.Children[childIndex].AddEnd(valueToMoveDown, childToMoveToNode);

         // remove the first value and child from the sibling now that they've been moved
         node.Children[indexOfMaxSibling].RemoveFirst();
      }
      else
      {
         // we are moving the right-most key from the left sibling into the parent
         // and pushing the parent down into the child 
         //
         //     [6      10]
         //  [1]  [7 8 9] [11]
         //
         // deleting something greater than 10
         // 
         //     [6     9]
         //  [1]  [7 8] [10, 11]

         // grab the 9
         var valueToMoveToX = node.Children[indexOfMaxSibling].Values.Last();

         // get 9's right child if it has one (not a leaf node) 
         var childToMoveToNode = node.Children[indexOfMaxSibling].Leaf
            ? null
            : node.Children[indexOfMaxSibling].Children.Last();

         // record the 10 (the push down value)
         var valueToMoveDown = node.Values[valueIndex];

         // move the 9 into the parent
         node.ReplaceValue(valueIndex, valueToMoveToX);

         // move the 10 into the child
         node.Children[childIndex].AddFront(valueToMoveDown, childToMoveToNode);

         // remove the last value and child from the sibling now that they've been moved
         node.Children[indexOfMaxSibling].RemoveLast();
      }
   }

   internal static void FindPotentialPath(BTreeNode<T>? node, T value, out int valueIndex, out int childIndex)
   {
      Debug.Assert(node != null, nameof(node) + " != null");

      // We want to find out which child the value we are searching for (value)
      // would be in if the value were in the tree.
      childIndex = node.Children.Count - 1;
      valueIndex = node.Values.Count - 1;

      // start at the right-most child and value indexes and work
      // backwards until we are less than the value we want.
      while (valueIndex > 0)
      {
         var compare = value.CompareTo(node.Values[valueIndex]);

         if (compare > 0)
         {
            break;
         }

         childIndex--;
         valueIndex--;
      }

      // if we make it all the way to the last value...
      if (valueIndex == 0)
      {
         // if the value we are searching for is less than the first 
         // value in the node then the child is the 0 index child, 
         // not the 1 index.
         if (value.CompareTo(node.Values[valueIndex]) < 0)
         {
            childIndex--;
         }
      }
   }

   // Returns the index (to the left or right) of the child node 
   // that has the most values in it.
   //
   // Example
   //
   //     [3      7]
   // [1 2] [4 5 6] [8 9]
   //
   // If we pass in the [3 7] node with index 0, the left child [1 2]
   // and right child [4 5 6] would be checked and the index 1, for child
   // node [4 5 6] would be returned.
   // 
   // If we checked [3 7] with index 1, the left child [4 5 6] and the
   // right child [8 9] would be checked and the value 1 would be returned
   private static int GetIndexOfMaxSibling(int index, BTreeNode<T>? node)
   {
      Debug.Assert(node != null, nameof(node) + " != null");

      var indexOfMaxSibling = -1;
      BTreeNode<T>? leftSibling = null;
      if (index > 0)
      {
         leftSibling = node.Children[index - 1];
      }

      BTreeNode<T>? rightSibling = null;
      if (index + 1 < node.Children.Count)
      {
         rightSibling = node.Children[index + 1];
      }

      if (leftSibling != null || rightSibling != null)
      {
         indexOfMaxSibling = leftSibling != null && rightSibling != null
            ? leftSibling.Values.Count > rightSibling.Values.Count
               ? index - 1
               : index + 1
            : leftSibling != null
               ? index - 1
               : index + 1;
      }

      return indexOfMaxSibling;
   }

   // Gets the index of the specified value from the current node's values
   // returning true if the value was found, false otherwise.
   private static bool TryGetIndexOf(BTreeNode<T>? node, T value, out int valueIndex)
   {
      Debug.Assert(node != null, nameof(node) + " != null");

      for (var index = 0; index < node.Values.Count; index++)
      {
         if (value.CompareTo(node.Values[index]) == 0)
         {
            valueIndex = index;
            return true;
         }
      }

      valueIndex = -1;
      return false;
   }

   // Finds the value of the predecessor value of a specific value in a node
   //
   // Example
   //
   //     [3     6]
   // [1 2] [4 5] [7 8]
   //
   // the predecessor of 3 is 2.
   private static T FindPredecessor(BTreeNode<T>? node, int index)
   {
      Debug.Assert(node != null, nameof(node) + " != null");

      node = node.Children[index];
      while (!node.Leaf)
      {
         node = node.Children.Last();
      }

      return node.Values.Last();
   }

   // Finds the value of the successor of a specific value in a node
   //
   // Example
   //
   //     [3     6]
   // [1 2] [4 5] [7 8]
   //
   // the successor of 3 is 4.
   private static T FindSuccessor(BTreeNode<T>? node, int index)
   {
      Debug.Assert(node != null, nameof(node) + " != null");

      node = node.Children[index + 1];
      while (!node.Leaf)
      {
         node = node.Children.First();
      }

      return node.Values.First();
   }

   // searches the node, and its children, looking for the specified value.
   internal bool TryFindNodeContainingValue(T value)
   {
      var current = _root;

      // if the current node is null then we never found the value
      // otherwise we still have hope.
      while (current != null)
      {
         var index = 0;
         // check each value in the node
         while (index < current.Values.Count)
         {
            var compare = value.CompareTo(current.Values[index]);

            // did we find it?
            if (compare == 0)
            {
               // awesome!
               return true;
            }

            // if the value to find is less than the current node's value
            // then we want to go left (which is where we are ...)
            if (compare < 0)
            {
               break;
            }

            // otherwise move on to the next value in the node
            index++;
         }

         if (current.Leaf)
         {
            // if we are at a leaf node there is no child to go
            // down to
            break;
         }

         // otherwise go into the child we determined must contain the 
         // value we want to find
         current = current.Children[index];
      }

      return false;
   }

   private static IEnumerable<T> InOrderEnumerator(BTreeNode<T> node)
   {
      if (node.Leaf)
      {
         foreach (var value in node.Values)
         {
            yield return value;
         }
      }
      else
      {
         using var children = node.Children.GetEnumerator();
         using var values = node.Values.GetEnumerator();

         while (children.MoveNext())
         {
            foreach (var childValue in InOrderEnumerator(children.Current))
            {
               yield return childValue;
            }

            if (values.MoveNext())
            {
               yield return values.Current;
            }
         }
      }
   }
}