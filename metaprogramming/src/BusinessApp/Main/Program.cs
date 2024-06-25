using Fundamentals;

var types = new Types();
var commands = types.FindMultiple<ICommand>();
var typeNames = string.Join("\n", commands.Select(_ => _.Name));
Console.WriteLine(typeNames);

Console.WriteLine("\n\nGDPR Report");
var typesWithConcepts = types.All
   .SelectMany(type => type.GetProperties()
      .Where(propertyInfo => propertyInfo.PropertyType.IsPIIConcept()))
   .GroupBy(propertyInfo => propertyInfo.DeclaringType);

foreach (var typeWithConcepts in typesWithConcepts)
{
   Console.WriteLine($"Type: {typeWithConcepts.Key!.FullName}");
   foreach (var property in typeWithConcepts)
   {
      Console.WriteLine($"  Property : {property.Name}");
   }
}