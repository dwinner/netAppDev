// public class Widget
// {
//   public static void Test() 
//   {
//     var list = new List<Widget>();
//   }
// }

using System.Reflection;
using System.Reflection.Emit;

var aname = new AssemblyName("MyEmissions");

var assemBuilder = AssemblyBuilder.DefineDynamicAssembly(
   aname, AssemblyBuilderAccess.Run);

var modBuilder = assemBuilder.DefineDynamicModule("MainModule");

var tb = modBuilder.DefineType("Widget", TypeAttributes.Public);

var mb = tb.DefineMethod("Test", MethodAttributes.Public |
                                 MethodAttributes.Static);
var gen = mb.GetILGenerator();

var variableType = typeof(List<>).MakeGenericType(tb);

var unbound = typeof(List<>).GetConstructor(Type.EmptyTypes);
var ci = TypeBuilder.GetConstructor(variableType, unbound);

var listVar = gen.DeclareLocal(variableType);
gen.Emit(OpCodes.Newobj, ci);
gen.Emit(OpCodes.Stloc, listVar);
gen.Emit(OpCodes.Ret);