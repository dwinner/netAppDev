using System.Diagnostics;

namespace MiscThings.Geometry;

/// <summary>
///    The <see cref="GrahamScan" /> data type provides methods for computing the
///    convex hull of a set of <em>n</em> points in the plane.
///    <p>
///       The implementation uses the Graham-Scan convex hull algorithm.
///       It runs in O(<em>n</em> log <em>n</em>) time in the worst case
///       and uses O(<em>n</em>) extra memory.
///    </p>
/// </summary>
public sealed class GrahamScan
{
   private readonly Stack<Point2D> _hullStack = new();
   private readonly Point2D[] _points;

   public GrahamScan(Point2D[] points)
   {
      if (points == null)
      {
         throw new ArgumentNullException(nameof(points));
      }

      if (points.Length == 0)
      {
         throw new ArgumentException("array is of length 0", nameof(points));
      }

      _points = new Point2D[points.Length];
      Array.Copy(points, _points, points.Length);

      // preprocess so that a[0] has lowest y-coordinate; break ties by x-coordinate
      // a[0] is an extreme point of the convex hull
      // (alternatively, could do easily in linear time)
      Array.Sort(_points);

      // sort by polar angle with respect to base point a[0],
      // breaking ties by distance to a[0]
      Array.Sort(_points, 1, _points.Length - 1, _points[0].PolarComparer);
   }

   /// <summary>
   ///    The extreme points on the convex hull in counterclockwise order.
   /// </summary>
   public IEnumerable<Point2D> Hull
   {
      get
      {
         var hull = new Stack<Point2D>();
         foreach (var point in _hullStack)
         {
            hull.Push(point);
         }

         return hull;
      }
   }

   /// <summary>
   ///    Computes the convex hull of the specified array of points.
   /// </summary>
   public void Apply()
   {
      // a[0] is the 1St extreme point
      _hullStack.Push(_points[0]);

      // find index k1 of first point not equal to a[0]
      int k1Idx;
      for (k1Idx = 1; k1Idx < _points.Length; k1Idx++)
      {
         if (_points[0] != _points[k1Idx])
         {
            break;
         }
      }

      // all points equal
      if (k1Idx == _points.Length)
      {
         return;
      }

      // find index k2 of first point not collinear with a[0] and a[k1]
      int k2Idx;
      for (k2Idx = k1Idx + 1; k2Idx < _points.Length; k2Idx++)
      {
         if (Point2D.GetCcw(_points[0], _points[k1Idx], _points[k2Idx]) != CcwTurn.Collinear)
         {
            break;
         }
      }

      // a[k2-1] is the second extreme point
      _hullStack.Push(_points[k2Idx - 1]);

      // Graham scan. note that a[n-1] is extreme point different from a[0]
      for (var i = k2Idx; i < _points.Length; i++)
      {
         var top = _hullStack.Pop();
         while (Point2D.GetCcw(_hullStack.Peek(), top, _points[i]) != CcwTurn.CounterClockwise)
         {
            top = _hullStack.Pop();
         }

         _hullStack.Push(top);
         _hullStack.Push(_points[i]);
      }

#if DEBUG
      Debug.Assert(IsConvex());
#endif
   }

#if DEBUG
   // check that boundary of hull is strictly convex
   private bool IsConvex()
   {
      var hullCount = _hullStack.Count;
      if (hullCount <= 2)
      {
         return true;
      }

      var points = new Point2D[hullCount];
      var kIdx = 0;
      foreach (var point in Hull)
      {
         points[kIdx++] = point;
      }

      for (var i = 0; i < hullCount; i++)
      {
         var ccwState = Point2D.GetCcw(points[i], points[(i + 1) % hullCount], points[(i + 2) % hullCount]);
         if (ccwState == CcwTurn.Clockwise)
         {
            return false;
         }
      }

      return true;
   }
#endif
}