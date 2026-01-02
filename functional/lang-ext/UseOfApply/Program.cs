using System;
using System.Collections.Generic;
using LanguageExt;

Either<int, string> intOrString1 = "Stuart";
Either<int, string> intOrString2 = "Jenny";
Either<int, string> intOrString3 = "Bruce";
Either<int, string> intOrString4 = "Bruce";
Either<int, string> intOrString5 = 66;

// basically pass yourself ie your current value to the function provided.
// Ie apply allows you to transform yourself (makes a copy of the result, not an in-place modification)
var resultA = intOrString5.Apply(UseThis);

// the function can transform the type of the either
var resultB = intOrString5.Apply(UseThisAndChangeType);

Console.WriteLine($"ResultA = {resultA}, ResultB = {resultB}");

IEnumerable<Either<int, string>> listOfEithers = [intOrString1, intOrString2, intOrString3, intOrString4, intOrString5];

// So in the same way, give your self ie your content (which is a List of Eithers) to the provided function
var result = listOfEithers.Apply(UseThisListOfEithers);

Console.WriteLine($"The result of is '{result}'");
return;

Either<int, string> UseThis(Either<int, string> useThis)
{
   var str = useThis.Match(
      rightString => rightString,
      _ => "was left"
   );
   Either<int, string> either = str;
   return either;
}

Either<int, char> UseThisAndChangeType(Either<int, string> useThis)
{
   var str = useThis.Match(_ => 'T', _ => 'F');
   Either<int, char> either = str;
   return either;
}

IEnumerable<Either<int, string>> UseThisListOfEithers(IEnumerable<Either<int, string>> useMe) =>
   // Transform the things in whatever state (type ie left or right) they are in
   useMe.BiMapT(
      rightString => rightString,
      leftInteger => leftInteger * 2
   );