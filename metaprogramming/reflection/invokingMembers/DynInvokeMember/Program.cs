object s = "Hello";
var prop = s.GetType().GetProperty("Length");
var length = (int)prop.GetValue(s, null); // 5
Console.WriteLine(length);