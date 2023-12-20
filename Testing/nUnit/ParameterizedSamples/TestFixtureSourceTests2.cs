namespace ParameterizedSamples;

[TestFixtureSource(typeof(AnotherClass), nameof(AnotherClass.FixtureArgs))]
public class TestFixtureSourceTests2
{
   public TestFixtureSourceTests2(string word, int num)
   {
   }
}

internal class AnotherClass
{
   internal static object[] FixtureArgs =
   {
      new object[] { "Question", 1 },
      new object[] { "Answer", 42 }
   };
}