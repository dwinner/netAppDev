#nullable disable
using System.Text.Json;

namespace SerializingCollections_DiffTypes;

internal static class Program
{
   private static void Main()
   {
      var sara = new Person { Name = "Sara" };
      var addr = new Address { Street = "1 Main St.", PostCode = "11235" };

      string json = JsonSerializer.Serialize(new object[] { sara, addr },
         new JsonSerializerOptions { WriteIndented = true });
      Console.WriteLine(json);

      var deserialized = JsonSerializer.Deserialize<JsonElement[]>(json);
      foreach (var element in deserialized)
      {
         foreach (var prop in element.EnumerateObject())
         {
            Console.WriteLine($"{prop.Name}: {prop.Value}");
         }

         Console.WriteLine("---");
      }
   }
}

public class Person
{
   public string Name { get; set; }

   public override string ToString() => $"{nameof(Name)}: {Name}";
}

public class Address
{
   public string Street { get; set; }

   public string PostCode { get; set; }

   public override string ToString() => $"{nameof(Street)}: {Street}, {nameof(PostCode)}: {PostCode}";
}