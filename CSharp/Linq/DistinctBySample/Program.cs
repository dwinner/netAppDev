var distinctByRes = new[] { 1.0, 1.1, 2.0, 2.1, 3.0, 3.1 }.DistinctBy(n => Math.Round(n, 0));
foreach (var item in distinctByRes)
{
   Console.WriteLine(item);
}

var distincted = "HelloWorld".Distinct();
foreach (var charAt in distincted)
{
   Console.WriteLine(charAt);
}