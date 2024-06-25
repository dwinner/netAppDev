// public static T Echo<T> (T value)
// {
//   return value;
// }

using System.Reflection;
using System.Reflection.Emit;

var aname = new AssemblyName("MyEmissions");
var assemBuilder = AssemblyBuilder.DefineDynamicAssembly(aname, AssemblyBuilderAccess.Run);
var modBuilder = assemBuilder.DefineDynamicModule("MainModule");
var tb = modBuilder.DefineType("Widget", TypeAttributes.Public);
var mb = tb.DefineMethod("Echo", MethodAttributes.Public | MethodAttributes.Static);
var genericParams = mb.DefineGenericParameters("T");

mb.SetSignature(genericParams[0], // Return type
   null, null,
   genericParams, // Parameter types
   null, null);

mb.DefineParameter(1, ParameterAttributes.None, "value"); // Optional
var gen = mb.GetILGenerator();
gen.Emit(OpCodes.Ldarg_0);
gen.Emit(OpCodes.Ret);