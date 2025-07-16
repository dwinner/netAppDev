using UseOfTransform;

// Now my Box contains another kind of thing, a list of integers. So effectively my Box is a box of numbers!
var numbers1 = new Box<int[]>([1, 2, 3, 4, 5, 6, 7, 8, 9, 10]);
var numbers2 = new Box<int[]>([11, 12, 13, 14, 15, 16, 17, 18, 19, 20]);

// Now have a look at Box class again, it has changed a little with two new extension methods.
// Notice how it (the extension methods) follows an interesting trend of doing VETL or
// Validate content, Extract Content, Transform Content and Lift the content

// Validate, Extract, Transform and YouLift(If Valid)
// user define transformation passed to bind(), this currently just ignores the extracted contents in the Box and returns a new set of numbers as dictated by MyFunction()
var transformedResult1 = numbers1.Bind(MyFunction);

// Validate, Extract, Transform and automatic Lift (If Valid)
// same transformation result, but our transformation function didn't
// have to return a Box (we used map to run our transformation which automatically
// will put the result of our transform function in a new Box)
var transformedResult2 = numbers2.Map(MyFunction2);

// The Box class is now considered a Monad, because it has these two additional functions defined on it (map and bind).
// Note in both cases of Bind() and Map() we did a transformation of the contents ie we provided a function that would
// work on the item in the Box(or Monad)

// In both cases we didn't doing anything with the contents of the box, so we didn't really transform the
// contents (we just used what Map() and Bind() extracted from the box)
// We could have included the contents in our transformation and manipulated it...

// The take away is that Map() and Bind() do the same thing but Bind() requires you to put your
// transformation result back in the Box, while Map doesn't.
// Both methods 'manage' your user defined transform function by running it only if it deems
// it should (validation passes) and then depending on the specific function,
// it will either lift the result of the transformation(map) or require that your transformation
// function's signature explicitly says it will itself (bind)
// This means, you don't have to return a Box (like you do when you with Bind),
// when transforming with the Map() function...it automatically does this for you

Console.WriteLine($"The result of the Bind operation was {transformedResult1.Item}");
Console.WriteLine($"The result of the Map operation was {transformedResult2.Item}");

// But here is something sneaky:
var
   transformedResult3 =
      numbers1.Map(contents =>
         "I'm a string!"); // Look, we've been able to change the type of the Box from int[] to a string! 
// This is by virtue of the fact that your transformation function returned, and it transformed the return
// type also...and put it back into a box
// It's still in a Box, but is a box of a string now instead of a box of numbers....
// Map() and Bind() can do this, and this is what makes these function really at transformations
return;

static int[] MyFunction2(int[] numbers)
{
   // Used as a map() transformation, so no need to lift into a Box or anything...
   return [3, 4, 5];
}

static Box<int[]> MyFunction(int[] integerArray)
{
   // Notice that this function is going to be used in Box's Bind() function
   // (and is indeed compatible with it - see Bind function signature), needs to return a new Box<T> ie lift into a Box() again
   return new Box<int[]>([1, 2]);
}