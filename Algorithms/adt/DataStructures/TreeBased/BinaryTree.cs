using System.Collections;
using DataStructures.LinkBased;

namespace DataStructures.TreeBased;

public sealed class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
{
   private BinaryTreeNode<T>? _head;

   public int Count { get; private set; }

   public IEnumerator<T> GetEnumerator() => InOrderTraversal();

   IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

   public void Add(T value)
   {
      // Case 1: The tree is empty - allocate the head
      if (_head == null)
      {
         _head = new BinaryTreeNode<T>(value);
      }
      else
      {
         // Case 2: The tree is not empty so recursively
         // find the right location to insert
         AddTo(_head, value);
      }

      Count++;
   }

   // Recursive add algorithm
   private static void AddTo(BinaryTreeNode<T> node, T value)
   {
      // Case 1: Value is less than the current node value
      if (value.CompareTo(node.Value) < 0)
      {
         // if there is no left child make this the new left
         if (node.Left == null)
         {
            node.Left = new BinaryTreeNode<T>(value);
         }
         else
         {
            // else add it to the left node
            AddTo(node.Left, value);
         }
      }
      else
      {
         // Case 2: Value is equal to or greater than the current value
         // If there is no right, add it to the right
         if (node.Right == null)
         {
            node.Right = new BinaryTreeNode<T>(value);
         }
         else
         {
            // else add it to the right node
            AddTo(node.Right, value);
         }
      }
   }

   public bool Contains(T value) =>
      // defer to the node search helper function.
      FindWithParent(value, out _) != null;

   /// <summary>
   ///    Finds and returns the first node containing the specified value.  If the value
   ///    is not found, returns null.  Also returns the parent of the found node (or null)
   ///    which is used in Remove.
   /// </summary>
   private BinaryTreeNode<T>? FindWithParent(T value, out BinaryTreeNode<T>? parent)
   {
      // Now, try to find data in the tree
      var current = _head;
      parent = null;

      // while we don't have a match
      while (current != null)
      {
         var result = current.CompareTo(value);
         if (result > 0)
         {
            // if the value is less than current, go left.
            parent = current;
            current = current.Left;
         }
         else if (result < 0)
         {
            // if the value is more than current, go right.
            parent = current;
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

   public bool Remove(T value)
   {
      // Find the node to remove
      var current = FindWithParent(value, out var parent);
      if (current == null)
      {
         return false;
      }

      Count--;

      // Case 1: If current has no right child, then current's left replaces current
      if (current.Right == null)
      {
         if (parent == null)
         {
            _head = current.Left;
         }
         else
         {
            var result = parent.CompareTo(current.Value);
            switch (result)
            {
               case > 0:
                  // if parent value is greater than current value
                  // make the current left child a left child of parent
                  parent.Left = current.Left;
                  break;

               case < 0:
                  // if parent value is less than current value
                  // make the current left child a right child of parent
                  parent.Right = current.Left;
                  break;
            }
         }
      }
      else if (current.Right.Left == null)
      {
         // Case 2: If current's right child has no left child, then current's right child replaces current
         current.Right.Left = current.Left;

         if (parent == null)
         {
            _head = current.Right;
         }
         else
         {
            var result = parent.CompareTo(current.Value);
            switch (result)
            {
               case > 0:
                  // if parent value is greater than current value
                  // make the current right child a left child of parent
                  parent.Left = current.Right;
                  break;

               case < 0:
                  // if parent value is less than current value
                  // make the current right child a right child of parent
                  parent.Right = current.Right;
                  break;
            }
         }
      }
      else
      {
         // Case 3: If current's right child has a left child, replace current with current's
         // right child's left-most child find the right's left-most child
         var leftmost = current.Right.Left;
         var leftmostParent = current.Right;

         while (leftmost.Left != null)
         {
            leftmostParent = leftmost;
            leftmost = leftmost.Left;
         }

         // the parent's left subtree becomes the leftmost's right subtree
         leftmostParent.Left = leftmost.Right;

         // assign leftmost's left and right to current's left and right children
         leftmost.Left = current.Left;
         leftmost.Right = current.Right;

         if (parent == null)
         {
            _head = leftmost;
         }
         else
         {
            var result = parent.CompareTo(current.Value);
            switch (result)
            {
               case > 0:
                  // if parent value is greater than current value
                  // make leftmost the parent's left child
                  parent.Left = leftmost;
                  break;

               case < 0:
                  // if parent value is less than current value
                  // make leftmost the parent's right child
                  parent.Right = leftmost;
                  break;
            }
         }
      }

      return true;
   }

   public void PreOrderTraversal(Action<T> action)
   {
      PreOrderTraversal(action, _head);
   }

   private static void PreOrderTraversal(Action<T> action, BinaryTreeNode<T>? node)
   {
      if (node == null)
      {
         return;
      }

      action(node.Value);
      PreOrderTraversal(action, node.Left);
      PreOrderTraversal(action, node.Right);
   }

   public void PostOrderTraversal(Action<T> action) => PostOrderTraversal(action, _head);

   private static void PostOrderTraversal(Action<T> action, BinaryTreeNode<T>? node)
   {
      if (node == null)
      {
         return;
      }

      PostOrderTraversal(action, node.Left);
      PostOrderTraversal(action, node.Right);
      action(node.Value);
   }

   public void InOrderTraversal(Action<T> action) => InOrderTraversal(action, _head);

   private static void InOrderTraversal(Action<T> action, BinaryTreeNode<T>? node)
   {
      if (node == null)
      {
         return;
      }

      InOrderTraversal(action, node.Left);
      action(node.Value);
      InOrderTraversal(action, node.Right);
   }

   public IEnumerator<T> InOrderTraversal()
   {
      // This is a non-recursive algorithm using a stack to demonstrate removing
      // recursion.
      if (_head == null)
      {
         yield break;
      }

      // store the nodes we've skipped in this stack (avoids recursion)
      var stack = new Stack2<BinaryTreeNode<T>>();

      var current = _head;

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
            while (current?.Left != null)
            {
               stack.Push(current);
               current = current.Left;
            }
         }

         // in-order is left->yield->right
         if (current == null)
         {
            continue;
         }

         yield return current.Value;

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
      _head = null;
      Count = 0;
   }

   private sealed class BinaryTreeNode<TNode> : IComparable<TNode> where TNode : IComparable<TNode>
   {
      public BinaryTreeNode(TNode value) => Value = value;

      public BinaryTreeNode<TNode>? Left { get; set; }

      public BinaryTreeNode<TNode>? Right { get; set; }

      public TNode Value { get; }

      /// <summary>
      ///    Compares the current node to the provided value
      /// </summary>
      /// <param name="other">The node value to compare to</param>
      /// <returns>
      ///    1 if the instance value is greater than
      ///    the provided value, -1 if less or 0 if equal.
      /// </returns>
      public int CompareTo(TNode? other) => Value.CompareTo(other);
   }
}