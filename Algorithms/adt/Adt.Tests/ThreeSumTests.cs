using MoreLinq;

namespace Adt.Tests;

[TestFixture]
public class ThreeSumTests
{
   [OneTimeSetUp]
   public void Init()
   {
      const int thresholdValue = 3_000;
      _array = Enumerable.Range(-thresholdValue, 2 * thresholdValue).Shuffle().ToArray();
      Array.Sort(_array, Comparer<int>.Default);
   }

   private static int[] _array = null!;

   [Test]
   public void CountN3Test()
   {
      var countN3 = ThreeSum.CountN3(_array);
      Assert.Pass($"{nameof(countN3)} = {countN3}");
   }

   [Test]
   public void CountN2LogNTest()
   {
      var countN2LogN = ThreeSum.CountN2LogN(_array);
      Assert.Pass($"{nameof(countN2LogN)} = {countN2LogN}");
   }
}