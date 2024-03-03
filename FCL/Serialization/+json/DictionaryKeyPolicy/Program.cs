#nullable disable
using System.Text.Json;

namespace DictionaryKeyPolicy;

internal static class Program
{
   private static void Main()
   {
      var dict = new Dictionary<string, string>
      {
         { "ProgramVersion", "1.2" },
         { "PackageName", "Nutshell" }
      };

      Console.WriteLine(JsonSerializer.Serialize(dict,
         new JsonSerializerOptions
         {
            WriteIndented = true,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
         }));
   }
}