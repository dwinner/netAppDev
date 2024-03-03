#nullable disable
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ChildObjects_Preserving;

internal static class Program
{
   private static void Main()
   {
      var home = new Address { Street = "1 Main St.", PostCode = "11235" };

      var p = new Person { Name = "Ian", HomeAddress = home, WorkAddress = home };

      var options = new JsonSerializerOptions
      {
         WriteIndented = true,
         ReferenceHandler = ReferenceHandler.Preserve
      };

      var json = JsonSerializer.Serialize(p, options);
      Console.WriteLine(json);

      var p2 = (Person)JsonSerializer.Deserialize(json, typeof(Person), options);
      Debug.Assert(p2.HomeAddress == p2.WorkAddress);
   }
}

public class Address
{
   public string Street { get; set; }
   public string PostCode { get; set; }
}

public class Person
{
   public string Name { get; set; }
   public Address HomeAddress { get; set; }
   public Address WorkAddress { get; set; }
}