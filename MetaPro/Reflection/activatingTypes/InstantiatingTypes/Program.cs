using System.Text;

var i = (int)(Activator.CreateInstance(typeof(int)) ?? default(int));
Console.WriteLine(i);

var dt = (DateTime)(Activator.CreateInstance(typeof(DateTime), 2000, 1, 1) ?? default(DateTime));
Console.WriteLine(dt);

// Fetch the constructor that accepts a single parameter of type string:
var ci = typeof(X).GetConstructor(new[] { typeof(string) });

// Construct the object using that overload, passing in null:
var foo = ci.Invoke(new object[] { null });
Console.WriteLine(foo);

internal class X
{
   public X(string s)
   {
   }

   public X(StringBuilder sb)
   {
   }
}