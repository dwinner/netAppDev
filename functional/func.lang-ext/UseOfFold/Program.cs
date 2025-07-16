﻿using System;
using LanguageExt;
using LanguageExt.DataTypes.Serialisation;

Either<int, string> intOrString = "start";

// A state (InitialResult) changes over time, and it changes using results of the previous change....
// It uses a new item extracted from the array in changing the state each time.
// The state changes the number of elements in the either - there will only be one - Left or right type.
// the state will change once based on the one value in either.
// For a List which has multiple items in it, the state will change that many times
var result = intOrString.Fold(
   "InitialState",
   (prev, extract) => ChangeState(extract, prev)
);

// The result is the last state change
Console.WriteLine($"The result value is {result}");
return;

string ChangeState(EitherData<int, string> extracted, string previousResult)
{
   var content = extracted.State == EitherStatus.IsLeft
      ? $"{extracted.Left}"
      : $"{extracted.Right}";
   var newResult = $"{previousResult} and {content}";
   return newResult;
}