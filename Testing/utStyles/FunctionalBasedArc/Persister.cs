namespace FunctionalBasedArc;

public class Persister
{
   public FileContent[] ReadDirectory(string directoryName)
   {
      return Directory
         .GetFiles(directoryName)
         .Select(x => new FileContent(
            Path.GetFileName(x),
            File.ReadAllLines(x)))
         .ToArray();
   }

   public void ApplyUpdate(string directoryName, FileUpdate update)
   {
      var filePath = Path.Combine(directoryName, update.FileName);
      File.WriteAllText(filePath, update.NewContent);
   }
}