#nullable disable
using System.Text.Json;

namespace SerializingCollections;

internal static class Program
{
   private static void Main()
   {
      var sara = new Person { Name = "Sara" };
      var ian = new Person { Name = "Ian" };

      var json = JsonSerializer.Serialize(new[] { sara, ian },
         new JsonSerializerOptions { WriteIndented = true });

      var people = JsonSerializer.Deserialize<Person[]>(json);
      Array.ForEach(people, Console.WriteLine);
   }
}

public class Person
{
   public string Name { get; set; }

   public override string ToString() => $"{nameof(Name)}: {Name}";
}