// public class Widget<T>
// {
//   public T Value;
// }

using System.Reflection;
using System.Reflection.Emit;

var aname = new AssemblyName("MyEmissions");
var assemBuilder = AssemblyBuilder.DefineDynamicAssembly(aname, AssemblyBuilderAccess.Run);
var modBuilder = assemBuilder.DefineDynamicModule("MainModule");
var tb = modBuilder.DefineType("Widget", TypeAttributes.Public);
var genericParams = tb.DefineGenericParameters("T");
tb.DefineField("Value", genericParams[0], FieldAttributes.Public);