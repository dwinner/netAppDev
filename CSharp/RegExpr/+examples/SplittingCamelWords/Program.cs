using System.Text.RegularExpressions;

var r = @"(?=[A-Z])";

foreach (var s in Regex.Split("oneTwoThree", r))
{
   Console.Write(s + " ");
}