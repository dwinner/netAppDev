using System.Reflection;
using System.Reflection.Emit;

var aname = new AssemblyName("MyDynamicAssembly");
var assemBuilder = AssemblyBuilder.DefineDynamicAssembly(aname, AssemblyBuilderAccess.Run);
var modBuilder = assemBuilder.DefineDynamicModule("DynModule");
var tb = modBuilder.DefineType("Widget", TypeAttributes.Public);

var methBuilder = tb.DefineMethod("SayHello",
   MethodAttributes.Public,
   null, null);
var gen = methBuilder.GetILGenerator();
gen.EmitWriteLine("Hello world");
gen.Emit(OpCodes.Ret);

// Create the type, finalizing its definition:
var t = tb.CreateType();

// Once the type is created, we use ordinary reflection to inspect
// and perform dynamic binding:
var o = Activator.CreateInstance(t);
t.GetMethod("SayHello").Invoke(o, null); // Hello world