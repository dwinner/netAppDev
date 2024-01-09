namespace RefReadonlySample;

internal class Program
{
   private static void Main()
   {
      var point = new Point
      {
         X = 1, Y = 2
      };

      Console.WriteLine(point.X);
      Console.WriteLine(point.Y);
   }
}

internal ref struct Point
{
   public int X, Y;
}