using System;

namespace BinaryTree.Lib
{
   public static class TreeNodeExtensions
   {
      /// <summary>
      ///    Insert tree item into the tree node, that contains nodes
      /// </summary>
      /// <remarks>Ignore duplicated values</remarks>
      /// <typeparam name="TTreeItem">Tree item type</typeparam>
      /// <param name="this">Extension parameter</param>
      /// <param name="treeItem">Tree item to insert</param>
      public static void Insert<TTreeItem>(this TreeNode<TTreeItem> @this, TTreeItem treeItem)
         where TTreeItem : IComparable<TTreeItem>
      {
         while (true)
         {
            if (treeItem.CompareTo(@this.Data) < 0) // Insert in left subtree
               if (@this.LeftNode == default(TreeNode<TTreeItem>))
                  @this.LeftNode = new TreeNode<TTreeItem>(treeItem);
               else
               {
                  @this = @this.LeftNode;
                  continue;
               }
            else if (treeItem.CompareTo(@this.Data) > 0) // Insert in right subtree
               if (@this.RightNode == default(TreeNode<TTreeItem>))
                  @this.RightNode = new TreeNode<TTreeItem>(treeItem);
               else
               {
                  @this = @this.RightNode;
                  continue;
               }

            break;
         }
      }

      public static void PreOrderHelper<TTreeItem>(this TreeNode<TTreeItem> @this,
         Action<TreeNode<TTreeItem>> walkAction) where TTreeItem : IComparable<TTreeItem>
      {
         while (true)
         {
            if (@this == default(TreeNode<TTreeItem>))
               return;

            walkAction?.Invoke(@this);
            PreOrderHelper(@this.LeftNode, walkAction);
            @this = @this.RightNode;
         }
      }

      public static void InOrderHelper<TTreeItem>(this TreeNode<TTreeItem> @this,
         Action<TreeNode<TTreeItem>> walkAction) where TTreeItem : IComparable<TTreeItem>
      {
         while (true)
         {
            if (@this == default(TreeNode<TTreeItem>)) return;

            InOrderHelper(@this.LeftNode, walkAction);
            walkAction?.Invoke(@this);
            @this = @this.RightNode;
         }
      }

      public static void PostOrderHelper<TTreeItem>(this TreeNode<TTreeItem> @this,
         Action<TreeNode<TTreeItem>> walkAction)
         where TTreeItem : IComparable<TTreeItem>
      {
         if (@this == default(TreeNode<TTreeItem>))
            return;

         PostOrderHelper(@this.LeftNode, walkAction);
         PostOrderHelper(@this.RightNode, walkAction);
         walkAction?.Invoke(@this);
      }
   }
}