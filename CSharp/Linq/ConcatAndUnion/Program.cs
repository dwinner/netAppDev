int[] seq1 = { 1, 2, 3 }, seq2 = { 3, 4, 5 };

foreach (var i in seq1.Concat(seq2))
{
   Console.Write(i);
}

Console.WriteLine();

foreach (var i in seq1.Union(seq2))
{
   Console.Write(i);
}