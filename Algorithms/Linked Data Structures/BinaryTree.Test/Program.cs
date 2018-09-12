using System;
using BinaryTree.Lib;

namespace BinaryTree.Test
{
   internal static class Program
   {
      private static void Main()
      {
         var tree = new Tree<int>();

         Console.WriteLine("Inserting values: ");
         var random = new Random();

         for (var i = 1; i <= 10; i++)
         {
            var insertedValue = random.Next(100);
            Console.Write($"{insertedValue}");
            tree.InsertNode(insertedValue);
         }

         void WalkAction(TreeNode<int> node) => Console.Write($"{node.Data} ");

         Console.WriteLine("\n\nPreorder traversal");
         tree.Visit(VisitMode.PreOrder, WalkAction);

         Console.WriteLine("\n\nInorder traversal");
         tree.Visit(VisitMode.InOrder, WalkAction);

         Console.WriteLine("\n\nPostorder traversal");
         tree.Visit(VisitMode.PostOrder, WalkAction);

         Console.WriteLine();
      }
   }
}