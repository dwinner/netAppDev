namespace StringHandling.Tests.Sorting;

[TestFixture]
public class LsdTests : StringTestsBase
{
   [TestCase("https://algs4.cs.princeton.edu/51radix/words3.txt")]
   public async Task LsdTest(string uriString)
   {
      var content = await GetContentAsync(uriString);
      var stringsToSort = SplitByWords(content);
      var len = stringsToSort.Length;

      // check that strings have fixed length
      Assert.That(len, Is.GreaterThan(0));
      var word1StLen = stringsToSort[0].Length;
      for (var i = 0; i < len; i++)
      {
         Assert.That(stringsToSort[i], Has.Length.EqualTo(word1StLen),
            "Strings must have fixed length");
      }

      // sort the strings
      StringHandling.Sorting.Lsd.Sort(stringsToSort, word1StLen);

      // print results
      Array.ForEach(stringsToSort, Console.WriteLine);
   }
}