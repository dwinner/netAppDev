#nullable disable
using System.Text.Json;

namespace ReadCommentHandling;

internal static class Program
{
   private static void Main()
   {
      var json = """
                 
                 	{
                 		"Name":"Dylan" // Comment here
                 		/* Another comment here */
                 	}
                 """;

      var dylan = JsonSerializer.Deserialize<Person>(json,
         new JsonSerializerOptions
         {
            WriteIndented = true,
            ReadCommentHandling = JsonCommentHandling.Skip
         });

      Console.WriteLine(json);
      Console.WriteLine(dylan);
   }
}

internal class Person
{
   public string Name { get; set; }

   public override string ToString() => $"{nameof(Name)}: {Name}";
}