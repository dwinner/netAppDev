/**
 * Реализация двоичного дерева, для которого
 * имеются три разных способа обхода
 */

using System;

namespace BinaryTree
{
   class Program
   {
      static void Main(string[] args)
      {
         BinaryTree<int> tree = new BinaryTree<int> {{10, 5, 12, 4, 7, 11}};
         Console.WriteLine("Count: {0}", tree.Count);
         Console.Write("Copy to array: ");
         int[] array = new int[tree.Count];
         tree.CopyTo(array, 0);
         foreach (int val in array)
         {
            Console.Write("{0} ", val);
         }
         Console.WriteLine();
         Console.WriteLine("Contains 4 ? {0}", tree.Contains(4) ? "yes" : "no");
         Console.WriteLine("Contains 15 ? {0}", tree.Contains(15) ? "yes" : "no");

         // Обойти дерево разными способами
         Console.Write("Pre-order: ");
         foreach (int val in tree.PreOrder)
         {
            Console.Write("{0} ", val);
         }
         Console.WriteLine();
         Console.Write("Post-order: ");
         foreach (int val in tree.PostOrder)
         {
            Console.Write("{0} ", val);
         }
         Console.WriteLine();
         Console.Write("In-order: ");
         foreach (int val in tree.InOrder)
         {
            Console.Write("{0} ", val);
         }
         Console.WriteLine();

         Console.WriteLine("Removed Root");
         tree.Remove(10);
         Console.Write("In-order: ");
         foreach (int val in tree.InOrder)
         {
            Console.Write("{0} ", val);
         }

         Console.ReadKey();
      }
   }
}
