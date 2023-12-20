using System.Collections;

namespace ParameterizedSamples;

[TestFixture]
public class TestCaseDataSamples
{
   [TestCaseSource(typeof(MyDataClass), nameof(MyDataClass.TestCases))]
   public int DivideTest(int n, int d) => n / d;
}

public class MyDataClass
{
   public static IEnumerable TestCases
   {
      get
      {
         yield return new TestCaseData(12, 3).Returns(4);
         yield return new TestCaseData(12, 2).Returns(6);
         yield return new TestCaseData(12, 4).Returns(3);
      }
   }
}