using System.Collections;

namespace ParameterizedSamples;

[TestFixtureSource(typeof(FixtureArgs))]
public class TestFixtureSourceTests3
{
   public TestFixtureSourceTests3(string word, int num)
   {
   }
}

internal class FixtureArgs : IEnumerable
{
   public IEnumerator GetEnumerator()
   {
      yield return new object[] { "Question", 1 };
      yield return new object[] { "Answer", 42 };
   }
}