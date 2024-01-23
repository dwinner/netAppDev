var files = Directory.GetFiles(Path.GetTempPath()).AsQueryable();

// Extensions with less then five files

var groupedEn =
   from file in files
   group file.ToUpper() by Path.GetExtension(file)
   into grouping
   where grouping.Count() < 5
   select grouping;

foreach (var grouping in groupedEn)
{
   Console.WriteLine(grouping.Key);
   foreach (var item in grouping)
   {
      Console.WriteLine($"\t{item}");
   }
}