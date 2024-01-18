using System.Reflection;

var sorted = new SortedDictionary<string, MethodInfo>();

foreach (var methodInfo in typeof(object).GetMethods())
{
   sorted[methodInfo.Name] = methodInfo;
}

Console.WriteLine("keys");
foreach (var sortedKey in sorted.Keys)
{
   Console.WriteLine(sortedKey);
}

Console.WriteLine("values");
foreach (var sortedValue in sorted.Values)
{
   Console.WriteLine(sortedValue);
}

foreach (var methodInfo in sorted.Values)
{
   Console.WriteLine($"{methodInfo.Name} returns a {methodInfo.ReturnType}");
}

Console.WriteLine(sorted["GetHashCode"]); // Int32 GetHashCode()