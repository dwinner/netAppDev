const int musicalNote = 0x1D161;

var s = char.ConvertFromUtf32(musicalNote);
var len = s.Length;
Console.WriteLine(len);

var s1 = char.ConvertToUtf32(s, 0).ToString("X");
Console.WriteLine(s1);

var s2 = char.ConvertToUtf32(s[0], s[1]).ToString("X");
Console.WriteLine(s2);