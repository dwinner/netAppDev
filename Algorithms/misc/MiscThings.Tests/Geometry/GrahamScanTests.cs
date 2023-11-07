using MiscThings.Geometry;

namespace MiscThings.Tests.Geometry;

[TestFixture(Description = "Computing convex hull")]
public class GrahamScanTests : PointsDistanceBaseTests
{
   [Test]
   public void GrahamScanTest1()
   {
      var grahamScan = new GrahamScan(RsArray);
      grahamScan.Apply();
      foreach (var point in grahamScan.Hull)
      {
         Console.WriteLine(point);
      }
   }

   [Test]
   public void GrahamScanTest2()
   {
      var grahamScan = new GrahamScan(KwArray);
      grahamScan.Apply();
      foreach (var point in grahamScan.Hull)
      {
         Console.WriteLine(point);
      }
   }
}