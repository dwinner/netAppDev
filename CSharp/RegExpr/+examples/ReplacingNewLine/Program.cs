// This replaces \n with \r\n without breaking existing \r\n occurrences.

using System.Text.RegularExpressions;

var n = "\n";
var rn = "\r\n";
var text = "L1" + n + "L2" + rn + "L3";

var result = Regex.Replace(text, "(?<!\r)\n", "\r\n");

foreach (var x1 in result.Select(c => new { c, Code = (int)c }))
{
   Console.WriteLine(x1);
}