using System.Reflection.Emit;

var dynMeth = new DynamicMethod("Foo",
   typeof(int), // Return type = int
   new[] { typeof(int), typeof(int) }, // Parameter types = int, int
   typeof(void));

var gen = dynMeth.GetILGenerator();

gen.Emit(OpCodes.Ldarg_0); // Push first arg onto eval stack
gen.Emit(OpCodes.Ldarg_1); // Push second arg onto eval stack
gen.Emit(OpCodes.Add); // Add them together (result on stack)
gen.Emit(OpCodes.Ret); // Return with stack having 1 value

var result = (int)dynMeth.Invoke(null, new object[] { 3, 4 }); // 7
Console.WriteLine(result);

// If you need to invoke the method repeatedly, here's an optimized solution:
var func = (Func<int, int, int>)dynMeth.CreateDelegate(typeof(Func<int, int, int>));
result = func(3, 4); // 7
Console.WriteLine(result);