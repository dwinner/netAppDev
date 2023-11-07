using System.Text.RegularExpressions;
using AppContext.Search;
using static AppContext.Search.SuffixArrayAlgs;

namespace AppContext.Tests;

[TestFixture]
public partial class SuffixArrayTests
{
   [Test]
   public void SuffixArrayXTest()
   {
      const string input = "ABRACADABRA!";
      var suffix1 = new SuffixArrayX(input);
      var suffix2 = new SuffixArray(input);
      var check = true;
      for (var i = 0; check && i < input.Length; i++)
      {
         var startIdx1 = suffix1[i];
         var startIdx2 = suffix2[i];
         if (startIdx1 != startIdx2)
         {
            Console.WriteLine($"suffix1({i}) = {startIdx1}");
            Console.WriteLine($"suffix2({i}) = {startIdx2}");
            var offset1 = Math.Min(startIdx1 + 50, input.Length);
            var ith = $"\"{input[startIdx1..offset1]}\"";
            var offset2 = Math.Min(startIdx2 + 50, input.Length);
            var jth = $"\"{input[startIdx2..offset2]}\"";
            Console.WriteLine(ith);
            Console.WriteLine(jth);
            check = false;
         }
      }

      Console.WriteLine("  i ind lcp rnk  select");
      Console.WriteLine("---------------------------");

      for (var i = 0; i < input.Length; i++)
      {
         var index = suffix2[i];
         var offset = Math.Min(index + 50, input.Length);
         var ith = $"\"{input[index..offset]}\"";
         var rank = suffix2.GetRank(input[index..]);
         Assert.That(suffix2.Select(i), Is.EqualTo(input[index..]));
         if (i == 0)
         {
            Console.WriteLine("{0,3:D} {1,3:D} {2,3} {3,3:D} {4}",
               i, index, "-", rank, ith);
         }
         else
         {
            var lcp = suffix2.GetLongestCommonPrefix(i);
            Console.WriteLine("{0,3:D} {1,3:D} {2,3:D} {3,3:D} {4}",
               i, index, lcp, rank, ith);
         }
      }
   }

   [Test]
   public void SuffixArrayTest()
   {
      const string testStr = "ABRACADABRA!";
      var sfxArray = new SuffixArray(testStr);
      for (var i = 0; i < testStr.Length; i++)
      {
         var index = sfxArray[i];
         var expected = testStr[index..];
         var actual = sfxArray.Select(i);
         Assert.That(actual, Is.EqualTo(expected));
         var rank = sfxArray.GetRank(expected);
         Assert.That(i, Is.EqualTo(rank));
      }
   }

   [TestCase("https://algs4.cs.princeton.edu/63suffix/tale.txt")]
   [TestCase("https://algs4.cs.princeton.edu/63suffix/tinyTale.txt")]
   [TestCase("https://algs4.cs.princeton.edu/63suffix/mobydick.txt")]
   public async Task LongestRepeatedSubstringTest(string url)
   {
      var content = await GetContentAsync(url);
      var whiteReplaceRegExpr = WhiteRegex();
      content = whiteReplaceRegExpr.Replace(content, " ");
      var lrStr = GetLongestRepeatedSubstring(content);
      Console.WriteLine(lrStr);
   }

   [TestCase(
      "https://algs4.cs.princeton.edu/63suffix/tale.txt",
      "https://algs4.cs.princeton.edu/63suffix/mobydick.txt")]
   public async Task GetLongestCommonString(string url1, string url2)
   {
      var content1 = await GetContentAsync(url1);
      content1 = WhiteRegex().Replace(content1.Trim(), " ");
      var content2 = await GetContentAsync(url2);
      content2 = WhiteRegex().Replace(content2.Trim(), " ");
      var lrs = SuffixArrayAlgs.GetLongestCommonString(content1, content2);
      Console.WriteLine(lrs);
   }

   [TestCase("https://algs4.cs.princeton.edu/63suffix/tale.txt", "majesty", 15)]
   [TestCase("https://algs4.cs.princeton.edu/63suffix/mobydick.txt", "the worst", 10)]
   public async Task KwikTest(string url, string keyword, int ctxLen)
   {
      var content = await GetContentAsync(url);
      content = WhiteRegex().Replace(content.Trim(), " ");
      var kwiks = GetKeywordInCtx(content, keyword, ctxLen);
      foreach (var kwik in kwiks)
      {
         Console.WriteLine(kwik);
      }
   }

   private static async Task<string> GetContentAsync(string uriString)
   {
      using var httpClient = new HttpClient();
      var content = await httpClient.GetStringAsync(uriString)
         .ConfigureAwait(false);

      return content;
   }

   [GeneratedRegex("\\s+", RegexOptions.Multiline | RegexOptions.Compiled)]
   private static partial Regex WhiteRegex();
}