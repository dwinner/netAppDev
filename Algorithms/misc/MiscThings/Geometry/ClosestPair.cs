namespace MiscThings.Geometry;

/// <summary>
///    The <see cref="ClosestPair" />  data type computes a closest pair of points
///    in a set of <em>n</em> points in the plane and provides accessor methods
///    for getting the closest pair of points and the distance between them.
///    The distance between two points is their Euclidean distance.
///    <p>
///       This implementation uses a divide-and-conquer algorithm.
///       It runs in O(<em>n</em> log <em>n</em>) time in the worst case and uses
///       O(<em>n</em>) extra space.
///    </p>
/// </summary>
public sealed class ClosestPair
{
   private readonly Point2D[] _points = null!;

   private double _minDistance = double.PositiveInfinity;

   // closest pair of points and their Euclidean distance
   private Point2D? _minPoint1;
   private Point2D? _minPoint2;

   /// <summary>
   ///    Computes the closest pair of points in the specified array of points.
   /// </summary>
   /// <param name="points">The array of points</param>
   public ClosestPair(Point2D[] points)
   {
      if (points == null)
      {
         throw new ArgumentNullException(nameof(points));
      }

      for (var i = 0; i < points.Length; i++)
      {
         if (points[i] == null)
         {
            throw new ArgumentException($"array element {i} is null", nameof(points));
         }
      }

      if (points.Length <= 1)
      {
         return;
      }

      _points = points;
   }

   public (double distance, Point2D? point1, Point2D? point2) Apply()
   {
      var pointsLen = _points.Length;

      // sort by x-coordinate (breaking ties by y-coordinate via stability)
      var pointsByX = new Point2D[pointsLen];
      for (var i = 0; i < pointsLen; i++)
      {
         pointsByX[i] = _points[i];
      }

      Array.Sort(pointsByX);

      // check for coincident points
      for (var i = 0; i < pointsLen - 1; i++)
      {
         if (pointsByX[i] == pointsByX[i + 1])
         {
            return (0.0, pointsByX[i], pointsByX[i + 1]);
         }
      }

      // sort by y-coordinate (but not yet sorted)
      var pointsByY = new Point2D[pointsLen];
      Array.Copy(pointsByX, pointsByY, pointsLen);

      // auxiliary array
      var auxArray = new Point2D[pointsLen];
      Closest(pointsByX, pointsByY, auxArray, 0, pointsLen - 1);

      return (_minDistance, _minPoint1, _minPoint2);
   }

   /**
    * find closest pair of points in pointsByX[lo..hi]
    * precondition:  pointsByX[lo..hi] and pointsByY[lo..hi] are the same sequence of points
    * precondition:  pointsByX[lo..hi] sorted by x-coordinate
    * postcondition: pointsByY[lo..hi] sorted by y-coordinate
    */
   private double Closest(
      IReadOnlyList<Point2D> pointsByX, IList<Point2D> pointsByY, IList<Point2D> aux, int lowIdx, int highIdx)
   {
      if (highIdx <= lowIdx)
      {
         return double.PositiveInfinity;
      }

      var middleIdx = lowIdx + (highIdx - lowIdx) / 2;
      var medianPoint = pointsByX[middleIdx];

      // compute closest pair with both endpoints in left subarray or both in right subarray
      var delta1 = Closest(pointsByX, pointsByY, aux, lowIdx, middleIdx);
      var delta2 = Closest(pointsByX, pointsByY, aux, middleIdx + 1, highIdx);
      var delta = Math.Min(delta1, delta2);

      // merge back so that pointsByY[lo..hi] are sorted by y-coordinate
      Merge(pointsByY, aux, lowIdx, middleIdx, highIdx);

      // aux[0..m-1] = sequence of points closer than delta, sorted by y-coordinate
      var mIdx = 0;
      for (var i = lowIdx; i <= highIdx; i++)
      {
         if (Math.Abs(pointsByY[i].X - medianPoint.X) < delta)
         {
            aux[mIdx++] = pointsByY[i];
         }
      }

      // compare each point to its neighbors with y-coordinate closer than delta
      for (var i = 0; i < mIdx; i++)
      {
         // a geometric packing argument shows that this loop iterates at most 7 times
         for (var j = i + 1;
              j < mIdx && aux[j].Y - aux[i].Y < delta;
              j++)
         {
            var distance = aux[i].GetDistanceTo(aux[j]);
            if (distance < delta)
            {
               delta = distance;
               if (distance < _minDistance)
               {
                  _minDistance = delta;
                  _minPoint1 = aux[i];
                  _minPoint2 = aux[j];
               }
            }
         }
      }

      return delta;
   }

   // is v < w ?
   private static bool Less<T>(T? first, T? second)
      where T : IComparable<T> =>
      first != null && first.CompareTo(second) < 0;

   // stably merge a[lo .. mid] with a[mid+1 ..hi] using aux[lo .. hi]
   // precondition: a[lo .. mid] and a[mid+1 .. hi] are sorted subarrays
   private static void Merge<T>(
      IList<T> pointArray, IList<T> auxArray, int lowIdx, int middleIdx, int highIdx)
      where T : IComparable<T>
   {
      // copy to aux[]
      for (var k = lowIdx; k <= highIdx; k++)
      {
         auxArray[k] = pointArray[k];
      }

      // merge back to a[]
      int i = lowIdx, j = middleIdx + 1;
      for (var k = lowIdx; k <= highIdx; k++)
      {
         if (i > middleIdx)
         {
            pointArray[k] = auxArray[j++];
         }
         else if (j > highIdx)
         {
            pointArray[k] = auxArray[i++];
         }
         else if (Less(auxArray[j], auxArray[i]))
         {
            pointArray[k] = auxArray[j++];
         }
         else
         {
            pointArray[k] = auxArray[i++];
         }
      }
   }
}