﻿using System;

for (var i = 0; i < 10; i++)
{
   Console.WriteLine(i);
} // i goes out of scope here

// We can declare a variable named i again, because
// there's no other variable with that name in scope
for (var i = 9; i >= 0; i--)
{
   Console.WriteLine(i);
} // i goes out of scope here.