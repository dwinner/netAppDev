using System.Text.Json.Nodes;

var node = new JsonArray
{
   new JsonObject
   {
      ["Name"] = "Tracy",
      ["Age"] = 30,
      ["Friends"] = new JsonArray("Lisa", "Joe")
   },
   new JsonObject
   {
      ["Name"] = "Jordyn",
      ["Age"] = 25,
      ["Friends"] = new JsonArray("Tracy", "Li")
   }
};

Console.WriteLine(node);