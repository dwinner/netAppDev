namespace MockBasedArc;

public interface IFileSystem
{
   string[] GetFiles(string directoryName);
   void WriteAllText(string filePath, string content);
   List<string> ReadAllLines(string filePath);
}