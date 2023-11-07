using System.Collections;
using DataStructures.LinkBased;

namespace DataStructures.TreeBased;

public sealed class AvlTree<T> : IEnumerable<T> where T : IComparable<T>
{
   public AvlTreeNode<T>? Head { get; internal set; }

   public int Count { get; private set; }

   /// <summary>
   ///    Returns an enumerator that performs an in-order traversal of the binary tree
   /// </summary>
   /// <returns>The in-order enumerator</returns>
   public IEnumerator<T> GetEnumerator() => InOrderTraversal();

   IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

   public void Add(T value)
   {
      if (Head == null)
      {
         // Case 1: The tree is empty - allocate the head
         Head = new AvlTreeNode<T>(value, null, this);
      }
      else
      {
         // Case 2: The tree is not empty so find the right location to insert
         AddTo(Head, value);
      }

      Count++;
   }

   // Recursive add algorithm
   private void AddTo(AvlTreeNode<T> node, T value)
   {
      // Case 1: Value is less than the current node value
      if (value.CompareTo(node.Value) < 0)
      {
         // if there is no left child make this the new left
         if (node.Left == null)
         {
            node.Left = new AvlTreeNode<T>(value, node, this);
         }
         else
         {
            // else add it to the left node
            AddTo(node.Left, value);
         }
      }
      // Case 2: Value is equal to or greater than the current value
      else
      {
         // If there is no right, add it to the right
         if (node.Right == null)
         {
            node.Right = new AvlTreeNode<T>(value, node, this);
         }
         else
         {
            // else add it to the right node
            AddTo(node.Right, value);
         }
      }

      node.Balance();
   }

   public bool Contains(T value) => Find(value) != null;

   /// <summary>
   ///    Finds and returns the first node containing the specified value. If the value
   ///    is not found, returns null. Also returns the parent of the found node (or null)
   ///    which is used in Remove.
   /// </summary>
   /// <param name="value">The value to search for</param>
   /// <returns>The found node (or null)</returns>
   private AvlTreeNode<T>? Find(T value)
   {
      // Now, try to find data in the tree
      var current = Head;

      // while we don't have a match
      while (current != null)
      {
         var result = current.CompareTo(value);
         if (result > 0)
         {
            // if the value is less than current, go left.
            current = current.Left;
         }
         else if (result < 0)
         {
            // if the value is more than current, go right.
            current = current.Right;
         }
         else
         {
            // we have a match!
            break;
         }
      }

      return current;
   }

   /// <summary>
   ///    Removes the first occurrence of the specified value from the tree.
   /// </summary>
   /// <param name="value">The value to remove</param>
   /// <returns>True if the value was removed, false otherwise</returns>
   public bool Remove(T value)
   {
      var current = Find(value);
      if (current == null)
      {
         return false;
      }

      var treeToBalance = current.Parent;
      Count--;

      switch (current.Right)
      {
         // Case 1: If current has no right child, then current's left replaces current
         case null when current.Parent == null:
         {
            Head = current.Left;
            if (Head != null)
            {
               Head.Parent = null;
            }

            break;
         }

         case null:
         {
            var result = current.Parent.CompareTo(current.Value);
            if (result > 0)
            {
               // if parent value is greater than current value
               // make the current left child a left child of parent
               current.Parent.Left = current.Left;
            }
            else if (result < 0)
            {
               // if parent value is less than current value
               // make the current left child a right child of parent
               current.Parent.Right = current.Left;
            }

            break;
         }

         // Case 2: If current's right child has no left child, then current's right child
         //         replaces current
         default:
         {
            if (current.Right.Left == null)
            {
               current.Right.Left = current.Left;
               if (current.Parent == null)
               {
                  Head = current.Right;
                  if (Head != null)
                  {
                     Head.Parent = null;
                  }
               }
               else
               {
                  var result = current.Parent.CompareTo(current.Value);
                  if (result > 0)
                  {
                     // if parent value is greater than current value
                     // make the current right child a left child of parent
                     current.Parent.Left = current.Right;
                  }
                  else if (result < 0)
                  {
                     // if parent value is less than current value
                     // make the current right child a right child of parent
                     current.Parent.Right = current.Right;
                  }
               }
            }
            // Case 3: If current's right child has a left child, replace current with current's
            //         right child's left-most child
            else
            {
               // find the right's left-most child
               var leftmost = current.Right.Left;
               while (leftmost.Left != null)
               {
                  leftmost = leftmost.Left;
               }

               // the parent's left subtree becomes the leftmost's right subtree
               leftmost.Parent!.Left = leftmost.Right;

               // assign leftmost's left and right to current's left and right children
               leftmost.Left = current.Left;
               leftmost.Right = current.Right;
               if (current.Parent == null)
               {
                  Head = leftmost;
                  if (Head != null)
                  {
                     Head.Parent = null;
                  }
               }
               else
               {
                  var result = current.Parent.CompareTo(current.Value);
                  if (result > 0)
                  {
                     // if parent value is greater than current value
                     // make leftmost the parent's left child
                     current.Parent.Left = leftmost;
                  }
                  else if (result < 0)
                  {
                     // if parent value is less than current value
                     // make leftmost the parent's right child
                     current.Parent.Right = leftmost;
                  }
               }
            }

            break;
         }
      }

      if (treeToBalance != null)
      {
         treeToBalance.Balance();
      }
      else
      {
         Head?.Balance();
      }

      return true;
   }

   /// <summary>
   ///    Enumerates the values contains in the binary tree in in-order traversal order.
   /// </summary>
   /// <returns>The enumerator</returns>
   private IEnumerator<T> InOrderTraversal()
   {
      // This is a non-recursive algorithm using a stack to demonstrate removing
      // recursion to make using the yield syntax easier.
      if (Head == null)
      {
         yield break;
      }

      // store the nodes we've skipped in this stack (avoids recursion)
      var stack = new Stack2<AvlTreeNode<T>>();
      var current = Head;

      // when removing recursion we need to keep track of whether or not
      // we should be going to the left node or the right nodes next.
      var goLeftNext = true;

      // start by pushing Head onto the stack
      stack.Push(current);
      while (stack.Count > 0)
      {
         // If we're heading left...
         if (goLeftNext)
         {
            // push everything but the left-most node to the stack
            // we'll yield the left-most after this block
            while (current!.Left != null)
            {
               stack.Push(current);
               current = current.Left;
            }
         }

         // in-order is left->yield->right
         yield return current!.Value;

         // if we can go right then do so
         if (current.Right != null)
         {
            current = current.Right;

            // once we've gone right once, we need to start
            // going left again.
            goLeftNext = true;
         }
         else
         {
            // if we can't go right then we need to pop off the parent node
            // so we can process it and then go to it's right node
            current = stack.Pop();
            goLeftNext = false;
         }
      }
   }

   public void Clear()
   {
      Head = null;
      Count = 0;
   }
}