using System.Text.Encodings.Web;
using System.Text.Json;

namespace UnsafeRelaxedJsonEscape;

internal static class Program
{
   private static void Main()
   {
      var dylan = "<b>Dylan & Friends</b>";
      var dylanDump = JsonSerializer.Serialize(dylan);
      Console.WriteLine(dylanDump);

      var deserialized = JsonSerializer.Serialize(dylan,
         new JsonSerializerOptions
         {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
         });
      Console.WriteLine(deserialized);
   }
}