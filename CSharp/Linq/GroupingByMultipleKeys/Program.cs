var grouped = from n in new[] { "Tom", "Dick", "Harry", "Mary", "Jay" }.AsQueryable()
   group n by new
   {
      FirstLetter = n[0],
      n.Length
   };

foreach (var grouping in grouped)
{
   Console.WriteLine(grouping.Key);
   foreach (var item in grouping)
   {
      Console.WriteLine($"\t{item}");
   }
}