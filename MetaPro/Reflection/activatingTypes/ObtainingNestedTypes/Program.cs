foreach (var type in typeof(Environment).GetNestedTypes())
{
   Console.WriteLine(type.FullName);
}

// The CLR treats a nested type as having special “nested” accessibility levels:
var sf = typeof(Environment.SpecialFolder);
Console.WriteLine(sf.IsPublic); // False
Console.WriteLine(sf.IsNestedPublic); // True