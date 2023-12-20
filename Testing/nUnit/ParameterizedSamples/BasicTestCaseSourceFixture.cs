namespace ParameterizedSamples;

public class BasicTestCaseSourceFixture
{
   public static object[] DivideCases =
   {
      new object[] { 12, 3, 4 },
      new object[] { 12, 2, 6 },
      new object[] { 12, 4, 3 }
   };

   private static int[] _evenNumbers = { 2, 4, 6, 8 };

   [TestCaseSource(nameof(DivideCases))]
   public void DivideTest(int n, int d, int q)
   {
      Assert.That(q, Is.EqualTo(n / d));
   }

   [Test]
   [TestCaseSource(nameof(_evenNumbers))]
   public void TestMethod(int num)
   {
      Assert.That(num % 2, Is.Zero);
   }
}