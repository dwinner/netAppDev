using System.Text.Json;

Number();
Array();
Age();

return;

void Number()
{
   using var document = JsonDocument.Parse("123");
   var root = document.RootElement;
   Console.WriteLine(root.ValueKind); // Number

   var number = document.RootElement.GetInt32();
   Console.WriteLine(number); // 123
}

void Array()
{
   using var document = JsonDocument.Parse("[1, 2, 3, 4, 5]");
   var length = document.RootElement.GetArrayLength(); // 5
   var value = document.RootElement[3].GetInt32(); // 4

   Console.WriteLine($"length: {length}; value {value}");
}

void Age()
{
   using var document = JsonDocument.Parse(@"{ ""Age"": 32}");
   var root = document.RootElement;
   var age = root.GetProperty("Age").GetInt32();
   Console.WriteLine(age);

   // Discover Age property
   var ageProp = root.EnumerateObject().First();
   var name = ageProp.Name; // Age  
   var value = ageProp.Value;
   Console.WriteLine(value.ValueKind); // Number
   Console.WriteLine(value.GetInt32()); // 32
}