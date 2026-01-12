using System;
using LanguageExt;

// put it in left state by default (integer)
Either<int, string> intOrString = 45;

var didFunctionRunForRight = false;
var didFunctionRunForLeft = false;

// extracts the right content and if it is right,
// it runs this non-transforming ie void returning function on it.
// The function won't run it its as left type
// So only runs the function if its right type
intOrString.Iter(RunAFunctionOnRightContents);

// both actions are void returning (Unit represents a typed void result)
var ret = intOrString.BiIter(
   RunAFunctionOnRightContents,
   _ => didFunctionRunForLeft = true
);

Console.WriteLine($"Did function run on right contents? {didFunctionRunForRight}");
Console.WriteLine($"Did function run on left contents? {didFunctionRunForLeft}");
return;

void RunAFunctionOnRightContents(string str)
{
   Console.WriteLine($"Hello {str}");
   didFunctionRunForRight = true;
}