#nullable disable

using System.Text.Json;

namespace PropertNamePolicy;

internal static class Program
{
   private static void Main()
   {
      var dylan = new Person { Name = "Dylan" };

      var json = JsonSerializer.Serialize(dylan,
         new JsonSerializerOptions
         {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
         });

      var dylan2 = JsonSerializer.Deserialize<Person>(json,
         new JsonSerializerOptions
         {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
         });

      Console.WriteLine(dylan);
      Console.WriteLine(json);
      Console.WriteLine(dylan2);
   }
}

internal class Person
{
   public string Name { get; set; }

   public override string ToString() => $"{nameof(Name)}: {Name}";
}