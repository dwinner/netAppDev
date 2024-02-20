var locker = new object();
double grandTotal = 0;

Parallel.For(
   1,
   10_000_000,
   () => 0.0, // Initialize the local value.
   (i, _, localTotal) => // Body delegate. Notice that it
      localTotal + Math.Sqrt(i), // returns the new local total.
   localTotal => // Add the local value
   {
      lock (locker)
      {
         grandTotal += localTotal;
      }
   } // to the master value.
);

Console.WriteLine(grandTotal);