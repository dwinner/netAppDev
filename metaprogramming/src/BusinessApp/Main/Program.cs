using Fundamentals;

var types = new Types();
var commands = types.FindMultiple<ICommand>();
var typeNames = string.Join("\n", commands.Select(type => type.Name));
Console.WriteLine(typeNames);

Console.WriteLine("\n\nGDPR Report");
var typesWithConcepts = types.All
   .SelectMany(
      type => type.GetProperties().Where(
         propInf => propInf.PropertyType.IsPiiConcept()))
   .GroupBy(propertyInfo => propertyInfo.DeclaringType);

foreach (var typeWithConcepts in typesWithConcepts)
{
   Console.WriteLine($"Type: {typeWithConcepts.Key!.FullName}");
   foreach (var property in typeWithConcepts)
   {
      Console.WriteLine($"  Property : {property.Name}");
   }
}