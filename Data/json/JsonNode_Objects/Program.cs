using System.Diagnostics;
using System.Text.Json.Nodes;

var node = JsonNode.Parse("""
                          {
                           "Name":"Alice",
                           "Age": 32
                          }
                          """);
Debug.Assert(node is JsonObject);

var name = (string)node["Name"]!; // Alice
var age = (int)node["Age"]!; // 32

Console.WriteLine(new { name, age });

// Enumerate over the dictionary’s key/value pairs:
foreach (var (propertyName, propertyValue) in node.AsObject())
{
   Console.WriteLine(new { propertyName, propertyValue });
}

if (node.AsObject().TryGetPropertyValue("Name", out var nameNode))
{
   Console.WriteLine(nameNode);
}