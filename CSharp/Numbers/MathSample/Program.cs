﻿using System;

// Call static members
var x = Math.GetSquareOf(5);
Console.WriteLine($"Square of 5 is {x}");

// Instantiate a Math object
Math math = new()
{
   // Call instance members
   Value = 30
};

Console.WriteLine($"Value field of math variable contains {math.Value}");
Console.WriteLine($"Square of 30 is {math.GetSquare()}");