// Wrapping a string in three or more quote characters (""") creates a raw string literal.
// Raw string literals can contain almost any character sequence, without escaping or doubling up:

var raw = """<file path="c:\temp\test.txt"></file>""";
Console.WriteLine(raw);

// Should you need to include three (or more) quote characters in the string itself, you can do so
// by wrapping the string in four (or more) quote characters:
raw = """"The """ sequence denotes raw string literals."""";
Console.WriteLine(raw);

// Multiline raw string literals are subject to special rules.
// The string, "Line 1\r\nLine 2", we can represent as follows:
var multiLineRaw = """
                   Line 1
                   Line 2
                   """;

Console.WriteLine(multiLineRaw);

// Notice that the opening and closing quotes must be on separate lines to the string content. Additionally:
//
//  •	Whitespace following the opening """ (on the same line) is ignored.
//
//  •	Whitespace preceding the closing """ (on the same line) is treated as common indentation and is removed
//    from every line in the string. This lets you include indentation for source-code readability without
//    that indentation becoming part of the string.
var json = """
           {
             "Name" : "Joe"
           }
           """;
Console.WriteLine(json);

var interpolatedWithRaw = $$"""{ "TimeStamp": "{{DateTime.Now}}" }""";
Console.WriteLine(interpolatedWithRaw);