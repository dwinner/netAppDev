namespace ArcBeforeSample;

public class AuditManager
{
   private readonly string _directoryName;
   private readonly int _maxEntriesPerFile;

   public AuditManager(int maxEntriesPerFile, string directoryName)
   {
      _maxEntriesPerFile = maxEntriesPerFile;
      _directoryName = directoryName;
   }

   public void AddRecord(string visitorName, DateTime timeOfVisit)
   {
      var filePaths = Directory.GetFiles(_directoryName);
      var sorted = SortByIndex(filePaths);

      var newRecord = visitorName + ';' + timeOfVisit.ToString("s");

      if (sorted.Length == 0)
      {
         var newFile = Path.Combine(_directoryName, "audit_1.txt");
         File.WriteAllText(newFile, newRecord);
         return;
      }

      var (currentFileIndex, currentFilePath) = sorted.Last();
      var lines = File.ReadAllLines(currentFilePath).ToList();

      if (lines.Count < _maxEntriesPerFile)
      {
         lines.Add(newRecord);
         var newContent = string.Join("\r\n", lines);
         File.WriteAllText(currentFilePath, newContent);
      }
      else
      {
         var newIndex = currentFileIndex + 1;
         var newName = $"audit_{newIndex}.txt";
         var newFile = Path.Combine(_directoryName, newName);
         File.WriteAllText(newFile, newRecord);
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