var buffer = new Buffer
{
   [^1] = 5,
   [^3] = 3,
   [0] = 42 // Option<int>.Some(42);
};

for (var i = 0; i < 10; i++)
{
   var opt = buffer[i];
   switch (opt.IsSome)
   {
      case true:
         Console.WriteLine((int)opt);
         break;
      default:
         Console.WriteLine($"No value at {i}");
         break;
   }
}