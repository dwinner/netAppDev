// The true and false operators are overloaded in the extremely rare case of types that
// are boolean “in spirit”, but do not have a conversion to bool.

// An example is the System.Data.SqlTypes.SqlBoolean type which is defined as follows:

using BoolOverloading;

var a = SqlBoolean.Null;
if (a)
{
   Console.WriteLine("True");
}
else if (!a)
{
   Console.WriteLine("False");
}
else
{
   Console.WriteLine("Null");
}