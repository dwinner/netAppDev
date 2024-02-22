{
   Span<int> x =  [1, 2, 3, 4];
   Span<int> y = new int[4];
   x.CopyTo(y);
   foreach (var item in y)
   {
      Console.WriteLine(item);
   }
}

{
   Span<int> x =  [1, 2, 3, 4];
   Span<int> y =  [10, 20, 30, 40];
   x[..2].CopyTo(y[2..]); // y is now { 10, 20, 1, 2 }

   foreach (var item in y)
   {
      Console.WriteLine(item);
   }
}