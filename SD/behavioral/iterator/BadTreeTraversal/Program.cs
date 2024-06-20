using static System.Console;

namespace BadTreeTraversal;

internal class Program
{
   private static void Main()
   {
      //   1
      //  / \
      // 2   3

      // in-order:  213
      // preorder:  123
      // postorder: 231

      var root = new Node<int>(1, new Node<int>(2), new Node<int>(3));

      // C++ style
      var it = new InOrderIterator<int>(root);

      while (it.MoveNext())
      {
         Write(it.Current.Value);
         Write(',');
      }

      WriteLine();

      // C# style
      var tree = new BinaryTree<int>(root);

      WriteLine(string.Join(",", tree.NaturalInOrder.Select(x => x.Value)));

      // duck typing!
      foreach (var node in tree)
      {
         WriteLine(node.Value);
      }
   }
}