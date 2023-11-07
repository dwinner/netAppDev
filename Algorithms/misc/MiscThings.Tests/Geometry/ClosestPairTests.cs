using MiscThings.Geometry;
using static MiscThings.Geometry.DistanceCriteria;

namespace MiscThings.Tests.Geometry;

[TestFixture]
public class ClosestPairTests : PointsDistanceBaseTests
{
   [Test]
   public void ClosestPairTest()
   {
      var (distance, point1, point2) = ApplyClosestPair(RsArray);
      Console.WriteLine($"Distance: {distance}. From ({point1}) To ({point2})");
      (distance, point1, point2) = ApplyClosestPair(KwArray);
      Console.WriteLine($"Distance: {distance}. From ({point1}) To ({point2})");
   }

   [Test]
   public void BrootforceAlgTest()
   {
      var (distance, point1, point2) = ApplyBruteforce(RsArray);
      Console.WriteLine($"Distance: {distance}. From ({point1}) To ({point2})");
      (distance, point1, point2) = ApplyBruteforce(KwArray);
      Console.WriteLine($"Distance: {distance}. From ({point1}) To ({point2})");
   }

   private static (double distance, Point2D? point1, Point2D? point2)
      ApplyClosestPair(Point2D[] points)
   {
      var closest = new ClosestPair(points);
      var (distance, point1, point2) = closest.Apply();
      return (distance, point1, point2);
   }

   private static (double distance, Point2D? point1, Point2D? point2)
      ApplyBruteforce(IReadOnlyList<Point2D> points)
   {
      var bestDistance = double.PositiveInfinity;
      Point2D?
         bestPoint1 = null,
         bestPoint2 = null;
      for (var i = 0; i < points.Count - 1; i++)
      {
         for (var j = i + 1; j < points.Count; j++)
         {
            var first = points[i];
            var second = points[j];
            var currentDistance = first.GetDistanceTo(second);
            if (MinDistanceFunc(currentDistance, bestDistance))
            {
               bestDistance = currentDistance;
               bestPoint1 = first;
               bestPoint2 = second;
            }
         }
      }

      return (bestDistance, bestPoint1, bestPoint2);
   }
}