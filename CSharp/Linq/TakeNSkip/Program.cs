// We need a long list of names for this example, so will indulge in a little
// reflection (using LINQ, of course!) The following query extracts all type 
// names in the System.CoreLib assembly:

using MoreLinq;

string[] typeNames = (from type in typeof(int).Assembly.GetTypes()
      select type.Name)
   .ToArray();

var first20Nth = typeNames
   .Where(t => t.Contains("Exception"))
   .OrderBy(t => t)
   .Take(20);

first20Nth.ForEach(item => Console.WriteLine(item));

var skipped20Nth = typeNames
   .Where(t => t.Contains("Exception"))
   .OrderBy(t => t)
   .Skip(20)
   .Take(20);

skipped20Nth.ForEach(item => Console.WriteLine(item));