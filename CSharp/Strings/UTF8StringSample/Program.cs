// From C# 11, you can use the u8 suffix to create string literals encoded in UTF-8 rather than UTF-16.
// This feature is intended for advanced scenarios such as the low-level handling of JSON text in
// performance hotspots:

var utf8 = "ab→cd"u8; // Arrow symbol consumes 3 bytes
Console.WriteLine(utf8.Length); // 7 

// The underlying type is ReadOnlySpan<byte>, which we cover in Chapter 23.
// You can convert this to an array by calling the ToArray() method.