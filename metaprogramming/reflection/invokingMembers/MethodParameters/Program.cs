var type = typeof(string);
Type[] parameterTypes = { typeof(int) };
var method = type.GetMethod("Substring", parameterTypes);

object[] arguments = { 2 };
var returnValue = method.Invoke("stamp", arguments);
Console.WriteLine(returnValue); // "amp"

var paramList = method.GetParameters();
foreach (var x in paramList)
{
   Console.WriteLine(x.Name); // startIndex
   Console.WriteLine(x.ParameterType); // System.Int32
}