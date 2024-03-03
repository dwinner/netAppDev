using System.Text.RegularExpressions;

var r =
   @"(?i)\b" +
   @"[0-9a-fA-F]{8}\-" +
   @"[0-9a-fA-F]{4}\-" +
   @"[0-9a-fA-F]{4}\-" +
   @"[0-9a-fA-F]{4}\-" +
   @"[0-9a-fA-F]{12}" +
   @"\b";

var text = "Its key is {3F2504E0-4F89-11D3-9A0C-0305E82C3301}.";
Console.WriteLine(Regex.Match(text, r).Index);