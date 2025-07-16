﻿using System;

namespace ShortCircuiting;

// This tutorial shows how pipe-lining and the way Box's Map() and Bind() functions work, allow for short-circuiting through the validation step in (VETL - Validate, Extract, Transform and Lift)
// and hows why this is useful.
// More specifically it shows how short-circuiting works when you use the Linq Query syntax, and how the underlying implementation of Select() and SelectMany()'s validation 
// routines are still used
internal static class Program
{
   private static void Main(string[] args)
   {
      var aBoxOfNumbers1 = new Box<int[]>(new[] { 1, 2, 3, 4, 5 });
      var aBoxOfNumbers2 = new Box<int[]>(); // empty box
      var aBoxOfNumbers3 = new Box<int[]>(new[] { 6, 7, 8, 9, 10 });

      // The way Box's validation step works in its Bind/Map functions says thet an empty box is invalid.
      // Further more we cannot process any boxes if even one box is empty:

      var result = from number1 in aBoxOfNumbers1
         from number2 in
            aBoxOfNumbers2 // this result causes the next Bind() to check see that its an invalid input and itself returns empty box and this repeats until the result is deemed empty
         from number3 in
            aBoxOfNumbers3 // this does run, but it just bails out at the Validation phase in the bind()'s VETL stage
         select number3;


      Console.WriteLine($"The result is: {result}");
   }
}