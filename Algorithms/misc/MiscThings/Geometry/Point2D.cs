namespace MiscThings.Geometry;

/// <summary>
///    The <see cref="Point2D" /> class is an immutable data type to encapsulate a
///    two-dimensional point with real-value coordinates.
///    <p>
///       Note: in order to deal with the difference behavior of double and
///       Double with respect to -0.0 and +0.0, the Point2D constructor converts
///       any coordinates that are -0.0 to +0.0.
///    </p>
/// </summary>
public sealed class Point2D :
   IComparable<Point2D>,
   IComparable,
   IEquatable<Point2D>
{
   private static readonly Comparer<Point2D> DefaultComparer = Comparer<Point2D>.Default;

   private static PointEqualityComparer? _defaultEqComparer;

   private static IComparer<Point2D>? _xOrderComparer;

   private static IComparer<Point2D>? _yOrderComparer;

   private static IComparer<Point2D>? _rComparer;

   private static IComparer<Point2D>? _originPointComparer;

   private IComparer<Point2D>? _angleComparer;

   private IComparer<Point2D>? _distanceToComparer;

   private IComparer<Point2D>? _polarComparer;

   /// <summary>
   ///    Initializes a new point (x, y).
   /// </summary>
   /// <param name="x">The x-coordinate</param>
   /// <param name="y">The y-coordinate</param>
   /// <exception cref="ArgumentException">If at least one of the parameters is infinity or not a number</exception>
   public Point2D(double x, double y)
   {
      if (double.IsInfinity(x) || double.IsNaN(x))
      {
         throw new ArgumentException($"Coordinate {nameof(x)} must be finite number", nameof(x));
      }

      if (double.IsInfinity(y) || double.IsNaN(y))
      {
         throw new ArgumentException($"Coordinate {nameof(y)} must be finite number", nameof(y));
      }

      X = x;
      Y = y;
   }

   public Point2D()
      : this(default, default)
   {
   }

   /// <summary>
   ///    The x-coordinate.
   /// </summary>
   public double X { get; }

   /// <summary>
   ///    The y-coordinate.
   /// </summary>
   public double Y { get; }

   /// <summary>
   ///    The polar radius of this point.
   /// </summary>
   public double Radius => Math.Sqrt(X * X + Y * Y);

   /// <summary>
   ///    the angle (in radians) of this point in polar coordinates (between -<see cref="Math.PI" /> and
   ///    <see cref="Math.PI" />)
   /// </summary>
   public double Theta => Math.Atan2(Y, X);

   public static IEqualityComparer<Point2D> DefaultEqComparer =>
      _defaultEqComparer ??= new PointEqualityComparer();

   public static IComparer<Point2D> XOrderComparer =>
      _xOrderComparer ??= new XOrder();

   public static IComparer<Point2D> YOrderComparer =>
      _yOrderComparer ??= new YOrder();

   public static IComparer<Point2D> RComparer =>
      _rComparer ??= new ROrder();

   public IComparer<Point2D> AngleComparer =>
      _angleComparer ??= new Atan2Order(this);

   public IComparer<Point2D> PolarComparer =>
      _polarComparer ??= new PolarOrderComparer(this);

   public IComparer<Point2D> DistanceComparer =>
      _distanceToComparer ??= new DistanceToComparer(this);

   public static IComparer<Point2D> OriginPointComparer =>
      _originPointComparer ??= new ByOriginPointComparer();

   public int CompareTo(object? obj)
   {
      if (ReferenceEquals(null, obj))
      {
         return 1;
      }

      if (ReferenceEquals(this, obj))
      {
         return 0;
      }

      return obj is Point2D other
         ? CompareTo(other)
         : throw new ArgumentException($"Object must be of type {nameof(Point2D)}");
   }

   /// <summary>
   ///    Compares two points by y-coordinate, breaking ties by x-coordinate.
   /// </summary>
   /// <remarks>
   ///    Formally, the invoking point (x0, y0) is less than the argument point (x1, y1)
   ///    if and only if either y0 is less than y1 or if y0 is equal to y and x0 is less than x1.
   /// </remarks>
   /// <param name="other">The other point</param>
   /// <returns>
   ///    the value 0 if this string is equal to the argument
   ///    string (precisely when equals() returns true);
   ///    a negative integer if this point is less than the argument
   ///    point; and a positive integer if this point is greater than the argument point
   /// </returns>
   public int CompareTo(Point2D? other)
   {
      if (ReferenceEquals(this, other))
      {
         return 0;
      }

      if (ReferenceEquals(null, other))
      {
         return 1;
      }

      if (Y < other.Y)
      {
         return -1;
      }

      if (Y > other.Y)
      {
         return +1;
      }

      if (X < other.X)
      {
         return -1;
      }

      if (X > other.X)
      {
         return +1;
      }

      return 0;
   }

   public bool Equals(Point2D? other)
   {
      if (ReferenceEquals(null, other))
      {
         return false;
      }

      if (ReferenceEquals(this, other))
      {
         return true;
      }

      return X.Equals(other.X) && Y.Equals(other.Y);
   }

   public override bool Equals(object? obj) =>
      ReferenceEquals(this, obj) || (obj is Point2D other && Equals(other));

   public override int GetHashCode() => HashCode.Combine(X, Y);

   public static bool operator ==(Point2D? left, Point2D? right) =>
      Equals(left, right);

   public static bool operator !=(Point2D? left, Point2D? right) =>
      !Equals(left, right);

   public static bool operator <(Point2D? left, Point2D? right) =>
      DefaultComparer.Compare(left, right) < 0;

   public static bool operator >(Point2D? left, Point2D? right) =>
      DefaultComparer.Compare(left, right) > 0;

   public static bool operator <=(Point2D? left, Point2D? right) =>
      DefaultComparer.Compare(left, right) <= 0;

   public static bool operator >=(Point2D? left, Point2D? right) =>
      DefaultComparer.Compare(left, right) >= 0;

   public override string ToString() => $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";

   /// <summary>
   ///    Returns the angle between this point and that point.
   /// </summary>
   /// <param name="that">That point</param>
   /// <returns>The angle in radians (between –PI and PI) between this point and that point (0 if equal)</returns>
   private double GetAngleTo(Point2D that)
   {
      var dx = that.X - X;
      var dy = that.Y - Y;
      return Math.Atan2(dy, dx);
   }

   /// <summary>
   ///    Returns Turn for if a→b→c points.
   /// </summary>
   /// <param name="pointA">The first point</param>
   /// <param name="pointB">The second point</param>
   /// <param name="pointC">The third point</param>
   /// <returns>The turn for points</returns>
   public static CcwTurn GetCcw(Point2D pointA, Point2D pointB, Point2D pointC)
   {
      var area2 = (pointB.X - pointA.X) * (pointC.Y - pointA.Y) - (pointB.Y - pointA.Y) * (pointC.X - pointA.X);
      return area2 switch
      {
         < 0 => CcwTurn.Clockwise,
         > 0 => CcwTurn.CounterClockwise,
         _ => CcwTurn.Collinear
      };
   }

   /// <summary>
   ///    Returns twice the signed area of the triangle a-b-c.
   /// </summary>
   /// <param name="pointA">First point</param>
   /// <param name="pointB">Second point</param>
   /// <param name="pointC">Third point</param>
   /// <returns>Twice the signed area of the triangle a-b-c</returns>
   public static double GetArea2(Point2D pointA, Point2D pointB, Point2D pointC) =>
      (pointB.X - pointA.X) * (pointC.Y - pointA.Y)
      - (pointB.Y - pointA.Y) * (pointC.X - pointA.X);

   /// <summary>
   ///    Returns the Euclidean distance between this point and that point.
   /// </summary>
   /// <param name="that">The other point</param>
   /// <returns>The Euclidean distance between this point and that point</returns>
   public double GetDistanceTo(Point2D that)
   {
      var dx = X - that.X;
      var dy = Y - that.Y;
      return Math.Sqrt(dx * dx + dy * dy);
   }

   /// <summary>
   ///    Returns the square of the Euclidean distance between this point and that point.
   /// </summary>
   /// <param name="that">The other point</param>
   /// <returns>The square of the Euclidean distance between this point and that point</returns>
   public double GetDistanceSquaredTo(Point2D that)
   {
      var dx = X - that.X;
      var dy = Y - that.Y;
      return dx * dx + dy * dy;
   }

   private sealed class PointEqualityComparer : IEqualityComparer<Point2D>
   {
      public bool Equals(Point2D? x, Point2D? y) => x?.Equals(y) ?? false;

      public int GetHashCode(Point2D obj) => obj.GetHashCode();
   }

   /// <summary>
   ///    Compare points according to their x-coordinate
   /// </summary>
   private sealed class XOrder : IComparer<Point2D>
   {
      public int Compare(Point2D? point1, Point2D? point2) =>
         point1 switch
         {
            null when ReferenceEquals(point2, null) => 0,
            null => -1,
            _ => ReferenceEquals(point2, null)
               ? 1
               : point1.X.CompareTo(point2.X)
         };
   }

   /// <summary>
   ///    Compare points according to their y-coordinate
   /// </summary>
   private sealed class YOrder : IComparer<Point2D>
   {
      public int Compare(Point2D? point1, Point2D? point2) =>
         point1 switch
         {
            null when ReferenceEquals(point2, null) => 0,
            null => -1,
            _ => ReferenceEquals(point2, null)
               ? 1
               : point1.Y.CompareTo(point2.Y)
         };
   }

   /// <summary>
   ///    Compare points according to their polar radius
   /// </summary>
   private sealed class ROrder : IComparer<Point2D>
   {
      public int Compare(Point2D? point1, Point2D? point2)
      {
         if (point1 == null)
         {
            throw new ArgumentNullException(nameof(point1));
         }

         if (point2 == null)
         {
            throw new ArgumentNullException(nameof(point2));
         }

         var delta = point1.X * point1.X + point1.Y * point1.Y
                     - (point2.X * point2.X + point2.Y * point2.Y);
         return delta.CompareTo(default);
      }
   }

   /// <summary>
   ///    Compares two points by atan2() angle (between –PI and PI) with respect to this point.
   /// </summary>
   private sealed class Atan2Order : IComparer<Point2D>
   {
      private readonly Point2D _point;

      public Atan2Order(Point2D point) => _point = point;

      public int Compare(Point2D? point1, Point2D? point2)
      {
         if (point1 == null)
         {
            throw new ArgumentNullException(nameof(point1));
         }

         if (point2 == null)
         {
            throw new ArgumentNullException(nameof(point2));
         }

         var angle1 = _point.GetAngleTo(point1);
         var angle2 = _point.GetAngleTo(point2);

         return angle1.CompareTo(angle2);
      }
   }

   /// <summary>
   ///    Compare other points relative to polar angle (between 0 and 2pi) they make with this Point
   /// </summary>
   private sealed class PolarOrderComparer : IComparer<Point2D>
   {
      private readonly Point2D _basedPoint;

      public PolarOrderComparer(Point2D basedPoint) => _basedPoint = basedPoint;

      public int Compare(Point2D? point1, Point2D? point2)
      {
         if (point1 == null)
         {
            throw new ArgumentNullException(nameof(point1));
         }

         if (point2 == null)
         {
            throw new ArgumentNullException(nameof(point2));
         }

         var deltaX1 = point1.X - _basedPoint.X;
         var deltaY1 = point1.Y - _basedPoint.Y;
         var deltaX2 = point2.X - _basedPoint.X;
         var deltaY2 = point2.Y - _basedPoint.Y;

         // q1 above; q2 below
         if (deltaY1 >= 0 && deltaY2 < 0)
         {
            return -1;
         }

         // q1 below; q2 above
         if (deltaY2 >= 0 && deltaY1 < 0)
         {
            return +1;
         }

         // 3-collinear and horizontal
         if (deltaY1 == 0 && deltaY2 == 0)
         {
            if (deltaX1 >= 0 && deltaX2 < 0)
            {
               return -1;
            }

            if (deltaX2 >= 0 && deltaX1 < 0)
            {
               return +1;
            }

            return 0;
         }

         // both above or below
         return ByCcw(point1, point2);
      }

      private int ByCcw(Point2D point1, Point2D point2)
      {
         var ccwTurn = GetCcw(_basedPoint, point1, point2);
         var intState = (int)ccwTurn;
         return -intState;
      }
   }

   /// <summary>
   ///    Compare points according to their distance to this point
   /// </summary>
   private sealed class DistanceToComparer : IComparer<Point2D>
   {
      private readonly Point2D _point;

      public DistanceToComparer(Point2D point) => _point = point;

      public int Compare(Point2D? point1, Point2D? point2)
      {
         if (point1 == null)
         {
            throw new ArgumentNullException(nameof(point1));
         }

         if (point2 == null)
         {
            throw new ArgumentNullException(nameof(point2));
         }

         var distance1 = _point.GetDistanceSquaredTo(point1);
         var distance2 = _point.GetDistanceSquaredTo(point2);

         return distance1.CompareTo(distance2);
      }
   }

   private sealed class ByOriginPointComparer : IComparer<Point2D>
   {
      private static readonly Point2D OriginPoint = new();

      public int Compare(Point2D? point1, Point2D? point2)
      {
         if (ReferenceEquals(point1, point2))
         {
            return 0;
         }

         if (ReferenceEquals(null, point2))
         {
            return 1;
         }

         if (ReferenceEquals(null, point1))
         {
            return -1;
         }

         // Comparing by origin point distance
         var distance1 = point1.GetDistanceTo(OriginPoint);
         var distance2 = point2.GetDistanceTo(OriginPoint);
         return distance1 > distance2
            ? 1
            : Math.Abs(distance1 - distance2) < double.Epsilon
               ? 0
               : -1;
      }
   }
}