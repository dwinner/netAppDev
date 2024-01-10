var print1 = Print(new Point(0, 0));
var print2 = Print(new Point(1, 1));

Console.WriteLine(print1);
Console.WriteLine(print2);

return;

string Print(object obj) => obj switch
{
   Point(0, 0) => "Empty point",
   Point(var x, var y) when x == y => "Diagonal",
   _ => "Other"
};

internal class Point(int x, int y)
{
   public readonly int X = x, Y = y;

   // Here's our deconstructor:
   public void Deconstruct(out int x, out int y)
   {
      x = X;
      y = Y;
   }
}