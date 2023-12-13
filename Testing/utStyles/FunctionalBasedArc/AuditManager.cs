namespace FunctionalBasedArc;

public class AuditManager
{
   private readonly int _maxEntriesPerFile;

   public AuditManager(int maxEntriesPerFile) => _maxEntriesPerFile = maxEntriesPerFile;

   public FileUpdate AddRecord(
      FileContent[] files,
      string visitorName,
      DateTime timeOfVisit)
   {
      var sorted = SortByIndex(files);

      var newRecord = visitorName + ';' + timeOfVisit.ToString("s");

      if (sorted.Length == 0)
      {
         return new FileUpdate("audit_1.txt", newRecord);
      }

      var (currentFileIndex, currentFile) = sorted.Last();
      var lines = currentFile.Lines.ToList();

      if (lines.Count < _maxEntriesPerFile)
      {
         lines.Add(newRecord);
         var newContent = string.Join("\r\n", lines);
         return new FileUpdate(currentFile.FileName, newContent);
      }

      var newIndex = currentFileIndex + 1;
      var newName = $"audit_{newIndex}.txt";
      return new FileUpdate(newName, newRecord);
   }

   private (int index, FileContent file)[] SortByIndex(
      FileContent[] files)
   {
      return files
         .Select(file => (index: GetIndex(file.FileName), file))
         .OrderBy(x => x.index)
         .ToArray();
   }

   private int GetIndex(string fileName)
   {
      // File name example: audit_1.txt
      var name = Path.GetFileNameWithoutExtension(fileName);
      return int.Parse(name.Split('_')[1]);
   }
}