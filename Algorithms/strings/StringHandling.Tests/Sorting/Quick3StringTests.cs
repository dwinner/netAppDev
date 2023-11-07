using StringHandling.Sorting;

namespace StringHandling.Tests.Sorting;

[TestFixture]
public class Quick3StringTests : StringTestsBase
{
   [TestCase("https://algs4.cs.princeton.edu/51radix/words3.txt")]
   [TestCase("https://algs4.cs.princeton.edu/51radix/shells.txt")]
   public async Task Quick3StringTest(string uri)
   {
      var content = await GetContentAsync(uri);
      var words = SplitByWords(content);
      Quick3String.Sort(words);
      Array.ForEach(words, Console.WriteLine);
   }
}