using System;

int[] data = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

// indices and the hat operator

var first1 = data[0];
var last1 = data[data.Length - 1];
Console.WriteLine($"first: {first1}, last: {last1}");

var last2 = data[^1];
Console.WriteLine(last2);

Index firstIndex = 0;
var lastIndex = ^1;
var first3 = data[firstIndex];
var last3 = data[lastIndex];
Console.WriteLine($"first: {first3}, last: {last3}");

// ranges

ShowRange("full range", data[..]);
ShowRange("first three", data[..3]);
ShowRange("fourth to sixth", data[3..6]);
ShowRange("last three", data[^3..]);

void ShowRange(string title, int[] data)
{
   Console.WriteLine(title);
   Console.WriteLine(string.Join(" ", data));
   Console.WriteLine();
}

var fullRange = ..;
var firstThree = ..3;
var fourthToSixth = 3..6;
var lastThree = ^3..;

Console.WriteLine(fullRange);

// efficiently changing array content

var slice1 = data[3..5];
slice1[0] = 42;
Console.WriteLine($"value in array didn't change: {data[3]}, value from slice: {slice1[0]}");

var sliceToSpan = data.AsSpan()[3..5];
sliceToSpan[0] = 42;
Console.WriteLine($"value in array: {data[3]}, value from slice: {sliceToSpan[0]}");

// indices and ranges with custom collections

MyCollection coll = new();
var n = coll[^20];
Console.WriteLine($"Item from the collection: {n}");
ShowRange("Using custom collection", coll[45..^40]);