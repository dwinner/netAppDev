using System.Collections;
using StringHandling.Substr;

namespace StringHandling.Tests;

[TestFixture]
public class SubstrTests
{
   [Test(Description = "Knuth-Morris-Pratt substring search")]
   [TestCaseSource(typeof(SubstrTests), nameof(StrCases))]
   public void KmpTest(string needle, string text)
   {
      var expectedIndexOf = text.IndexOf(needle, StringComparison.CurrentCulture);
      var substr = new KnuthMorrisPrattSubstr(needle);
      var actualIndexOf = substr.Search(text);
      Assert.That(actualIndexOf, Is.EqualTo(expectedIndexOf));
   }

   [Test(Description = "Boyer-Moore substring search")]
   [TestCaseSource(typeof(SubstrTests), nameof(StrCases))]
   public void BoyerMooreSubstrTest(string needle, string text)
   {
      var expectedIndexOf = text.IndexOf(needle, StringComparison.CurrentCulture);
      var substr = new BoyerMooreSubstr(needle);
      var actualIndexOf = substr.Search(text);
      Assert.That(actualIndexOf, Is.EqualTo(expectedIndexOf));
   }

   [Test(Description = "RabinKarpSubstr dactiloscope search method")]
   [TestCaseSource(typeof(SubstrTests), nameof(StrCases))]
   public void RabinKarpSubstrTest(string needle, string text)
   {
      var expectedIndexOf = text.IndexOf(needle, StringComparison.CurrentCulture);
      foreach (var algApproach in Enum.GetValues<RabinKarpSubstr.AlgApproach>())
      {
         var substr = new RabinKarpSubstr(needle, algApproach);
         var actualIndexOf = substr.Search(text);
         Assert.That(actualIndexOf, Is.EqualTo(expectedIndexOf));
      }
   }

   public static IEnumerable StrCases
   {
      get
      {
         const string sourceText = "abacadabrabracabracadabrabrabracad";
         yield return new TestCaseData("abracadabra", sourceText);
         yield return new TestCaseData("rab", sourceText);
         yield return new TestCaseData("bcara", sourceText);
         yield return new TestCaseData("rabrabracad", sourceText);
         yield return new TestCaseData("abacad", sourceText);
      }
   }
}