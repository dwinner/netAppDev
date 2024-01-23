string[] seq1 = { "A", "b", "C" };
string[] seq2 = { "a", "B", "c" };

foreach (var s in seq1.UnionBy(seq2, x => x.ToUpperInvariant()))
{
   Console.Write(s);
}

Console.WriteLine();

// We can achieve the same result in this simple case with Union and an equality comparer:
foreach (var s in seq1.Union(seq2, StringComparer.InvariantCultureIgnoreCase))
{
   Console.Write(s);
}