using System;

Book2 book2 = new("Professional C#", "Wrox Press");
Console.WriteLine(book2);

Book1 book1A = new() { Title = "Professional C#", Publisher = "Wrox Press" };
Book1 book1B = new() { Title = "Professional C#", Publisher = "Wrox Press" };
if (!ReferenceEquals(book1A, book1B))
{
   Console.WriteLine("Two different references for equal records");
}

if (book1A == book1B)
{
   Console.WriteLine("Both records have the same values");
}

var aNewBook = book1A with { Title = "Professional C# and .NET - 2024" };

// nominal records - would be init-only properties, but this seems to have changed
//public record Book2 { string Title; string Publisher; }
public record Book1
{
   public string Title { get; init; } = string.Empty;
   public string Publisher { get; init; } = string.Empty;
}

// positional record
public record Book2(string Title, string Publisher);

// implements Equals, ==, != operators, GetHashCode, properties with get/init, Deconstruct

public record Book3(string Title, string Publisher) : Book2(Title, Publisher)
{
   public Type GetContract() => EqualityContract;
}