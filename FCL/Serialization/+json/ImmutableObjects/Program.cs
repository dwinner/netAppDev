#nullable disable
using System.Text.Json;

namespace ImmutableObjects;

internal static class Program
{
   private static void Main()
   {
      var p = new Person("Joe", "Bloggs");
      var json = JsonSerializer.Serialize(p);
      var p2 = JsonSerializer.Deserialize<Person>(json);
      Console.WriteLine(json);
      Console.WriteLine(p2);
   }
}

public class Person(string firstName, string lastName)
{
   public string FirstName { get; } = firstName;
   public string LastName { get; } = lastName;

   public override string ToString() => $"{nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}";
}