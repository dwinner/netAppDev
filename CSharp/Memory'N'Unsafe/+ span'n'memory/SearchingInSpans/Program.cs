using System.Buffers;

ReadOnlySpan<char> span = "The quick brown fox jumps over the lazy dog.";
var vowels = SearchValues.Create("aeiou");
Console.WriteLine(span.IndexOfAny(vowels)); // 2