// This tutorial shows you what a what an Either<>'s BiBind() functionality

using System;
using LanguageExt;

const int number = 5;
const string word = "five";

Either<int, string> amount = word;

// Instead of only being able to run a transform on the right hand side only as bind() does,
// you can use BiBind() to prepare transform functions for whatever side, left or right type is assigned to it!
// The result is the result of whichever bind run,
// depending on which state the either is in (left=has integer)(right=has string)
var resultOfTransform = amount.BiBind(
   TransformExtractedRight,
   TransformExtractedLeft
);

Console.WriteLine($"the result of the transform is {resultOfTransform}");

amount = number;

resultOfTransform = amount.BiBind(
   TransformExtractedRight,
   TransformExtractedLeft
);
Console.WriteLine($"the result of the transform is {resultOfTransform}");
return;


Either<int, string> TransformExtractedRight(string rightString)
{
   Either<int, string> ret = $"{rightString} is a word, i think";
   return ret;
}

Either<int, string> TransformExtractedLeft(int leftInteger)
{
   Either<int, string> ret = leftInteger + 1;
   return ret;
}