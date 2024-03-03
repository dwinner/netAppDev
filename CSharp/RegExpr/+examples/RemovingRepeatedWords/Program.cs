using System.Text.RegularExpressions;

var r = @"(?'dupe'\w+)\W\k'dupe'";

var text = "In the the beginning...";
Console.WriteLine(Regex.Replace(text, r, "${dupe}"));