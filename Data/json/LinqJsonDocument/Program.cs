using System.Text.Json;

const string personArrayPath = "PersonArray.json";
using var json = File.OpenRead(personArrayPath);
using var document = JsonDocument.Parse(json);

var query =
   from person in document.RootElement.EnumerateArray()
   select new
   {
      FirstName = person.GetProperty("FirstName").GetString(),
      Age = person.GetProperty("Age").GetInt32(),
      Friends =
         from friend in person.GetProperty("Friends").EnumerateArray()
         select friend.GetString()
   };

foreach (var qElement in query)
{
   Console.WriteLine(qElement.FirstName);
   Console.WriteLine(qElement.Age);
   foreach (var friend in qElement.Friends)
   {
      Console.WriteLine(friend);
   }
}