using System;
using System.Collections.Generic;
using LanguageExt;

Either<int, string> intOrString1 = "Stuart";
Either<int, string> intOrString2 = "Jenny";
Either<int, string> intOrString3 = "Bruce";
Either<int, string> intOrString4 = "Bruce";
Either<int, string> intOrString5 = 66;

IEnumerable<Either<int, string>> listOfEithers = [intOrString1, intOrString2, intOrString3, intOrString4, intOrString5];

// instead of calling Lefts() and Rights(), you can get them all in one go as a tuple
var (lefts, rights) = listOfEithers.Partition();

foreach (var left in lefts)
{
   Console.WriteLine($"Left: {left}");
}

foreach (var right in rights)
{
   Console.WriteLine($"Right: {right}");
}