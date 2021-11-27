using System;
using System.Collections.Generic;

SortedList<string, string> books = new();
books.Add("Front-end Development with ASP.NET Core", "978-1-119-18140-8");
books.Add("Beginning C# 7 Programming", "978-1-119-45866-1");

books["Enterprise Services"] = "978-0321246738";
books["Professional C# 7 and .NET Core 2.1"] = "978-1-119-44926-3";

foreach (var (key, val) in books)
{
   Console.WriteLine($"{key}, {val}");
}

foreach (string isbn in books.Values)
{
   Console.WriteLine(isbn);
}

foreach (string title in books.Keys)
{
   Console.WriteLine(title);
}

{
   const string title = "Professional C# 10";
   Console.WriteLine(!books.TryGetValue(title, out var isbn)
      ? $"{title} not found"
      : $"{title} found: {isbn}");
}