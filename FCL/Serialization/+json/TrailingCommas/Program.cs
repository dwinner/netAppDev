#nullable disable
using System.Text.Json;

namespace TrailingCommas;

internal static class Program
{
   private static void Main()
   {
      var dylan = new PersonV3
      {
         Name = "Dylan",
         LuckyNumbers = new List<int> { 10, 7 },
         Age = 46
      };

      var opts = new JsonSerializerOptions
      {
         WriteIndented = true
      };
      var json = JsonSerializer.Serialize(dylan, opts);
      Console.WriteLine(json);

      var brokenJson = json.Replace("7", "7,").Replace("46", "46,");
      Console.WriteLine(brokenJson);

      // Try to deserialize trailing commas without setting options

      try
      {
         var dylanBroken = JsonSerializer.Deserialize<PersonV3>(brokenJson);
      }
      catch (JsonException ex)
      {
         Console.WriteLine($"As expected, the JSON can't be parsed: {ex.Message}");
      }

      // Deserialize with option AllowTrailingCommas = true

      var dylanCommaTolerant = JsonSerializer.Deserialize<PersonV3>(brokenJson,
         new JsonSerializerOptions { AllowTrailingCommas = true });
      Console.WriteLine(dylanCommaTolerant);
   }
}

internal class PersonV3
{
   public string Name { get; set; }

   public List<int> LuckyNumbers { get; set; }

   public int Age { get; set; }

   public override string ToString() => $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
}