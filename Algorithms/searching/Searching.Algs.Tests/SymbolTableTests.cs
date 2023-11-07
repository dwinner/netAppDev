namespace Searching.Algs.Tests;

[TestFixture]
public class SymbolTableTests
{
   [Test]
   public void SymbolTableTest()
   {
      string[] strArray =
      {
         "first",
         "second",
         "third",
         "fourth",
         "fifth"
      };
      var symbolTable = new SymbolTable<string, int>();
      for (var i = 0; i < strArray.Length; i++)
      {
         symbolTable[strArray[i]] = i;
      }

      foreach (var item in symbolTable)
      {
         Console.WriteLine(item);
      }

      const int expectedSize = 5;
      Assert.That(symbolTable.Size, Is.EqualTo(expectedSize));
   }

   [Test]
   public void FrequencyCounterTest()
   {
      SymbolTable<string, int> symbolTable = new();
      var distinct = 0;
      var words = 0;

      foreach (var strItem in GetStrings())
      {
         words++;
         if (symbolTable.Contains(strItem))
         {
            symbolTable[strItem] += 1;
         }
         else
         {
            symbolTable[strItem] = 1;
            distinct++;
         }
      }

      // find a key with the highest frequency count
      var max = string.Empty;
      symbolTable[max] = 0;
      foreach (var word in symbolTable.Keys)
      {
         if (symbolTable[word] > symbolTable[max])
         {
            max = word;
         }
      }

      const string expectedMax = "fifth";
      const int expectedDistinctCnt = 5;
      const int expectedWordsCnt = 19;
      const string expectedThMax = "third";
      Assert.Multiple(() =>
      {
         Assert.That(max, Is.EqualTo(expectedMax));
         Assert.That(distinct, Is.EqualTo(expectedDistinctCnt));
         Assert.That(words, Is.EqualTo(expectedWordsCnt));
         Assert.That(symbolTable.Max, Is.EqualTo(expectedThMax));
      });

      static IEnumerable<string> GetStrings()
      {
         yield return "first";
         yield return "first";
         yield return "first";
         yield return "first";
         yield return "first";
         yield return "first";

         yield return "second";
         yield return "second";
         yield return "second";
         yield return "second";
         yield return "second";

         yield return "third";
         yield return "third";
         yield return "third";
         yield return "third";

         yield return "fourth";
         yield return "fourth";
         yield return "fourth";

         yield return "fifth";
      }
   }
}