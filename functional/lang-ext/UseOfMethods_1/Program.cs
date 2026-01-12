// See https://aka.ms/new-console-template for more information

using UseOfMethods_1;

// A Box
var boxOfIntegers = new Box<int[]>(new[] { 3, 5, 7, 9, 11, 13, 15 });

// Do something with or to the Box
var doubled1 = DoubleBox1(boxOfIntegers); // Use Bind
var doubled2 = DoubleBox2(boxOfIntegers); // use Map
var doubled3 = DoubleBox3(boxOfIntegers); // Use SelectMany via linq expression syntax
var doubled4 = DoubleBox4(boxOfIntegers); // Use Select via linq expression syntax

return;

static Box<int[]> DoubleBox1(Box<int[]> boxOfIntegers)
{
   return boxOfIntegers.Bind(DoubleNumbers);
}

// transform Extracted, and Lift it
static Box<int[]> DoubleNumbers(int[] extract)
{
   // Remember a Select() run a function on the item in the box
   // Also note that the Select() function here is provided by the .NET runtime and it not the Select() we
   // wrote for the Box type, as extract is of type int[].
   // This version of select behaves similarly to what the Box's Select does - we just can't see it (it runs the user provided transform function)
   return new Box<int[]>(extract.Select(x => x * 2).ToArray());
}

static Box<int[]> DoubleBox2(Box<int[]> boxOfIntegers)
{
   // Use Map to automatically lift transformed result
   return boxOfIntegers.Map(DoubleNumbersNoLift);
}

static Box<int[]> DoubleBox3(Box<int[]> boxOfIntegers) =>
   // We can use the SelectMany() extension method to Validate, Extract, and transform its contents.
   // Have a look at Box's SelectMany() implementation now
   // and realize that its this that is used to allow this 'double from' Linq expression construct, that you see from time to time in
   // peoples code
   from extract in boxOfIntegers
   let transformedAndLifted = DoubleNumbers(extract) // bind() part of SelectMany() ie transform extracted value
   from transformed in transformedAndLifted
   select
      transformed; // see internals of SelectMany function --> project(extract, transformedAndLiftedResult) as this select statement is this project() function in SelectMany implementation

/* Note: we are not using 'extract' value in this project function (the final select), just the transformed value
 * we could have used in during our transformation, because it in scope and is accessible to be included */
static Box<string> DoubleBox11(Box<int[]> boxOfIntegers)
{
   return
      from start in boxOfIntegers
      from startTransformed in DoTransformToAnyBox(start) // bind() part of SelectMany() ie transform extracted value
      select start + startTransformed; // Project(extract, transformedAndLifted) part of SelectMany

   // local function
   Box<string> DoTransformToAnyBox(int[] input) => new($"{input.Sum()}");
}

static Box<int[]> DoubleBox4(Box<int[]> boxOfIntegers)
{
   Box<int[]> t = from extract in boxOfIntegers
      select DoubleNumbersNoLift(extract); // Remember Select() does the lift!

   return t;
}

// As this does not return a Monad, it can be used as a transformation function, that is passed to Map()
static int[] DoubleNumbersNoLift(int[] extract)
{
   return extract.Select(x => x * 2).ToArray();
}