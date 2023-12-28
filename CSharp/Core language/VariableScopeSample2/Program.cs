using System;

var j = 20;
for (var i = 0; i < 10; i++)
{
   // Can't do this — j is still in scope
   // var j = 30; 
   Console.WriteLine(j + i);
}