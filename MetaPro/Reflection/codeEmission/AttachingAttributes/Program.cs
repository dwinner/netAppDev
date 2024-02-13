// [XmlElement ("FirstName", Namespace="http://test/", Order=3)]

using System.Reflection;
using System.Reflection.Emit;
using System.Xml.Serialization;

var aname = new AssemblyName("MyEmissions");
var assemBuilder = AssemblyBuilder.DefineDynamicAssembly(aname, AssemblyBuilderAccess.Run);
var modBuilder = assemBuilder.DefineDynamicModule("MainModule");
var tb = modBuilder.DefineType("Widget", TypeAttributes.Public);
var attType = typeof(XmlElementAttribute);
var attConstructor = attType.GetConstructor(new[] { typeof(string) });

var att = new CustomAttributeBuilder(
   attConstructor, // Constructor
   new object[] { "FirstName" }, // Constructor arguments
   new[]
   {
      attType.GetProperty("Namespace"), // Properties
      attType.GetProperty("Order")
   },
   new object[] { "http://test/", 3 } // Property values
);

var myFieldBuilder = tb.DefineField("SomeField", typeof(string), FieldAttributes.Public);

myFieldBuilder.SetCustomAttribute(att);
// or propBuilder.SetCustomAttribute (att);
// or typeBuilder.SetCustomAttribute (att);  etc

var t = tb.CreateType();
foreach (var attr in t.GetField("SomeField").GetCustomAttributes())
{
   Console.WriteLine(attr);
}