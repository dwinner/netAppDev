namespace DataStructures.TreeBased;

public sealed class AvlTreeNode<TNode> : IComparable<TNode> where TNode : IComparable<TNode>
{
   private readonly AvlTree<TNode> _tree;
   private AvlTreeNode<TNode>? _left;
   private AvlTreeNode<TNode>? _right;

   public AvlTreeNode(TNode value, AvlTreeNode<TNode>? parent, AvlTree<TNode> tree)
   {
      Value = value;
      Parent = parent;
      _tree = tree;
   }

   public AvlTreeNode<TNode>? Left
   {
      get => _left;
      internal set
      {
         _left = value;
         if (_left != null)
         {
            _left.Parent = this;
         }
      }
   }

   public AvlTreeNode<TNode>? Right
   {
      get => _right;
      internal set
      {
         _right = value;
         if (_right != null)
         {
            _right.Parent = this;
         }
      }
   }

   public AvlTreeNode<TNode>? Parent { get; internal set; }

   public TNode Value { get; }

   private int LeftHeight => GetMaxChildHeight(Left);

   private int RightHeight => GetMaxChildHeight(Right);

   private TreeState State =>
      LeftHeight - RightHeight > 1
         ? TreeState.LeftHeavy
         : RightHeight - LeftHeight > 1
            ? TreeState.RightHeavy
            : TreeState.Balanced;

   private int BalanceFactor => RightHeight - LeftHeight;

   public int CompareTo(TNode? other) => Value.CompareTo(other);

   internal void Balance()
   {
      switch (State)
      {
         case TreeState.RightHeavy when Right is { BalanceFactor: < 0 }:
            LeftRightRotation();
            break;
         case TreeState.RightHeavy:
            LeftRotation();
            break;
         case TreeState.LeftHeavy when Left is { BalanceFactor: > 0 }:
            RightLeftRotation();
            break;
         case TreeState.LeftHeavy:
            RightRotation();
            break;
         case TreeState.Balanced:
            break;
         default:
            throw new ArgumentOutOfRangeException(nameof(State));
      }
   }

   private void LeftRotation()
   {
      //     a
      //      \
      //       b
      //        \
      //         c
      //
      // becomes
      //       b
      //      / \
      //     a   c

      var newRoot = Right;

      // replace the current root with the new root
      ReplaceRoot(newRoot!);

      // take ownership of right's left child as right (now parent)
      Right = newRoot!.Left;

      // the new root takes this as it's left
      newRoot.Left = this;
   }

   private void RightRotation()
   {
      //     c (this)
      //    /
      //   b
      //  /
      // a
      //
      // becomes
      //       b
      //      / \
      //     a   c

      var newRoot = Left;

      // replace the current root with the new root
      ReplaceRoot(newRoot!);

      // take ownership of left's right child as left (now parent)
      Left = newRoot!.Right;

      // the new root takes this as it's right
      newRoot.Right = this;
   }

   private void LeftRightRotation()
   {
      Right!.RightRotation();
      LeftRotation();
   }

   private void RightLeftRotation()
   {
      Left!.LeftRotation();
      RightRotation();
   }

   private void ReplaceRoot(AvlTreeNode<TNode> newRoot)
   {
      if (Parent != null)
      {
         if (Parent.Left == this)
         {
            Parent.Left = newRoot;
         }
         else if (Parent.Right == this)
         {
            Parent.Right = newRoot;
         }
      }
      else
      {
         _tree.Head = newRoot;
      }

      newRoot.Parent = Parent;
      Parent = newRoot;
   }

   // TOREFACTOR: unoptimized version, we could simply track the height of each node while adding/removing
   private static int GetMaxChildHeight(AvlTreeNode<TNode>? node) =>
      node != null
         ? 1 + Math.Max(GetMaxChildHeight(node.Left), GetMaxChildHeight(node.Right))
         : 0;

   private enum TreeState
   {
      Balanced,
      LeftHeavy,
      RightHeavy
   }
}