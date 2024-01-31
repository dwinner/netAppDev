using System.Text.Json.Nodes;

var node = JsonNode.Parse(File.ReadAllText("PersonArray.json"));
var li = (string?)node?[1]?["Friends"]?[2] ?? string.Empty;
Console.WriteLine(li);