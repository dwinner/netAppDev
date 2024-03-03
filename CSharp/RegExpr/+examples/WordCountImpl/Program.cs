using System.Text.RegularExpressions;

var r = @"\b(\w|[-'])+\b";

var text = "It's all mumbo-jumbo to me";
Console.WriteLine(Regex.Matches(text, r).Count);