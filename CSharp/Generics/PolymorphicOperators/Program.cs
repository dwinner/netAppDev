namespace PolymorphicOperators;

internal static class Program
{
   private static void Main()
   {
      // With a constrained type parameter, we can then write a method that calls our 
      // addition operator polymorphically (with edge-case handling omitted for brevity):

      var point = Sum(new Point(1, 2), new Point(3, 4));
      Console.WriteLine(point);

      //Our call to the + operator (via the += operator) is polymorphic because it binds to
      // IAddable<T>, not Point. Hence, our Sum method works with all types that implement IAddable<T>.
   }

   private static T Sum<T>(params T[] values) where T : IAddable<T>
   {
      var total = values[0];
      for (var i = 1; i < values.Length; i++)
      {
         total += values[i];
      }

      return total;
   }
}

internal interface IAddable<T> where T : IAddable<T>
{
   static abstract T operator +(T left, T right);
}

// Here’s how we can implement the interface:
internal record Point(int X, int Y) : IAddable<Point>
{
   public static Point operator +(Point left, Point right) =>
      new(left.X + right.X, left.Y + right.Y);
}