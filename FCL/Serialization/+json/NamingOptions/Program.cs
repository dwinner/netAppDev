#nullable disable
using System.Text.Json;

namespace NamingOptions;

internal static class Program
{
   private static void Main()
   {
      var json = """{ "name":"Dylan" }""";

      var dylan1 = JsonSerializer.Deserialize<Person>(json,
         new JsonSerializerOptions
         {
            WriteIndented = true
         });

      var dylan2 = JsonSerializer.Deserialize<Person>(json,
         new JsonSerializerOptions
         {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
         });

      Console.WriteLine(dylan1);
      Console.WriteLine(dylan2);
   }
}

internal class Person
{
   public string Name { get; set; }

   public override string ToString() => $"{nameof(Name)}: {Name}";
}