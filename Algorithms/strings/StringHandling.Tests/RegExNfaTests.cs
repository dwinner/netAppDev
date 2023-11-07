using System.Collections;

namespace StringHandling.Tests;

[TestFixture]
public class RegExNfaTests : StringTestsBase
{
   public static IEnumerable RegExCases
   {
      get
      {
         yield return new TestCaseData("(A*B|AC)D", "AAAABD", true);
         yield return new TestCaseData("(A*B|AC)D", "AAAAC", false);
         yield return new TestCaseData("(a|(bc)*d)*", "abcbcd", true);
         yield return new TestCaseData("(a|(bc)*d)*", "abcbcbcdaaaabcbcdaaaddd", true);
      }
   }

   [Test(Description = "Manually implemented NFA for regular expressions")]
   [TestCaseSource(typeof(RegExNfaTests), nameof(RegExCases))]
   public void RegExNfaTestsTest(string regExpr, string strToTest, bool isMatch)
   {
      var nfa = new StringHandling.RegExNfa(regExpr);
      var recognized = nfa.Recognized(strToTest);
      Assert.That(recognized, Is.EqualTo(isMatch));
   }

   [Test]
   [TestCase("https://algs4.cs.princeton.edu/54regexp/tinyL.txt", "(A*B|AC)D")]
   public async Task GrepUtilTest(string url, string regExpr)
   {
      var inputRegExpr = $"(.*{regExpr}.*)";
      var content = await GetContentAsync(url);
      var words = SplitByWords(content);
      var expected = new HashSet<string>
      {
         "ABD",
         "ABCCBD"
      };

      var actual = new HashSet<string>();
      Array.ForEach(words, word =>
      {
         var nfa = new StringHandling.RegExNfa(inputRegExpr);
         if (nfa.Recognized(word))
         {
            actual.Add(word);
         }
      });

      var isSame = actual.SetEquals(expected);
      Assert.That(isSame, Is.True);
   }
}