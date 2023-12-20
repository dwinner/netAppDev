namespace ParameterizedSamples;

[TestFixtureSource(nameof(_fixtureArgs))]
public class TestFixtureSourceTests
{
   public TestFixtureSourceTests(string word, int num)
   {
   }

   private static object[] _fixtureArgs =
   {
      new object[] { "Question", 1 },
      new object[] { "Answer", 42 }
   };
}