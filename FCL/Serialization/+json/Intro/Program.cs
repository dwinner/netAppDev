#nullable disable
using System.Text.Json;

namespace Intro;

internal static class Program
{
   private static void Main()
   {
      var p = new Person { Name = "Ian" };
      var json = JsonSerializer.Serialize(p, new JsonSerializerOptions { WriteIndented = true });
      Console.WriteLine(json);

      var p2 = JsonSerializer.Deserialize<Person>(json);
      Console.WriteLine(p2);
   }
}

public class Person
{
   public string Name { get; set; }

   public override string ToString() => $"{nameof(Name)}: {Name}";
}