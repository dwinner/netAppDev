using System.Reflection.Emit;

var dynMeth = new DynamicMethod("Foo", null, null, typeof(void));
var gen = dynMeth.GetILGenerator();
var writeLineInt = typeof(Console).GetMethod("WriteLine", new[] { typeof(int) });

// The Ldc* op-codes load numeric literals of various types and sizes.

gen.Emit(OpCodes.Ldc_I4, 123); // Push a 4-byte integer onto stack
gen.Emit(OpCodes.Call, writeLineInt);

gen.Emit(OpCodes.Ret);
dynMeth.Invoke(null, null); // 123