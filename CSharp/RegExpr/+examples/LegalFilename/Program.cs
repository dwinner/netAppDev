using System.Text.RegularExpressions;

var input = "My \"good\" <recipes>.txt";

var invalidChars = Path.GetInvalidFileNameChars();
var invalidString = Regex.Escape(new string(invalidChars));

var valid = Regex.Replace(input, $"[{invalidString}]", string.Empty);
Console.WriteLine(valid);