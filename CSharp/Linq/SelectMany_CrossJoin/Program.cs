using MoreLinq;

var players = new[] { "Tom", "Jay", "Mary" }.AsQueryable();

IEnumerable<string> query =
   from name1 in players
   from name2 in players
   select $"{name1} vs {name2}";

query.ForEach(name => Console.WriteLine(name));