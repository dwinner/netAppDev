/**
 * Compressing files via Zip, Deflate using .NET Core
 */

using System.IO;
using System.IO.Compression;
using System.Text;
using static System.Console;

namespace CompressFileSample
{
   public class Program
   {
      public static void Main()
      {
         CompressFile("test.txt", "test.compressed");
         DecompressFile("test.txt.gzip");
         CreateZipFile("c:/test", "c:/test2/test.zip");
      }

      private static void CreateZipFile(string directory, string zipFile)
      {
         using (var zipStream = File.OpenWrite(zipFile))
         using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
         {
            var files = Directory.EnumerateFiles(directory, "*", SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
               var entry = archive.CreateEntry(Path.GetFileName(file));
               using (var inputStream = File.OpenRead(file))
               using (var outputStream = entry.Open())
               {
                  inputStream.CopyTo(outputStream);
               }
            }
         }
      }

      private static void DecompressFile(string fileName)
      {
         const int bufferSize = 0x1000;
         using (var inputStream = File.OpenRead(fileName))
         using (var outputStream = new MemoryStream())
         using (var compressStream = new DeflateStream(inputStream, CompressionMode.Decompress))
         {
            compressStream.CopyTo(outputStream);
            outputStream.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(outputStream, Encoding.UTF8, true, bufferSize, true))
            {
               var result = reader.ReadToEnd();
               WriteLine(result);
            }
         }
      }

      private static void CompressFile(string fileName, string compressedFileName)
      {
         using (var inputStream = File.OpenRead(fileName))
         using (var outputStream = File.OpenWrite(compressedFileName))
         using (var compressStream = new DeflateStream(outputStream, CompressionMode.Compress))
         {
            inputStream.CopyTo(compressStream);
         }
      }
   }
}