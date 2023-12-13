namespace FunctionalBasedArc;

public class FileContent
{
   public readonly string FileName;
   public readonly string[] Lines;

   public FileContent(string fileName, string[] lines)
   {
      FileName = fileName;
      Lines = lines;
   }
}