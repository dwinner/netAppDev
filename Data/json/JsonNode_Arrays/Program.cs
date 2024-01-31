using System.Diagnostics;
using System.Text.Json.Nodes;

var node = JsonNode.Parse("[1, 2, 3, 4, 5]");
Debug.Assert(node is JsonArray);
Console.WriteLine(node);

var jsonArray = node.AsArray();
var count = jsonArray.Count;
Console.WriteLine(count);

foreach (var child in jsonArray)
{
   var jsonString = child?.ToJsonString() ?? string.Empty;
   Console.WriteLine(jsonString);
}

// Reach directly in to first element via indexer:
Console.WriteLine((int)node[0]); // 1