﻿using System.Runtime.CompilerServices;

Print(Math.PI * 2);
Print(Math.PI /*(π)*/ * 2);

void Print(double number,
   [CallerArgumentExpression("number")] string expr = null)
   => Console.WriteLine(expr);

Assert(2 + 2 == 5);

return;

void Assert(bool condition,
   [CallerArgumentExpression(nameof(condition))]
   string? message = null)
{
   if (!condition)
   {
      throw new Exception($"Assertion failed: {message}");
   }
}