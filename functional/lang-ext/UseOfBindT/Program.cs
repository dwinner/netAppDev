using System;
using System.Collections.Generic;
using LanguageExt;

Either<int, string> intOrString1 = "Stuart";
Either<int, string> intOrString2 = "Jenny";
Either<int, string> intOrString3 = "Bruce";
Either<int, string> intOrString4 = "Bruce";
Either<int, string> intOrString5 = 66;

IEnumerable<Either<int, string>> listOfEithers = [intOrString1, intOrString2, intOrString3, intOrString4, intOrString5];

// transform the right values (if they are there) for each either in the list
// As this is a bind, you need to lift the result in to Either
var transformedList = listOfEithers.BindT(TransformRight);

var newRights = transformedList.Rights();

/* note we don't care about the lefts,
  if we did we might you match to see what both left and right values would be
  if they are set on the eithers we are looking at - see Tutorial 22 */
foreach (var str in newRights)
{
   Console.WriteLine(str);
}

return;

Either<int, string> TransformRight(string rightString)
{
   Either<int, string> right = $"My name is '{rightString}'";
   return right;
}