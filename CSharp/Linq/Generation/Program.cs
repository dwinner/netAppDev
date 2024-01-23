using MoreLinq;

Enumerable.Range(5, 5).ForEach(Console.Write);
Console.WriteLine();

Enumerable.Repeat(true, 3).ForEach(Console.Write);
Console.WriteLine();

int[][] numbers =
{
   new[] { 1, 2, 3 },
   new[] { 4, 5, 6 },
   null // this necessitates the null coalescing operator below
};

var flat = numbers
   .SelectMany(innerArray => innerArray ?? Enumerable.Empty<int>());

flat.ForEach(Console.Write);