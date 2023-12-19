namespace RangeSample;

public class RangeTests
{
   [SetUp]
   public void Setup()
   {
   }

   [Test]
   [Repeat(2)]
   [Retry(2)]
   public void RangeTest([Values(1, 2, 3)] int x, [Range(0.2, 0.6, 0.2)] double d)
   {
      Console.WriteLine($"{x} {d}");
      Assert.Pass();
   }
}