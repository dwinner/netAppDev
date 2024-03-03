#nullable disable
using System.Text.Json;

namespace IgnoreNullValues;

internal static class Program
{
   private static void Main()
   {
      var dylan = new Person { Name = null };
      var defDump = JsonSerializer.Serialize(dylan, new JsonSerializerOptions { WriteIndented = true });
      var serialized = JsonSerializer.Serialize(dylan, new JsonSerializerOptions
      {
         WriteIndented = true,
         IgnoreNullValues = true
      });

      Console.WriteLine(defDump);
      Console.WriteLine(serialized);
   }
}

internal class Person
{
   public string Name { get; set; }

   public int Age
   {
      get
      {
         var age = DateTime.Today.Year - Birthdate.Year;
         if (Birthdate.Date > DateTime.Today.AddYears(-age))
         {
            age--;
         }

         return age;
      }
   }

   public DateTime Birthdate { get; set; }
}