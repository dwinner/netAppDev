namespace RecordsEquality;

internal static class Program
{
   private static void Main(string[] args)
   {
      Console.WriteLine(new Point1(1, 2) == new Point1(1, 2)); // False
      Console.WriteLine(new Point2(1, 2) == new Point2(1, 2)); // True
   }
}

internal record Point1(double X, double Y)
{
   private static int _nextInstance;
   private double _someOtherField = _nextInstance++;
}

internal record Point2(double X, double Y)
{
   private static int _nextInstance;
   private double _someOtherField = _nextInstance++;

   public virtual bool Equals(Point2 other) =>
      other != null && X == other.X && Y == other.Y;

   public override int GetHashCode() => (X, Y).GetHashCode();
}