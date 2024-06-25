object[] oArgs = { "23", 0 };
Type[] argTypes = { typeof(string), typeof(int).MakeByRefType() };
var tryParse = typeof(int).GetMethod("TryParse", argTypes);
var successfulParse = (bool)tryParse.Invoke(null, oArgs);

Console.WriteLine($"{successfulParse} {oArgs[1]}"); // True 23