using System.Text.RegularExpressions;

var htmlFragment = "© 2007";

var result = Regex.Replace(
   htmlFragment,
   @"[\u0080-\uFFFF]",
   m => $@"&#{(int)m.Value[0]};");

Console.WriteLine(result);