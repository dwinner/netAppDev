namespace Searching.Algs.Tests;

[TestFixture]
public class RedBlackTreeTests
{
   [Test]
   public void RedBlackTreeTest()
   {
      var tree = new RedBlackTree<char, int>
      {
         ['H'] = 5,
         ['L'] = 11,
         ['A'] = 8,
         ['C'] = 4,
         ['E'] = 12,
         ['M'] = 9,
         ['S'] = 0,
         ['X'] = 7,
         ['P'] = 10,
         ['R'] = 3
      };

      Assert.That(tree.Check(out var errorMessage), Is.True, errorMessage);

      foreach (var key in tree.Keys)
      {
         Console.WriteLine("{0}: {1}", key, tree[key]);
      }
   }
}