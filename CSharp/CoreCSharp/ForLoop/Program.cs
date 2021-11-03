using System;

// This loop iterates through rows
for (var i = 0; i < 100; i += 10)
{
   // This loop iterates through columns
   for (var j = i; j < i + 10; j++)
   {
      Console.Write($"  {j}");
   }

   Console.WriteLine();
}