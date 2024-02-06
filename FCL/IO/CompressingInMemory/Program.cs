using System.IO.Compression;

var data = new byte[1_000]; // We can expect a good compression

// ratio from an empty array!
var memoryStream = new MemoryStream();
using (Stream outStream = new DeflateStream(memoryStream, CompressionMode.Compress))
{
   outStream.Write(data, 0, data.Length);
}

var compressed = memoryStream.ToArray();
Console.WriteLine(compressed.Length); // 11

// Decompress back to the data array:
memoryStream = new MemoryStream(compressed);
using (Stream inStream = new DeflateStream(memoryStream, CompressionMode.Decompress))
{
   for (var i = 0; i < 1_000; i += inStream.Read(data, i, 1_000 - i))
   {
   }
}