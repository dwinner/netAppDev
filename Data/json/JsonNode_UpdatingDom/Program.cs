using System.Text.Json.Nodes;

var node = JsonNode.Parse("{ \"Color\": \"Red\" }");
node!["Color"] = "White";
node["Valid"] = true;
Console.WriteLine(node);

node.AsObject().Remove("Valid");
Console.WriteLine(node);

var arrayNode = JsonNode.Parse("[1, 2, 3]");
arrayNode!.AsArray().RemoveAt(0);
arrayNode.AsArray().Add(4);
Console.WriteLine(arrayNode);

// more examples
node = JsonNode.Parse(File.ReadAllText("PersonArray.json"));
var oldJson = node.ToString();
node[0]["Friends"].AsArray().Add("Amy"); // Give the first person another friend
node[1]["NewValue"] = 123.456; // Add a new simply-typed property to the second person 
node[2]["NewObject"] = new JsonObject // Add a new object-typed property to the third person
{
   ["X"] = 1,
   ["Y"] = 2
};

// Write the udpated DOM back to a JSON string:
var newJson = node.ToString();

Console.WriteLine(oldJson);
Console.WriteLine(newJson);