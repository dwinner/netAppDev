using System;
using System.Collections.Generic;
using LanguageExt;

Either<int, string> intOrString1 = "Stuart";
Either<int, string> intOrString2 = "Jenny";
Either<int, string> intOrString3 = "Bruce";
Either<int, string> intOrString4 = "Bruce";
Either<int, string> intOrString5 = 66;

IEnumerable<Either<int, string>> listOfEithers = [intOrString1, intOrString2, intOrString3, intOrString4, intOrString5];

// Extract right values from the eithers in the list and run this function to get them.
// note if there is no right value for the either being inspected, this function is not run
// ie skips 66
listOfEithers.IterT(rightString => Console.WriteLine($"{rightString}"));