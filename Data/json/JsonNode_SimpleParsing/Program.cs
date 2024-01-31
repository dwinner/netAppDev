using System.Text.Json.Nodes;

var node = JsonNode.Parse("123"); // Parses to a JsonValue
Console.WriteLine(node is JsonValue);
Console.WriteLine(node);

Console.WriteLine(((JsonValue)node).GetValue<int>()); // Works, but clumsy!
Console.WriteLine(node.AsValue().GetValue<int>()); // Shortcut for above
Console.WriteLine(node.GetValue<int>()); // Better shortcut
Console.WriteLine((int)node); // Even better shortcut!

if (node.AsValue().TryGetValue<int>(out var number))
{
   Console.WriteLine("Parse succeeded");
}