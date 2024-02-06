using System.Formats.Tar;
using System.Runtime.InteropServices;

namespace TarFilesSample;

internal static class Program
{
   private static string DirectoryToTarball =>
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

      var tarPath = Path.Combine(DirectoryToTarball, "..", "archive.tar");
      TarFile.CreateFromDirectory(DirectoryToTarball, tarPath, false);

      Directory.CreateDirectory(DirectoryToExtractTo);
      TarFile.ExtractToDirectory(tarPath, DirectoryToExtractTo, true);

      foreach (var file in Directory.EnumerateFiles(DirectoryToExtractTo))
      {
         Console.WriteLine(file);
      }
   }

   private static void AddFilesToFolder()
   {
      Directory.CreateDirectory(DirectoryToTarball);
      foreach (var chr in new[] { 'A', 'B', 'C' })
      {
         File.WriteAllText(Path.Combine(DirectoryToTarball, $"{chr}.txt"), $"This is {chr}");
      }
   }
}