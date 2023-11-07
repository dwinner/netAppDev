using StringHandling.Sorting;

namespace StringHandling.Tests.Sorting;

[TestFixture]
public class MsdTests : StringTestsBase
{
   [TestCase("https://algs4.cs.princeton.edu/51radix/shells.txt")]
   [TestCase("https://algs4.cs.princeton.edu/51radix/words3.txt")]
   public async Task MsdTest(string inputUri)
   {
      var content = await GetContentAsync(inputUri);
      var strArray = SplitByWords(content);
      Msd.Sort(strArray);
      Array.ForEach(strArray, Console.WriteLine);
   }
}