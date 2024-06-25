// class Widget
// {
//   int _capacity = 4000;
// }

using System.Reflection;
using System.Reflection.Emit;

var aname = new AssemblyName("MyEmissions");
var assemBuilder = AssemblyBuilder.DefineDynamicAssembly(aname, AssemblyBuilderAccess.Run);
var modBuilder = assemBuilder.DefineDynamicModule("MainModule");
var tb = modBuilder.DefineType("Widget", TypeAttributes.Public);
var field = tb.DefineField("_capacity", typeof(int), FieldAttributes.Private);
var ctorBuilder = tb.DefineConstructor(
   MethodAttributes.Public,
   CallingConventions.Standard,
   Type.EmptyTypes); // Constructor parameters

var gen = ctorBuilder.GetILGenerator();
gen.Emit(OpCodes.Ldarg_0); // Load "this" onto eval stack
gen.Emit(OpCodes.Ldc_I4, 4000); // Load 4000 onto eval stack
gen.Emit(OpCodes.Stfld, field); // Store it to our field
gen.Emit(OpCodes.Ret);

var t = tb.CreateType();
var ctorInfo = t.GetConstructors().Single();
Console.WriteLine(ctorInfo);