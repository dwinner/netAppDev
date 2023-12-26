using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace CompressFileSample
{
   internal static class Program
   {
      private static void Main()
      {
         CompressFile("./test.txt", "./test.txt.gzip");
         DecompressFile("./test.txt.gzip");

         CompressFileWithBrotli("./test.txt", "./test.txt.brotli");
         DecompressFileWithBrotli("./test.txt.brotli");

         CreateZipFile("../StreamSamples/", "./test.zip");
      }

      public static void CreateZipFile(string directory, string zipFile)
      {
         InitSampleFilesForZip(directory);
         var destDirectory = Path.GetDirectoryName(zipFile);
         if (!Directory.Exists(destDirectory))
         {
            Directory.CreateDirectory(destDirectory);
         }

         var zipStream = File.Create(zipFile);
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

      private static void InitSampleFilesForZip(string directory)
      {
         if (!Directory.Exists(directory))
         {
            Directory.CreateDirectory(directory);

            for (var i = 0; i < 10; i++)
            {
               var destFileName = Path.Combine(directory, $"test{i}.txt");

               File.Copy("Test.txt", destFileName);
            }
         } // else nothing to do, using existing files from the directory
      }

      public static void DecompressFile(string fileName)
      {
         var inputStream = File.OpenRead(fileName);
         using (var outputStream = new MemoryStream())
         using (var compressStream = new DeflateStream(inputStream, CompressionMode.Decompress))
         {
            compressStream.CopyTo(outputStream);
            outputStream.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(outputStream, Encoding.UTF8, true, 4096, true))
            {
               var result = reader.ReadToEnd();
               Console.WriteLine(result);
            }
         }
      }

      public static void CompressFile(string fileName, string compressedFileName)
      {
         using (var inputStream = File.OpenRead(fileName))
         {
            var outputStream = File.OpenWrite(compressedFileName);
            using (var compressStream = new DeflateStream(outputStream, CompressionMode.Compress))
            {
               inputStream.CopyTo(compressStream);
            }
         }
      }

      public static void CompressFileWithBrotli(string fileName, string compressedFileName)
      {
         using (var inputStream = File.OpenRead(fileName))
         {
            var outputStream = File.OpenWrite(compressedFileName);
            using (var compressStream = new BrotliStream(outputStream, CompressionMode.Compress))
            {
               inputStream.CopyTo(compressStream);
            }
         }
      }

      public static void DecompressFileWithBrotli(string fileName)
      {
         var inputStream = File.OpenRead(fileName);
         using (var outputStream = new MemoryStream())
         using (var compressStream = new BrotliStream(inputStream, CompressionMode.Decompress))
         {
            compressStream.CopyTo(outputStream);
            outputStream.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(outputStream, Encoding.UTF8, true, 4096, true))
            {
               var result = reader.ReadToEnd();
               Console.WriteLine(result);
            }
         }
      }
   }
}