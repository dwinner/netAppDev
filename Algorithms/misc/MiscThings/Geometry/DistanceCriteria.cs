namespace MiscThings.Geometry;

public static class DistanceCriteria
{
   public static readonly Func<double, double, bool> MinDistanceFunc = (current, best) => current < best;

   public static readonly Func<double, double, bool> MaxDistanceFunc = (current, best) => current > best;
}