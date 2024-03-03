using System.Text.RegularExpressions;

var r =
   @"<(?'tag'\w+?).*>" + // match first tag, and name it 'tag'
   @"(?'text'.*?)" + // match text content, name it 'text'
   @"</\k'tag'>"; // match last tag, denoted by 'tag'

var text = "<h1>hello</h1>";

var m = Regex.Match(text, r);

Console.WriteLine(m.Groups["tag"].Value);
Console.WriteLine(m.Groups["text"].Value);