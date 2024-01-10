// From C# 11, when you declare an operator function, you can also declare a checked version.
// The checked version will be called inside checked expressions or blocks.

using CheckedOpVersion;

var b = new Note(2);
var other = checked(b + int.MaxValue); // throws OverflowException