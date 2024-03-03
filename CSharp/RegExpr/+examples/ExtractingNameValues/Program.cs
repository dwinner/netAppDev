using System.Text.RegularExpressions;

var r = @"(?m)^\s*(?'name'\w+)\s*=\s*(?'value'.*)\s*(?=\r?$)";

var text =
   @"id = 3
		secure = true
		timeout = 30";

foreach (Match m in Regex.Matches(text, r))
{
   Console.WriteLine(m.Groups["name"] + " is " + m.Groups["value"]);
}