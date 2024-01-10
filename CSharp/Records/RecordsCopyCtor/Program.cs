namespace RecordsCopyCtor;

internal class Program
{
   private static void Main(string[] args)
   {
      var p1 = new Point(3, 4);
      var p2 = p1 with { Y = 5 };
      Console.WriteLine(p2);
   }
}

internal record Point(double X, double Y)
{
   protected Point(Point original)
   {
      Console.WriteLine("Copying...");
      X = original.X;
      Y = original.Y;
   }
}