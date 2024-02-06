using System.IO.Compression;
using System.Runtime.InteropServices;

namespace ZipFilesSample;

internal static class Program
{
   private static string DirectoryToZip =>
      RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
         ? @".\MyFolder"
         : "./MyFolder";

   private static string DirectoryToExtractTo =>
      RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
         ? @".\Extracted"
         : "./Extracted";

   private static void Main()
   {
      AddFilesToFolder();

      var zipPath = Path.Combine(DirectoryToZip, "..", "archive.zip");
      ZipFile.CreateFromDirectory(DirectoryToZip, zipPath);

      Directory.CreateDirectory(DirectoryToExtractTo);
      ZipFile.ExtractToDirectory(zipPath, DirectoryToExtractTo);

      foreach (var file in Directory.EnumerateFiles(DirectoryToExtractTo))
      {
         Console.WriteLine(file);
      }
   }

   private static void AddFilesToFolder()
   {
      Directory.CreateDirectory(DirectoryToZip);
      foreach (var chr in new[] { 'A', 'B', 'C' })
      {
         File.WriteAllText(Path.Combine(DirectoryToZip, $"{chr}.txt"), $"This is {chr}");
      }
   }
}