var base1 = typeof(string).BaseType;
var base2 = typeof(FileStream).BaseType;

Console.WriteLine(base1?.Name ?? string.Empty); // Object
Console.WriteLine(base2?.Name ?? string.Empty); // Stream

Console.WriteLine();

foreach (var iType in typeof(Guid).GetInterfaces())
{
   Console.WriteLine(iType.Name);
}