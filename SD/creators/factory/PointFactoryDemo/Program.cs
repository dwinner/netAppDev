using PointFactoryDemo;

var p1 = new Point(2, 3);
var origin = Point.Origin;
var p2 = Point.Factory.NewCartesianPoint(1, 2);

Console.WriteLine(p1);
Console.WriteLine(origin);
Console.WriteLine(p2);