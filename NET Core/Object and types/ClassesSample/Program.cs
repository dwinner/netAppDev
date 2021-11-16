using System;

Book theBook = new("Professional C#")
{
   Publisher = "Wrox Press"
};


Person katharina = new("Katharina", "Nagel");
Console.WriteLine($"{katharina.FirstName} {katharina.LastName}");

GreetingService service = new();
var greeting = service.Greet(katharina);
Console.WriteLine(greeting);

// deconstruction

var (first, last, _) = katharina;
Console.WriteLine($"{first} {last}");