namespace FunctionalBasedArc;

public struct FileUpdate
{
   public readonly string FileName;
   public readonly string NewContent;

   public FileUpdate(string fileName, string newContent)
   {
      FileName = fileName;
      NewContent = newContent;
   }
}