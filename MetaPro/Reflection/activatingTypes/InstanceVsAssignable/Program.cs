using System.Diagnostics;

object obj = Guid.NewGuid();
var target = typeof(IFormattable);

var isTrue = obj is IFormattable; // Static C# operator
var alsoTrue = target.IsInstanceOfType(obj); // Dynamic equivalent

Debug.Assert(isTrue);
Debug.Assert(alsoTrue);

Type target2 = typeof(IComparable), source = typeof(string);
Console.WriteLine(target2.IsAssignableFrom(source)); // True
Console.WriteLine(target2.IsSubclassOf(source)); // False