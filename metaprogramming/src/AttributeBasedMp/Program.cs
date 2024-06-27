using System.Reflection;
using Fundamentals;
using Fundamentals.Compliance.GDPR;

var types = new Types();

var piiTypes = types.All
   .Where(type => type.GetMembers()
      .Any(memberInfo => memberInfo.HasAttribute<PersonalIdentifiableInformationAttribute>()));
var typeNames = string.Join("\n", piiTypes.Select(type => type.FullName));
Console.WriteLine(typeNames);

Console.WriteLine("\n\nGDPR Report");
var typesWithPii = types.All
   .SelectMany(type => type.GetProperties()
      .Where(propertyInfo => propertyInfo.HasAttribute<PersonalIdentifiableInformationAttribute>()))
   .GroupBy(propertyInfo => propertyInfo.DeclaringType);

foreach (var typeWithPii in typesWithPii)
{
   Console.WriteLine($"Type: {typeWithPii.Key!.FullName}");
   foreach (var property in typeWithPii)
   {
      var pii = property.GetCustomAttribute<PersonalIdentifiableInformationAttribute>();
      Console.WriteLine($"  Property : {property.Name}");
      Console.WriteLine($"    Reason : {pii!.ReasonForCollecting}");
   }
}