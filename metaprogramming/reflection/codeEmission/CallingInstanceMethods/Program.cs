using System.Reflection.Emit;
using System.Text;

var dynMeth = new DynamicMethod("Test", null, null, typeof(void));
var gen = dynMeth.GetILGenerator();
var ctorInfo = typeof(StringBuilder).GetConstructor(Type.EmptyTypes);
gen.Emit(OpCodes.Newobj, ctorInfo);
gen.Emit(OpCodes.Callvirt, typeof(StringBuilder).GetProperty("MaxCapacity").GetGetMethod());
gen.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new[] { typeof(int) }));
gen.Emit(OpCodes.Ret);

dynMeth.Invoke(null, null); // 2147483647