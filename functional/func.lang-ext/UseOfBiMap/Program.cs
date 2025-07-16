﻿using System;
using LanguageExt;

Either<int, string> intOrString = 45;

// This will extract the item and allow you to run a transformation like a Map,
// which automatically lifts the result of the transform. it will allow you to specify this
// transform function for both types that either can hold and the correct one will run
// depending on the actual type that the either holds at that moment in time.
var result = intOrString.BiMap(
   rightString => rightString.ToUpper(),
   leftInteger => leftInteger + 1024
);

Console.WriteLine($"The result is {result}");

intOrString = "Stuart";
result = intOrString.BiMap(
   rightString => rightString.ToUpper(),
   leftInteger => leftInteger + 1024
);


Console.WriteLine($"The result is {result}");