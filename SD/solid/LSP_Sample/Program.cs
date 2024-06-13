using LSP_Sample;
using static System.Console;

var rc = new Rectangle(2, 3);
UseIt(rc);

var sq = new Square(5);
UseIt(sq);
return;

static void UseIt(Rectangle r)
{
   var width = r.Width;
   r.Height = 10;
   WriteLine($"Expected area of {10 * width}, got {r.Area}");
}