namespace Searching.Algs.Tests;

[TestFixture]
public class BinarySearchSymbolTableTests
{
   [Test]
   public void BinarySearchSymbolTableTreeValidationTest()
   {
      var symbolTable = new BinarySearchSymbolTable<char, int>();
      symbolTable.Put('A', 8);
      symbolTable.Put('C', 4);
      symbolTable.Put('E', 12);
      symbolTable.Put('H', 5);
      symbolTable.Put('L', 11);
      symbolTable.Put('M', 9);
      symbolTable.Put('P', 10);
      symbolTable.Put('R', 3);
      symbolTable.Put('S', 0);
      symbolTable.Put('X', 7);

      Assert.That(symbolTable.Check(), Is.True);

      foreach (var tableKey in symbolTable.Keys)
      {
         Console.WriteLine("{0} {1}",
            tableKey,
            symbolTable[tableKey]);
      }
   }
}