// Query syntax provides a shortcut for using the Cast operator on the 
// input sequence. You simply include the type name directly after the from clause:

object[] untyped = { 1, 2, 3 };

// Explicitly calling Cast operator
var query1 =
   from i in untyped.Cast<int>() // Without syntactic shortcut
   select i * 10;
foreach (var q in query1)
{
   Console.Write(q);
}

Console.WriteLine();

// Syntactic shortcut for same query
var query2 =
   from int i in untyped // Notice we've slipped in "int"
   select i * 10;
foreach (var q in query2)
{
   Console.Write(q);
}

var query = "one two two three".Split().Distinct();
var toArray = query.ToArray();
var toList = query.ToList();

Array.ForEach(toArray, Console.Write);
toList.ForEach(Console.Write);