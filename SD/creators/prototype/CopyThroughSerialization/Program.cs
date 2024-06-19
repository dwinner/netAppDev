using CopyThroughSerialization;
using static System.Console;

var foo = new Foo { Stuff = 42, Whatever = "abc" };

//Foo foo2 = foo.DeepCopy(); // crashes without [Serializable]
var foo2 = foo.DeepCopyXml();

if (foo2 != null)
{
   foo2.Whatever = "xyz";
   WriteLine(foo);
   WriteLine(foo2);
}