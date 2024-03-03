using System.Text.RegularExpressions;

var r = @"(?m)^.{80,}(?=\r?$)";

var fifty = new string('x', 50);
var eighty = new string('x', 80);

var text = eighty + "\r\n" + fifty + "\r\n" + eighty;

Console.WriteLine(Regex.Matches(text, r).Count);