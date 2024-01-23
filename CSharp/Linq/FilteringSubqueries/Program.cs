using MoreLinq;

var names = new[] { "Tom", "Dick", "Harry", "Mary", "Jay" }.AsQueryable();
var subQuery = 
   from name in names
   where name.Length == names.Min(n2 => n2.Length)
   select name;

subQuery.ForEach(item => Console.WriteLine(item));