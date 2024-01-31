using System.Text.Json.Nodes;

var node = JsonNode.Parse(File.ReadAllText("PersonArray.json"));

var query =
   from person in node?.AsArray()
   select new
   {
      FirstName = (string)person["FirstName"],
      Age = (int)person["Age"],
      Friends =
         from friend in person["Friends"].AsArray()
         select (string)friend
   };

foreach (var qItem in query)
{
   Console.WriteLine(qItem.Age);
   Console.WriteLine(qItem.FirstName);
   foreach (var friend in qItem.Friends)
   {
      Console.WriteLine($"\t{friend}");
   }
}