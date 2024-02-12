var unbound = typeof(IEnumerator<>).GetProperty("Current");
var closed = typeof(IEnumerator<int>).GetProperty("Current");

Console.WriteLine(unbound); // T Current
Console.WriteLine(closed); // Int32 Current

Console.WriteLine(unbound.PropertyType.IsGenericParameter); // True
Console.WriteLine(closed.PropertyType.IsGenericParameter); // False

// The MemberInfo objects returned from unbound and closed generic types are always distinct
// — even for members whose signatures don’t feature generic type parameters:

unbound = typeof(List<>).GetProperty("Count");
closed = typeof(List<int>).GetProperty("Count");

Console.WriteLine(unbound); // Int32 Count
Console.WriteLine(closed); // Int32 Count

Console.WriteLine(unbound == closed); // False

Console.WriteLine(unbound.DeclaringType.IsGenericTypeDefinition); // True
Console.WriteLine(closed.DeclaringType.IsGenericTypeDefinition); // False