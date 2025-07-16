using System;
using LanguageExt;

Either<int, string> intOrString = "Stuart";

// Transform the left hand side, remember bind will not automatically lift the result of that transformation,
// so your transformation function will need to do that.
// We call BindLeft because by default Either is right biased so default Bind() only transforms the right
// side if there is one (there isn't as we've assigned a right value of string 'Stuart'
var result = intOrString.BindLeft(TransformLeft);

// Note the transformation did not occur because either contained a right type ie string
Console.WriteLine($"result is {result}");

intOrString = 55;

// Note the transformation should now occur because either contained a left type,
// and we've defined a transformation for that on this either
result = intOrString.BindLeft(TransformLeft);

Console.WriteLine($"result is {result}");
return;

Either<int, string> TransformLeft(int left)
{
   Either<int, string> transformedResult = left + 22;
   return transformedResult;
}