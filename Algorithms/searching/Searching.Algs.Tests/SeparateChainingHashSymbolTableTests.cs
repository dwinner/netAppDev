namespace Searching.Algs.Tests;

[TestFixture]
public class SeparateChainingHashSymbolTableTests
{
   [Test]
   public void SeparateChainingHashSymbolTableTest()
   {
      var hashSymbolTable = new SeparateChainingHashSymbolTable<string, int>();
      Assert.Multiple(() =>
      {
         Assert.That(hashSymbolTable.Size, Is.EqualTo(0));
         Assert.That(hashSymbolTable.IsEmpty, Is.True);
      });

      hashSymbolTable["first"] = 1;
      hashSymbolTable["second"] = 2;
      hashSymbolTable["third"] = 3;
      hashSymbolTable["fourth"] = 4;
      hashSymbolTable["fifth"] = 5;
      hashSymbolTable["sixth"] = 6;
      hashSymbolTable["seventh"] = 7;
      hashSymbolTable["eights"] = 8;
      hashSymbolTable["first"] = 15;
      hashSymbolTable["sixth"] = 17;

      Assert.That(hashSymbolTable.Contains("first"), Is.True);
      hashSymbolTable.Delete("first");
      Assert.That(hashSymbolTable.Contains("first"), Is.False);

      foreach (var key in hashSymbolTable.Keys)
      {
         Console.WriteLine("{0} = {1}",
            key,
            hashSymbolTable[key]);
      }
   }
}