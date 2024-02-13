//int x = 6;
//int y = 7;
//x *= y;
//Console.WriteLine (x);

using System.Reflection.Emit;

var dynMeth = new DynamicMethod("Test", null, null, typeof(void));
var gen = dynMeth.GetILGenerator();

var localX = gen.DeclareLocal(typeof(int)); // Declare x
var localY = gen.DeclareLocal(typeof(int)); // Declare y

gen.Emit(OpCodes.Ldc_I4, 6); // Push literal 6 onto eval stack
gen.Emit(OpCodes.Stloc, localX); // Store in localX
gen.Emit(OpCodes.Ldc_I4, 7); // Push literal 7 onto eval stack
gen.Emit(OpCodes.Stloc, localY); // Store in localY

gen.Emit(OpCodes.Ldloc, localX); // Push localX onto eval stack
gen.Emit(OpCodes.Ldloc, localY); // Push localY onto eval stack
gen.Emit(OpCodes.Mul); // Multiply values together
gen.Emit(OpCodes.Stloc, localX); // Store the result to localX

gen.EmitWriteLine(localX); // Write the value of localX
gen.Emit(OpCodes.Ret);

dynMeth.Invoke(null, null); // 42