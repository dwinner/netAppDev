using AdapterWithCaching;
using MoreLinq;
using static System.Console;

var vectorObjects = new List<VectorObject>
{
   new VectorRectangle(1, 1, 10, 10),
   new VectorRectangle(3, 3, 6, 6)
};

Draw();
Draw();

return;

void Draw()
{
   foreach (var adapter in
            from vectorObj in vectorObjects
            from line in vectorObj
            select new LineToPointAdapter(line))
   {
      adapter.ForEach(DrawPoint);
   }
}

void DrawPoint(Point p)
{
   Write(".");
}