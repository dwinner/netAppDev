string[] votes = { "Dogs", "Cats", "Cats", "Dogs", "Dogs" };

IEnumerable<string> query = 
   from vote in votes
   group vote by vote
   into g
   orderby g.Count() descending
   select g.Key;

var winner = query.First(); // Dogs
Console.WriteLine(winner);