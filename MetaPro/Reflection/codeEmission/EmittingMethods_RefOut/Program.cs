// public static void SquareRoot (ref double value) => value = Math.Sqrt (value);

using System.Reflection;
using System.Reflection.Emit;

var aname = new AssemblyName("MyEmissions");
var assemBuilder = AssemblyBuilder.DefineDynamicAssembly(aname, AssemblyBuilderAccess.Run);
var modBuilder = assemBuilder.DefineDynamicModule("MainModule");
var tb = modBuilder.DefineType("Widget", TypeAttributes.Public);
var mb = tb.DefineMethod("SquareRoot",
   MethodAttributes.Static | MethodAttributes.Public,
   CallingConventions.Standard,
   null,
   new[] { typeof(double).MakeByRefType() });

// For an 'out' parameter, use ParameterAttributes.Out instead:
mb.DefineParameter(1, ParameterAttributes.None, "value");

var gen = mb.GetILGenerator();
gen.Emit(OpCodes.Ldarg_0);
gen.Emit(OpCodes.Ldarg_0);
gen.Emit(OpCodes.Ldind_R8);
gen.Emit(OpCodes.Call, typeof(Math).GetMethod("Sqrt"));
gen.Emit(OpCodes.Stind_R8);
gen.Emit(OpCodes.Ret);

var realType = tb.CreateType();
object[] oArgs = { 10.0 };
tb.GetMethod("SquareRoot").Invoke(null, oArgs);
Console.WriteLine(oArgs[0]); // 3.16227766016838