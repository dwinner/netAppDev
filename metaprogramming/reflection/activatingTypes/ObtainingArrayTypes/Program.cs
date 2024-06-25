var simpleArrayType = typeof(int).MakeArrayType();
Console.WriteLine(simpleArrayType == typeof(int[])); // True

//MakeArrayType can be passed an integer argument to make multidimensional rectangular arrays:
var cubeType = typeof(int).MakeArrayType(3); // cube shaped
Console.WriteLine(cubeType == typeof(int[,,])); // True

//GetElementType does the reverse: it retrieves an array type’s element type:
var e = typeof(int[]).GetElementType()!; // e == typeof (int)
Console.WriteLine(e);

//GetArrayRank returns the number of dimensions of a rectangular array:
var rank = typeof(int[,,]).GetArrayRank(); // 3
Console.WriteLine(rank);