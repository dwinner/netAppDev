using System.Reflection.Emit;

var dynMeth = new DynamicMethod("Foo", null, null, typeof(void));
var gen = dynMeth.GetILGenerator();
var writeLineInt = typeof(Console).GetMethod("WriteLine", new[] { typeof(int) });

// Calculate 2 + 2

gen.Emit(OpCodes.Ldc_I4, 2); // Push a 4-byte integer, value=2
gen.Emit(OpCodes.Ldc_I4, 2); // Push a 4-byte integer, value=2
gen.Emit(OpCodes.Add); // Add the result together
gen.Emit(OpCodes.Call, writeLineInt);

gen.Emit(OpCodes.Ret);
dynMeth.Invoke(null, null);