using MoreLinq;

var names = new[] { "Tom", "Dick", "Harry", "Mary", "Jay" }.AsQueryable();

// By length, then alphabetically
names
   .OrderBy(s => s.Length)
   .ThenBy(s => s)
   .ForEach(name => Console.WriteLine(name));


// By length desc, then second character, then first character
names
   .OrderByDescending(s => s.Length)
   .ThenBy(s => s[1])
   .ThenBy(s => s[0])
   .ForEach(name => Console.WriteLine(name));

// Same query in query syntax
(from s in names
   orderby s.Length, s[1], s[0]
   select s).ForEach(n => Console.WriteLine(n));