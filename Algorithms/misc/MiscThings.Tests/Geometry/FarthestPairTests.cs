using MiscThings.Geometry;

namespace MiscThings.Tests.Geometry;

[TestFixture]
public class FarthestPairTests : PointsDistanceBaseTests
{
   [Test]
   public void FarthestPairTest1()
   {
      var farthestPair = new FarthestPair(RsArray);
      var (distance, point1, point2) = farthestPair.Apply();
      Console.WriteLine($"Distance = {distance} from ({point1}) To ({point2})");
   }

   [Test]
   public void FarthestPairTest2()
   {
      var farthestPair = new FarthestPair(KwArray);
      var (distance, point1, point2) = farthestPair.Apply();
      Console.WriteLine($"Distance = {distance} from ({point1}) To ({point2})");
   }

   [Test]
   public void BruteforceTest1()
   {
      var (distance, point1, point2) = Bruteforce(RsArray);
      Console.WriteLine($"Distance = {distance} from ({point1}) To ({point2})");
   }

   [Test]
   public void BruteforceTest2()
   {
      var (distance, point1, point2) = Bruteforce(KwArray);
      Console.WriteLine($"Distance = {distance} from ({point1}) To ({point2})");
   }

   private static (double distance, Point2D? point1, Point2D? point2) Bruteforce(IReadOnlyList<Point2D> points)
   {
      var bestDistance = double.NegativeInfinity;
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
            if (DistanceCriteria.MaxDistanceFunc(currentDistance, bestDistance))
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