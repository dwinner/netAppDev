namespace RandomSample;

public class RandomTests
{
   [SetUp]
   public void Setup()
   {
   }

   [Test]
   public void Test1([Values(1, 2, 3)] int x, [Random(-1.0, 1.0, 5, Distinct = true)] double d)
   {
      Console.WriteLine($"{x} {d}");
      Assert.Pass();
   }
}