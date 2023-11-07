using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MiscThings.Geometry;

/// <summary>
///    The <see cref="FarthestPair" /> data type computes the farthest pair of points
///    in a set of <em>n</em> points in the plane and provides accessor methods
///    for getting the farthest pair of points and the distance between them.
///    The distance between two points is their Euclidean distance.
///    <p>
///       This implementation computes the convex hull of the set of points and
///       uses the rotating calipers method to find all antipodal point pairs
///       and the farthest pair.
///       It runs in O(<em>n</em> log <em>n</em>) time in the worst case and uses
///       O(<em>N</em>) extra space.
///       See also <seealso cref="ClosestPair" /> and <seealso cref="GrahamScan" />
///    </p>
/// </summary>
public sealed class FarthestPair
{
   private readonly Point2D[] _points;

   public FarthestPair(Point2D[] points) => _points = points;

   public (double distance, Point2D? point1, Point2D? point2) Apply()
   {
      var distance = double.NegativeInfinity;
      Point2D?
         point1 = null,
         point2 = null;

      // single point
      if (_points.Length <= 1)
      {
         return (default, null, null);
      }

      // number of points on the hull
      var grahamScan = new GrahamScan(_points);
      grahamScan.Apply();
      var hullCount = grahamScan.Hull.Count();

      // the hull, in counterclockwise order hull[1] to hull[m]
      var hullPoints = new Point2D[hullCount + 1];
      hullCount = 1;
      foreach (var point in grahamScan.Hull)
      {
         hullPoints[hullCount++] = point;
      }

      hullCount--;

      // all points are equal
      if (hullCount == 1)
      {
         return (default, null, null);
      }

      // points are collinear
      if (hullCount == 2)
      {
         point1 = hullPoints[1];
         point2 = hullPoints[2];
         distance = point1.GetDistanceSquaredTo(point2);
         return (Math.Sqrt(distance), point1, point2);
      }

      // k = farthest vertex from edge from hull[1] to hull[m]
      var k = 2;
      while (Point2D.GetArea2(hullPoints[hullCount], hullPoints[1], hullPoints[k + 1])
             > Point2D.GetArea2(hullPoints[hullCount], hullPoints[1], hullPoints[k]))
      {
         k++;
      }

      var j = k;
      for (var i = 1; i <= k && j <= hullCount; i++)
      {
#if TRACE
         Debug.WriteLine($"{hullPoints[i]} and {hullPoints[j]} are antipodal");
#endif
         var currentDistance = hullPoints[i].GetDistanceSquaredTo(hullPoints[j]);
         UpdateFarthestValues(hullPoints[i], hullPoints[j], currentDistance);
         while (j < hullCount
                && Point2D.GetArea2(hullPoints[i], hullPoints[i + 1], hullPoints[j + 1]) >
                Point2D.GetArea2(hullPoints[i], hullPoints[i + 1], hullPoints[j]))
         {
            j++;
#if TRACE
            Debug.WriteLine($"{hullPoints[i]} and {hullPoints[j]} are antipodal");
#endif
            var distanceSquared = hullPoints[i].GetDistanceSquaredTo(hullPoints[j]);
            UpdateFarthestValues(hullPoints[i], hullPoints[j], distanceSquared);
         }
      }

      return (Math.Sqrt(distance), point1, point2);

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      void UpdateFarthestValues(Point2D newPoint1, Point2D newPoint2, double newDistance)
      {
         if (newDistance > distance)
         {
            point1 = newPoint1;
            point2 = newPoint2;
            distance = newDistance;
         }
      }
   }
}