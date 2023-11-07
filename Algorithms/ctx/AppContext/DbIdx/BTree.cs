using System.Diagnostics;

namespace AppContext.DbIdx;

/// <summary>
///    Based on BTree chapter in "Introduction to Algorithms", by Thomas Cormen, Charles Leiserson, Ronald Rivest.
///    This implementation is not thread-safe, and user must handle thread-safety.
/// </summary>
/// <typeparam name="TKey">Type of BTree Key.</typeparam>
/// <typeparam name="TPointer">Type of BTree Pointer associated with each Key.</typeparam>
public class BTree<TKey, TPointer>
   where TKey : IComparable<TKey>
{
   public BTree(int degree)
   {
      if (degree < 2)
      {
         throw new ArgumentException("BTree degree must be at least 2", nameof(degree));
      }

      Root = new BTreeNode<TKey, TPointer>(degree);
      Degree = degree;
      Height = 1;
   }

   public BTreeNode<TKey, TPointer> Root { get; private set; }

   public int Degree { get; }

   public int Height { get; private set; }

   /// <summary>
   ///    Searches a key in the BTree, returning the entry with it and with the pointer.
   /// </summary>
   /// <param name="key">Key being searched.</param>
   /// <returns>Entry for that key, null otherwise.</returns>
   public BTreeEntry<TKey, TPointer>? Search(TKey key) => SearchInternal(Root, key);

   /// <summary>
   ///    Inserts a new key associated with a pointer in the BTree. This
   ///    operation splits nodes as required to keep the BTree properties.
   /// </summary>
   /// <param name="newKey">Key to be inserted.</param>
   /// <param name="newPointer">Pointer to be associated with inserted key.</param>
   public void Insert(TKey newKey, TPointer newPointer)
   {
      // there is space in the root node
      if (!Root.HasReachedMaxEntries)
      {
         InsertNonFull(Root, newKey, newPointer);
         return;
      }

      // need to create new node and have it split
      var oldRoot = Root;
      Root = new BTreeNode<TKey, TPointer>(Degree);
      Root.Children.Add(oldRoot);
      SplitChild(Root, 0, oldRoot);
      InsertNonFull(Root, newKey, newPointer);

      Height++;
   }

   /// <summary>
   ///    Deletes a key from the BTree. This operations moves keys and nodes
   ///    as required to keep the BTree properties.
   /// </summary>
   /// <param name="keyToDelete">Key to be deleted.</param>
   public void Delete(TKey keyToDelete)
   {
      DeleteInternal(Root, keyToDelete);

      // if root's last entry was moved to a child node, remove it
      if (Root.Entries.Count == 0 && !Root.IsLeaf)
      {
         Root = Root.Children.Single();
         Height--;
      }
   }

   /// <summary>
   ///    Internal method to delete keys from the BTree
   /// </summary>
   /// <param name="bTree">Node to use to start search for the key.</param>
   /// <param name="keyToDelete">Key to be deleted.</param>
   private void DeleteInternal(BTreeNode<TKey, TPointer> bTree, TKey keyToDelete)
   {
      var i = bTree.Entries.TakeWhile(entry => keyToDelete.CompareTo(entry.Key) > 0).Count();

      // found key in node, so delete if from it
      if (i < bTree.Entries.Count && bTree.Entries[i].Key.CompareTo(keyToDelete) == 0)
      {
         DeleteKeyFromNode(bTree, keyToDelete, i);
         return;
      }

      // delete key from subtree
      if (!bTree.IsLeaf)
      {
         DeleteKeyFromSubtree(bTree, keyToDelete, i);
      }
   }

   /// <summary>
   ///    Helper method that deletes a key from a subtree.
   /// </summary>
   /// <param name="parentBTree">Parent node used to start search for the key.</param>
   /// <param name="keyToDelete">Key to be deleted.</param>
   /// <param name="subtreeIndexInNode">Index of subtree node in the parent node.</param>
   private void DeleteKeyFromSubtree(BTreeNode<TKey, TPointer> parentBTree, TKey keyToDelete, int subtreeIndexInNode)
   {
      var childBTree = parentBTree.Children[subtreeIndexInNode];

      // node has reached min # of entries, and removing any from it will break the btree property,
      // so this block makes sure that the "child" has at least "degree" # of nodes by moving an 
      // entry from a sibling node or merging nodes
      if (childBTree.HasReachedMinEntries)
      {
         var leftIndex = subtreeIndexInNode - 1;
         var leftSibling = subtreeIndexInNode > 0 ? parentBTree.Children[leftIndex] : null;

         var rightIndex = subtreeIndexInNode + 1;
         var rightSibling = subtreeIndexInNode < parentBTree.Children.Count - 1
            ? parentBTree.Children[rightIndex]
            : null;

         if (leftSibling != null && leftSibling.Entries.Count > Degree - 1)
         {
            // left sibling has a node to spare, so this moves one node from left sibling 
            // into parent's node and one node from parent into this current node ("child")
            childBTree.Entries.Insert(0, parentBTree.Entries[subtreeIndexInNode]);
            parentBTree.Entries[subtreeIndexInNode] = leftSibling.Entries.Last();
            leftSibling.Entries.RemoveAt(leftSibling.Entries.Count - 1);
            if (!leftSibling.IsLeaf)
            {
               childBTree.Children.Insert(0, leftSibling.Children.Last());
               leftSibling.Children.RemoveAt(leftSibling.Children.Count - 1);
            }
         }
         else if (rightSibling != null && rightSibling.Entries.Count > Degree - 1)
         {
            // right sibling has a node to spare, so this moves one node from right sibling 
            // into parent's node and one node from parent into this current node ("child")
            childBTree.Entries.Add(parentBTree.Entries[subtreeIndexInNode]);
            parentBTree.Entries[subtreeIndexInNode] = rightSibling.Entries.First();
            rightSibling.Entries.RemoveAt(0);
            if (!rightSibling.IsLeaf)
            {
               childBTree.Children.Add(rightSibling.Children.First());
               rightSibling.Children.RemoveAt(0);
            }
         }
         else
         {
            // this block merges either left or right sibling into the current node "child"
            if (leftSibling != null)
            {
               childBTree.Entries.Insert(0, parentBTree.Entries[subtreeIndexInNode]);
               var oldEntries = childBTree.Entries;
               childBTree.Entries = leftSibling.Entries;
               childBTree.Entries.AddRange(oldEntries);
               if (!leftSibling.IsLeaf)
               {
                  var oldChildren = childBTree.Children;
                  childBTree.Children = leftSibling.Children;
                  childBTree.Children.AddRange(oldChildren);
               }

               parentBTree.Children.RemoveAt(leftIndex);
               parentBTree.Entries.RemoveAt(subtreeIndexInNode);
            }
            else
            {
#if DEBUG
               Debug.Assert(rightSibling != null, "Node should have at least one sibling");
#endif
               childBTree.Entries.Add(parentBTree.Entries[subtreeIndexInNode]);
               childBTree.Entries.AddRange(rightSibling.Entries);
               if (!rightSibling.IsLeaf)
               {
                  childBTree.Children.AddRange(rightSibling.Children);
               }

               parentBTree.Children.RemoveAt(rightIndex);
               parentBTree.Entries.RemoveAt(subtreeIndexInNode);
            }
         }
      }

      // at this point, we know that "child" has at least "degree" nodes, so we can
      // move on - this guarantees that if any node needs to be removed from it to
      // guarantee BTree's property, we will be fine with that
      DeleteInternal(childBTree, keyToDelete);
   }

   /// <summary>
   ///    Helper method that deletes key from a node that contains it, be this
   ///    node a leaf node or an internal node.
   /// </summary>
   /// <param name="bTree">Node that contains the key.</param>
   /// <param name="keyToDelete">Key to be deleted.</param>
   /// <param name="keyIndexInNode">Index of key within the node.</param>
   private void DeleteKeyFromNode(BTreeNode<TKey, TPointer> bTree, TKey keyToDelete, int keyIndexInNode)
   {
      // if leaf, just remove it from the list of entries (we're guaranteed to have
      // at least "degree" # of entries, to BTree property is maintained
      if (bTree.IsLeaf)
      {
         bTree.Entries.RemoveAt(keyIndexInNode);
         return;
      }

      var predecessorChild = bTree.Children[keyIndexInNode];
      if (predecessorChild.Entries.Count >= Degree)
      {
         var predecessor = DeletePredecessor(predecessorChild);
         bTree.Entries[keyIndexInNode] = predecessor;
      }
      else
      {
         var successorChild = bTree.Children[keyIndexInNode + 1];
         if (successorChild.Entries.Count >= Degree)
         {
            var successor = DeleteSuccessor(predecessorChild);
            bTree.Entries[keyIndexInNode] = successor;
         }
         else
         {
            predecessorChild.Entries.Add(bTree.Entries[keyIndexInNode]);
            predecessorChild.Entries.AddRange(successorChild.Entries);
            predecessorChild.Children.AddRange(successorChild.Children);

            bTree.Entries.RemoveAt(keyIndexInNode);
            bTree.Children.RemoveAt(keyIndexInNode + 1);

            DeleteInternal(predecessorChild, keyToDelete);
         }
      }
   }

   /// <summary>
   ///    Helper method that deletes a predecessor key (i.e. rightmost key) for a given node.
   /// </summary>
   /// <param name="bTree">Node for which the predecessor will be deleted.</param>
   /// <returns>Predecessor entry that got deleted.</returns>
   private BTreeEntry<TKey, TPointer> DeletePredecessor(BTreeNode<TKey, TPointer> bTree)
   {
      if (bTree.IsLeaf)
      {
         var result = bTree.Entries[^1];
         bTree.Entries.RemoveAt(bTree.Entries.Count - 1);
         return result;
      }

      return DeletePredecessor(bTree.Children.Last());
   }

   /// <summary>
   ///    Helper method that deletes a successor key (i.e. leftmost key) for a given node.
   /// </summary>
   /// <param name="bTree">Node for which the successor will be deleted.</param>
   /// <returns>Successor entry that got deleted.</returns>
   private BTreeEntry<TKey, TPointer> DeleteSuccessor(BTreeNode<TKey, TPointer> bTree)
   {
      if (bTree.IsLeaf)
      {
         var result = bTree.Entries[0];
         bTree.Entries.RemoveAt(0);
         return result;
      }

      return DeletePredecessor(bTree.Children.First());
   }

   /// <summary>
   ///    Helper method that search for a key in a given BTree.
   /// </summary>
   /// <param name="bTree">Node used to start the search.</param>
   /// <param name="key">Key to be searched.</param>
   /// <returns>Entry object with key information if found, null otherwise.</returns>
   private BTreeEntry<TKey, TPointer>? SearchInternal(BTreeNode<TKey, TPointer> bTree, TKey key)
   {
      var i = bTree.Entries.TakeWhile(entry => key.CompareTo(entry.Key) > 0).Count();
      if (i < bTree.Entries.Count && bTree.Entries[i].Key.CompareTo(key) == 0)
      {
         return bTree.Entries[i];
      }

      return bTree.IsLeaf
         ? null
         : SearchInternal(bTree.Children[i], key);
   }

   /// <summary>
   ///    Helper method that splits a full node into two nodes.
   /// </summary>
   /// <param name="parentBTree">Parent node that contains node to be split.</param>
   /// <param name="nodeToBeSplitIndex">Index of the node to be split within parent.</param>
   /// <param name="bTreeToBeSplit">Node to be split.</param>
   private void SplitChild(BTreeNode<TKey, TPointer> parentBTree, int nodeToBeSplitIndex,
      BTreeNode<TKey, TPointer> bTreeToBeSplit)
   {
      var newNode = new BTreeNode<TKey, TPointer>(Degree);

      parentBTree.Entries.Insert(nodeToBeSplitIndex, bTreeToBeSplit.Entries[Degree - 1]);
      parentBTree.Children.Insert(nodeToBeSplitIndex + 1, newNode);

      newNode.Entries.AddRange(bTreeToBeSplit.Entries.GetRange(Degree, Degree - 1));

      // remove also Entries[this.Degree - 1], which is the one to move up to the parent
      bTreeToBeSplit.Entries.RemoveRange(Degree - 1, Degree);

      if (!bTreeToBeSplit.IsLeaf)
      {
         newNode.Children.AddRange(bTreeToBeSplit.Children.GetRange(Degree, Degree));
         bTreeToBeSplit.Children.RemoveRange(Degree, Degree);
      }
   }

   private void InsertNonFull(BTreeNode<TKey, TPointer> bTree, TKey newKey, TPointer newPointer)
   {
      var positionToInsert = bTree.Entries.TakeWhile(entry => newKey.CompareTo(entry.Key) >= 0).Count();

      // leaf node
      if (bTree.IsLeaf)
      {
         bTree.Entries.Insert(positionToInsert, new BTreeEntry<TKey, TPointer> { Key = newKey, Pointer = newPointer });
         return;
      }

      // non-leaf
      var child = bTree.Children[positionToInsert];
      if (child.HasReachedMaxEntries)
      {
         SplitChild(bTree, positionToInsert, child);
         if (newKey.CompareTo(bTree.Entries[positionToInsert].Key) > 0)
         {
            positionToInsert++;
         }
      }

      InsertNonFull(bTree.Children[positionToInsert], newKey, newPointer);
   }
}