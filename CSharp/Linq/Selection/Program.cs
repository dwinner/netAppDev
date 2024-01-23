using System.Drawing;
using MoreLinq;

var query =
   from f in FontFamily.Families
   select f.Name;

query.ForEach(font => Console.WriteLine(font));

FontFamily.Families.Select(f => f.Name).ForEach(font => Console.WriteLine(font));