#nullable disable
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CtrlAttrSerialization;

internal static class Program
{
   private static void Main()
   {
      var p = new Person { Name = "Ian" };
      var json = JsonSerializer.Serialize(p,
         new JsonSerializerOptions { WriteIndented = true });
      Console.WriteLine(json);

      var p2 = JsonSerializer.Deserialize<Person>(json);
      Console.WriteLine(p2);
   }
}

public class Person
{
   [JsonPropertyName("FullName")]
   public string Name { get; set; }

   [JsonIgnore]
   public decimal NetWorth { get; set; } // Not serialized

   public override string ToString() => $"{nameof(Name)}: {Name}, {nameof(NetWorth)}: {NetWorth}";
}