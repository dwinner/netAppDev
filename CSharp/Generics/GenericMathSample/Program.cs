// .NET 7 introduced the INumber<TSelf> interface to unify arithmetic operations across 
// numeric types, allowing generic methods such as the following to be written:

using System.Numerics;

var intSum = Sum(3, 5, 7);
var doubleSum = Sum(3.2, 5.3, 7.1);
var decimalSum = Sum(3.2m, 5.3m, 7.1m);

Console.WriteLine(intSum);
Console.WriteLine(doubleSum);
Console.WriteLine(decimalSum);

// The System.Numerics namespace also contains interfaces that are not part of INumber for
// operations specific to certain kinds of numbers (such as floating-point). To compute a
// root-mean-square, for instance, we can add the IRootFunctions<T> interface to the 
// constraint list to expose its static RootN method to T:

var rms1 = Rms(5f, 10f, 20f);
var rms2 = Rms(5d, 10d, 20d);

Console.WriteLine(rms1);
Console.WriteLine(rms2);

return;

T Rms<T>(params T[] values)
   where T : INumber<T>, IRootFunctions<T>
{
   var total = T.Zero;
   for (var i = 0; i < values.Length; i++)
   {
      total += values[i] * values[i];
   }

   // Use T.CreateChecked to convert values.Length (type int) to T.
   var count = T.CreateChecked(values.Length);

   return T.RootN(total / count, 2); // Calculate square root
}

T Sum<T>(params T[] numbers) where T : INumber<T>
{
   var total = T.Zero;
   foreach (var n in numbers)
   {
      total += n; // Invokes addition operator for any numeric type
   }

   return total;
}