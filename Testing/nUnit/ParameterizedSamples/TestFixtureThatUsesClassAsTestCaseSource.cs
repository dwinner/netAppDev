using System.Collections;

namespace ParameterizedSamples;

public class TestFixtureThatUsesClassAsTestCaseSource
{
   [TestCaseSource(typeof(DivideCasesClass))]
   public void DivideTest(int n, int d, int q)
   {
      Assert.That(q, Is.EqualTo(n / d));
   }
}

public class DivideCasesClass : IEnumerable
{
   public IEnumerator GetEnumerator()
   {
      yield return new object[] { 12, 3, 4 };
      yield return new object[] { 12, 2, 6 };
      yield return new object[] { 12, 4, 3 };
   }
}