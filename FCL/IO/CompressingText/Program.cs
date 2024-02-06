using System.IO.Compression;

var words = "The quick brown fox jumps over the lazy dog".Split();
var rand = new Random(0);

await using (Stream outFileStream = File.Create("compressed.bin"))
await using (Stream outBrotliStream = new BrotliStream(outFileStream, CompressionMode.Compress))
await using (TextWriter outTextWriter = new StreamWriter(outBrotliStream))
{
   for (var i = 0; i < 1000; i++)
   {
      await outTextWriter.WriteAsync($"{words[rand.Next(words.Length)]} ")
         .ConfigureAwait(false);
   }
}

var fileInfo = new FileInfo("compressed.bin");
Console.WriteLine(fileInfo.Length);

await using (Stream inFileStream = File.OpenRead("compressed.bin"))
await using (Stream inBrotliStream = new BrotliStream(inFileStream, CompressionMode.Decompress))
using (TextReader inTextReader = new StreamReader(inBrotliStream))
{
   var content = await inTextReader.ReadToEndAsync()
      .ConfigureAwait(false);
   Console.Write(content); // Output below:
}