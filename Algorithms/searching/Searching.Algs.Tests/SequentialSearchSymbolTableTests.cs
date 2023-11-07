namespace Searching.Algs.Tests;

[TestFixture]
public class SequentialSearchSymbolTableTests
{
   [Test]
   public void SequentialSearchSymbolTableTest()
   {
      var symbolTable = new SequentialSearchSymbolTable<char, int>();

      Assert.That(symbolTable.IsEmpty, Is.True);

      symbolTable['L'] = 11;
      symbolTable['P'] = 10;
      symbolTable['M'] = 9;
      symbolTable['X'] = 7;
      symbolTable['H'] = 5;
      symbolTable['C'] = 4;
      symbolTable['R'] = 3;
      symbolTable['A'] = 8;
      symbolTable['E'] = 12;
      symbolTable['L'] = 11;
      symbolTable['S'] = 0;

      const int expectedSize = 10;
      Assert.That(symbolTable.Size, Is.EqualTo(expectedSize));

      foreach (var key in symbolTable.Keys)
      {
         Console.WriteLine("{0} {1}", key, symbolTable[key]);
      }
   }
}