// string _text;
// public string Text
// {
//   get => _text;
//   internal set => _text = value;
// }

using System.Reflection;
using System.Reflection.Emit;

var aname = new AssemblyName("MyEmissions");
var assemBuilder = AssemblyBuilder.DefineDynamicAssembly(aname, AssemblyBuilderAccess.Run);
var modBuilder = assemBuilder.DefineDynamicModule("MainModule");
var tb = modBuilder.DefineType("Widget", TypeAttributes.Public);
var field = tb.DefineField("_text", typeof(string), FieldAttributes.Private);
var prop = tb.DefineProperty(
   "Text", // Name of property
   PropertyAttributes.None,
   typeof(string), // Property type
   Type.EmptyTypes); // Indexer types

var getter = tb.DefineMethod(
   "get_Text", // Method name
   MethodAttributes.Public | MethodAttributes.SpecialName,
   typeof(string), // Return type
   Type.EmptyTypes); // Parameter types

var getGen = getter.GetILGenerator();
getGen.Emit(OpCodes.Ldarg_0); // Load "this" onto eval stack
getGen.Emit(OpCodes.Ldfld, field); // Load field value onto eval stack
getGen.Emit(OpCodes.Ret); // Return

var setter = tb.DefineMethod(
   "set_Text",
   MethodAttributes.Assembly | MethodAttributes.SpecialName,
   null, // Return type
   new[] { typeof(string) }); // Parameter types

var setGen = setter.GetILGenerator();
setGen.Emit(OpCodes.Ldarg_0); // Load "this" onto eval stack
setGen.Emit(OpCodes.Ldarg_1); // Load 2nd arg, i.e., value
setGen.Emit(OpCodes.Stfld, field); // Store value into field
setGen.Emit(OpCodes.Ret); // return

prop.SetGetMethod(getter); // Link the get method and property
prop.SetSetMethod(setter); // Link the set method and property

// Test it:

var t = tb.CreateType();
var o = Activator.CreateInstance(t);
t.GetProperty("Text").SetValue(o, "Good emissions!", Array.Empty<object>());
var text = (string)t.GetProperty("Text").GetValue(o, null);

Console.WriteLine(text); // Good emissions!