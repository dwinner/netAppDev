using System;

namespace BinaryTree.Lib
{
   public class TreeNode<TTreeItem>
      where TTreeItem : IComparable<TTreeItem>
   {
      public TreeNode(TTreeItem data) => Data = data;
      public TreeNode<TTreeItem> LeftNode { get; set; }
      public TreeNode<TTreeItem> RightNode { get; set; }
      public TTreeItem Data { get; }
   }
}