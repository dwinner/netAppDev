namespace ParameterizedSamples;

public class TestFixtureThatUsesClassMethodAsTestCaseSource
{
   [TestCaseSource(
      typeof(AnotherClassWithTestFixtures),
      nameof(AnotherClassWithTestFixtures.DivideCases))]
   public void DivideTest(int n, int d, int q)
   {
      Assert.That(q, Is.EqualTo(n / d));
   }
}

public class AnotherClassWithTestFixtures
{
   public static object[] DivideCases =
   {
      new object[] { 12, 3, 4 },
      new object[] { 12, 2, 6 },
      new object[] { 12, 4, 3 }
   };
}