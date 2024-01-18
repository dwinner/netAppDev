var letters = new SortedSet<char>("the quick brown fox");

foreach (var c in letters)
{
   Console.Write(c); //  bcefhiknoqrtuwx
}

Console.WriteLine();

foreach (var c in letters.GetViewBetween('f', 'i'))
{
   Console.Write(c); //  fhi
}