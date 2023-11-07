using MoreLinq.Extensions;

namespace Searching.Algs.Tests;

[TestFixture]
public class LinearProbingHashSymbolTableTests
{
   [Test]
   public void LinearProbingHashSymbolTableTest()
   {
      var symbolTable = new LinearProbingHashSymbolTable<string?, int>();

      Assert.Multiple(() =>
      {
         Assert.That(symbolTable.Size, Is.EqualTo(0));
         Assert.That(symbolTable.IsEmpty, Is.True);
      });

      symbolTable["first"] = 1;
      symbolTable["second"] = 2;
      symbolTable["third"] = 3;
      symbolTable["fourth"] = 4;
      symbolTable["fifth"] = 5;
      symbolTable["sixth"] = 6;
      symbolTable["seventh"] = 7;
      symbolTable["eighth"] = 8;
      symbolTable["ninth"] = 9;
      symbolTable["tenth"] = 10;
      symbolTable["eleventh"] = 11;
      symbolTable["twelfth"] = 12;
      symbolTable["thirteenth"] = 13;

      const int expectedSize = 13;
      Assert.That(symbolTable.Size, Is.EqualTo(expectedSize));

      symbolTable.Keys.ForEach(key => Console.WriteLine($"{key} = {symbolTable[key]}"));
      Assert.Multiple(() =>
      {
         Assert.That(symbolTable.Contains("first"), Is.True);
         Assert.That(symbolTable.Contains("second"), Is.True);
         Assert.That(symbolTable.Contains("third"), Is.True);
      });

      symbolTable.Delete("first");

      Assert.That(symbolTable.Contains("first"), Is.False);
      Assert.That(symbolTable.Check(out var message), Is.True, message);
   }
}