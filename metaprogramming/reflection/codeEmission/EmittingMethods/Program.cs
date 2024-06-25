// public static double SquareRoot (double value) => Math.Sqrt (value);

using System.Reflection;
using System.Reflection.Emit;

var aname = new AssemblyName("MyEmissions");
var assemBuilder = AssemblyBuilder.DefineDynamicAssembly(aname, AssemblyBuilderAccess.Run);
var modBuilder = assemBuilder.DefineDynamicModule("MainModule");
var tb = modBuilder.DefineType("Widget", TypeAttributes.Public);
var mb = tb.DefineMethod("SquareRoot",
   MethodAttributes.Static | MethodAttributes.Public,
   CallingConventions.Standard,
   typeof(double), // Return type
   new[] { typeof(double) }); // Parameter types

mb.DefineParameter(1, ParameterAttributes.None, "value"); // Assign name

var gen = mb.GetILGenerator();
gen.Emit(OpCodes.Ldarg_0); // Load 1st arg
gen.Emit(OpCodes.Call, typeof(Math).GetMethod("Sqrt"));
gen.Emit(OpCodes.Ret);

var realType = tb.CreateType();
var x = (double)tb.GetMethod("SquareRoot").Invoke(null, new object[] { 10.0 });
Console.WriteLine(x); // 3.16227766016838

// LINQPad can disassemble methods for you:
var methodInfo = tb.GetMethod("SquareRoot");
Console.WriteLine(methodInfo.Name);