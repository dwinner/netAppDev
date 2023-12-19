namespace ParameterizedSamples;

public class GenericCaseTests
{
   [SetUp]
   public void Setup()
   {
   }

   [TestCase(12, 3, ExpectedResult = 4)]
   [TestCase(12, 2, ExpectedResult = 6)]
   [TestCase(12, 4, ExpectedResult = 3)]
   public int DivideTest(int n, int d) => n / d;
}