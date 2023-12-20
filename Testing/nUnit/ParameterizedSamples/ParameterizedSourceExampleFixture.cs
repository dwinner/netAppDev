namespace ParameterizedSamples;

public class ParameterizedSourceExampleFixture
{
   [TestCaseSource(nameof(TestStrings), new object[] { true })]
   public void LongNameWithEvenNumberOfCharacters(string name)
   {
      Assert.That(name.Length, Is.GreaterThan(5));

      var hasEvenNumOfCharacters = name.Length % 2 == 0;
      Assert.That(hasEvenNumOfCharacters, Is.True);
   }

   [TestCaseSource(nameof(TestStrings), new object[] { false })]
   public void ShortNameWithEvenNumberOfCharacters(string name)
   {
      Assert.That(name.Length, Is.LessThan(15));

      var hasEvenNumOfCharacters = name.Length % 2 == 0;
      Assert.That(hasEvenNumOfCharacters, Is.True);
   }

   private static IEnumerable<string> TestStrings(bool generateLongTestCase)
   {
      if (generateLongTestCase)
      {
         yield return "ThisIsAVeryLongNameThisIsAVeryLongName";
         yield return "SomeName";
         yield return "YetAnotherName";
      }
      else
      {
         yield return "AA";
         yield return "BB";
         yield return "CC";
      }
   }
}