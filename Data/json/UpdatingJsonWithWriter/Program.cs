using System.Text.Json;

var personsFile = "PersonArray.json";
var personArrayJson = File.ReadAllText(personsFile);
using var document = JsonDocument.Parse(personArrayJson);

var options = new JsonWriterOptions { Indented = true };

using (var stream = File.Create("MyFile.json"))
using (var writer = new Utf8JsonWriter(stream, options))
{
   writer.WriteStartArray();
   foreach (var person in
            from person in document.RootElement.EnumerateArray()
            let friendCount = person.GetProperty("Friends").GetArrayLength()
            where friendCount >= 2
            select person)
   {
      person.WriteTo(writer);
   }
}

var updated = File.ReadAllText("MyFile.json");
Console.WriteLine(updated);