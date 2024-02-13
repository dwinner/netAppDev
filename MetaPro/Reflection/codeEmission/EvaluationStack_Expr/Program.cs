using System.Reflection.Emit;

var dynMeth = new DynamicMethod("Foo", null, null, typeof(void));
var gen = dynMeth.GetILGenerator();
var writeLineInt = typeof(Console).GetMethod("WriteLine", new[] { typeof(int) });

// Calculate 10 / 2 + 1:

gen.Emit(OpCodes.Ldc_I4, 10);
gen.Emit(OpCodes.Ldc_I4, 2);
gen.Emit(OpCodes.Div);
gen.Emit(OpCodes.Ldc_I4, 1);
gen.Emit(OpCodes.Add);
gen.Emit(OpCodes.Call, writeLineInt);

// Here's another way to do the same thing:

gen.Emit(OpCodes.Ldc_I4, 1);
gen.Emit(OpCodes.Ldc_I4, 10);
gen.Emit(OpCodes.Ldc_I4, 2);
gen.Emit(OpCodes.Div);
gen.Emit(OpCodes.Add);
gen.Emit(OpCodes.Call, writeLineInt);


gen.Emit(OpCodes.Ret);
dynMeth.Invoke(null, null);