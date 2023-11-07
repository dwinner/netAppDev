namespace Searching.Algs.Tests;

[TestFixture]
public class BinarySearchTreeTests
{
   [Test]
   public void BinarySearchTreeTest()
   {
      var bstSymTbl = new BinarySearchTree<char, int>();
      bstSymTbl.Put('A', 8);
      bstSymTbl.Put('C', 4);
      bstSymTbl.Put('E', 12);
      bstSymTbl.Put('H', 5);
      bstSymTbl.Put('L', 11);
      bstSymTbl.Put('M', 9);
      bstSymTbl.Put('P', 10);
      bstSymTbl.Put('R', 3);
      bstSymTbl.Put('S', 0);
      bstSymTbl.Put('X', 7);

      foreach (var @char in bstSymTbl.GetLevelOrdered())
      {
         Console.WriteLine("{0} {1}",
            @char,
            bstSymTbl[@char]);
      }

      Console.WriteLine();

      foreach (var key in bstSymTbl.Keys)
      {
         Console.WriteLine("{0} {1}",
            key,
            bstSymTbl[key]);
      }

      Assert.That(bstSymTbl.Check(), Is.True);
   }
}