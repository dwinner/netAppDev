using System.IO.Compression;

namespace GZipStreamSample;

internal static class Program
{
   private const string Textfile = "myfile.txt";

   private static readonly Random Rnd = new();

   private static async Task Main()
   {
      await File.WriteAllTextAsync(Textfile, RandomString(0x1000))
         .ConfigureAwait(false);
      var backup = Textfile.Replace(".txt", "-Copy.txt");
      File.Copy(Textfile, backup);

      await GZipAsync(Textfile)
         .ConfigureAwait(false);
      // Don't delete it so we can list it for comparison
      await GUnzipAsync($"{Textfile}.gz", false).ConfigureAwait(false);

      foreach (var fileInfo in Directory
                  .EnumerateFiles(Environment.CurrentDirectory, "myfile*")
                  .Select(fileName => new FileInfo(fileName)))
      {
         Console.WriteLine($"{fileInfo.Name} {fileInfo.Length} bytes");
      }

      File.Delete(Textfile);
      File.Delete(Textfile + ".gz");
      File.Delete(backup);
   }

   private static async Task GZipAsync(string sourcefile, bool deleteSource = true)
   {
      var gzipFile = $"{sourcefile}.gz";
      if (File.Exists(gzipFile))
      {
         throw new Exception("Gzip file already exists");
      }

      // Compress
      await using (var inStream = File.Open(sourcefile, FileMode.Open))
      await using (var outStream = new FileStream(gzipFile, FileMode.CreateNew))
      await using (var gzipStream = new GZipStream(outStream, CompressionMode.Compress))
      {
         await inStream.CopyToAsync(gzipStream)
            .ConfigureAwait(false);
      }

      if (deleteSource)
      {
         File.Delete(sourcefile);
      }
   }

   private static async Task GUnzipAsync(string gzipFile, bool deleteGzip = true)
   {
      if (Path.GetExtension(gzipFile) != ".gz")
      {
         throw new Exception("Not a gzip file");
      }

      var uncompressedFile = gzipFile[..^3];
      if (File.Exists(uncompressedFile))
      {
         throw new Exception("Destination file already exists");
      }

      // Uncompress
      await using (var uncompressToStream = File.Open(uncompressedFile, FileMode.Create))
      await using (var zipfileStream = File.Open(gzipFile, FileMode.Open))
      await using (var unzipStream = new GZipStream(zipfileStream, CompressionMode.Decompress))
      {
         await unzipStream.CopyToAsync(uncompressToStream)
            .ConfigureAwait(false);
      }

      if (deleteGzip)
      {
         File.Delete(gzipFile);
      }
   }

   private static string RandomString(int length)
   {
      const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
      return new string(Enumerable.Repeat(chars, length)
         .Select(str => str[Rnd.Next(str.Length)])
         .ToArray());
   }
}