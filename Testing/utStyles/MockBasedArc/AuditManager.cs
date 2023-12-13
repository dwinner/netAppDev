namespace MockBasedArc;

public class AuditManager
{
   private readonly string _directoryName;
   private readonly IFileSystem _fileSystem;
   private readonly int _maxEntriesPerFile;

   public AuditManager(
      int maxEntriesPerFile,
      string directoryName,
      IFileSystem fileSystem)
   {
      _maxEntriesPerFile = maxEntriesPerFile;
      _directoryName = directoryName;
      _fileSystem = fileSystem;
   }

   public void AddRecord(string visitorName, DateTime timeOfVisit)
   {
      var filePaths = _fileSystem.GetFiles(_directoryName);
      var sorted = SortByIndex(filePaths);

      var newRecord = visitorName + ';' + timeOfVisit.ToString("s");

      if (sorted.Length == 0)
      {
         var newFile = Path.Combine(_directoryName, "audit_1.txt");
         _fileSystem.WriteAllText(newFile, newRecord);
         return;
      }

      (var currentFileIndex, var currentFilePath) = sorted.Last();
      var lines = _fileSystem.ReadAllLines(currentFilePath);

      if (lines.Count < _maxEntriesPerFile)
      {
         lines.Add(newRecord);
         var newContent = string.Join("\r\n", lines);
         _fileSystem.WriteAllText(currentFilePath, newContent);
      }
      else
      {
         var newIndex = currentFileIndex + 1;
         var newName = $"audit_{newIndex}.txt";
         var newFile = Path.Combine(_directoryName, newName);
         _fileSystem.WriteAllText(newFile, newRecord);
      }
   }

   private (int index, string path)[] SortByIndex(string[] files)
   {
      return files
         .Select(path => (index: GetIndex(path), path))
         .OrderBy(x => x.index)
         .ToArray();
   }

   private int GetIndex(string filePath)
   {
      // File name example: audit_1.txt
      var fileName = Path.GetFileNameWithoutExtension(filePath);
      return int.Parse(fileName.Split('_')[1]);
   }
}