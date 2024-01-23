using System.Drawing;

var projection = from f in FontFamily.Families.AsQueryable()
   select new
   {
      f.Name,
      LineSpacing = f.GetLineSpacing(FontStyle.Bold)
   };

foreach (var item in projection)
{
   Console.WriteLine($"{item.Name}: {item.LineSpacing}");
}