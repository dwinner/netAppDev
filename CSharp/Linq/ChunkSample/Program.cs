var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };

foreach (var ints in numbers.Chunk(3))
{
   Array.ForEach(ints, Console.Write);
   Console.WriteLine();
}

foreach (var chunk in numbers.Chunk(3))
{
   Console.WriteLine(string.Join(", ", chunk));
}