const double d = 3.9;
var i = Convert.ToInt32(d);
Console.WriteLine(i);

var thirty = Convert.ToInt32("1E", 16); // Parse in hexadecimal
var five = Convert.ToUInt32("101", 2); // Parse in binary

Console.WriteLine(thirty);
Console.WriteLine(five);

// Dynamic conversions:

var targetType = typeof(int);
object source = "42";

var result = Convert.ChangeType(source, targetType);

Console.WriteLine(result); // 42
Console.WriteLine(result.GetType()); // System.Int32

// Base-64 conversions:

var base64String = Convert.ToBase64String(new byte[] { 123, 5, 33, 210 });
Console.WriteLine(base64String);

var bytes = Convert.FromBase64String("ewUh0g==");
Array.ForEach(bytes, @byte => Console.WriteLine(@byte));