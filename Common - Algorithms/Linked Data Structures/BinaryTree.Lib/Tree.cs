using System;

namespace BinaryTree.Lib
{
   public class Tree<TTreeItem>
      where TTreeItem : IComparable<TTreeItem>
   {
      private TreeNode<TTreeItem> _root;

      public void InsertNode(TTreeItem treeItem)
      {
         if (_root == default(TreeNode<TTreeItem>))
            _root = new TreeNode<TTreeItem>(treeItem);
         else
            _root.Insert(treeItem);
      }

      public sealed class TreeVisitor<TItem>
         where TItem : IComparable<TItem>
      {
         private readonly Tree<TItem> _tree;
         private readonly VisitMode _visitMode;

         public TreeVisitor(Tree<TItem> tree, VisitMode visitMode)
         {
            _tree = tree;
            _visitMode = visitMode;
         }

         public void Visit(Action<TreeNode<TItem>> walkAction)
         {
            switch (_visitMode)
            {
               case VisitMode.PreOrder:
                  _tree._root.PreOrderHelper(walkAction);
                  break;
               case VisitMode.InOrder:
                  _tree._root.InOrderHelper(walkAction);
                  break;
               case VisitMode.PostOrder:
                  _tree._root.PostOrderHelper(walkAction);
                  break;
               default:
                  goto case VisitMode.PreOrder;
            }
         }
      }
   }
}