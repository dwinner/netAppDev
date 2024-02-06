using System.IO.Compression;

using (Stream outfileStream = File.Create("compressed.bin"))
using (Stream outDeflateStream = new DeflateStream(outfileStream, CompressionMode.Compress))
{
   for (byte i = 0; i < 100; i++)
   {
      outDeflateStream.WriteByte(i);
   }
}

var fileInfo = new FileInfo("compressed.bin");
Console.WriteLine(fileInfo.Length);

using (Stream inFileStream = File.OpenRead("compressed.bin"))
using (Stream inDeflateStream = new DeflateStream(inFileStream, CompressionMode.Decompress))
{
   for (byte i = 0; i < 100; i++)
   {
      Console.WriteLine(inDeflateStream.ReadByte()); // Writes 0 to 99
   }
}