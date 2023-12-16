namespace Invokes;

public interface IBuildCsv
{
   void SetHeader(IEnumerable<string> fields);
   void AddRow(IEnumerable<string> fields);
   string Build();
}