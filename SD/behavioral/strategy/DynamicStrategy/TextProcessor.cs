using System.Text;

namespace DynamicStrategy;

public class TextProcessor
{
   private readonly StringBuilder _stringBuilder = new();
   private IListStrategy? _listStrategy;

   public void SetOutputFormat(OutputFormat format) =>
      _listStrategy = format switch
      {
         OutputFormat.Markdown => new MarkdownListStrategy(),
         OutputFormat.Html => new HtmlListStrategy(),
         _ => throw new ArgumentOutOfRangeException(nameof(format), format, null)
      };

   public void AppendList(IEnumerable<string> items)
   {
      if (_listStrategy == null)
      {
         return;
      }

      _listStrategy.Start(_stringBuilder);
      foreach (var item in items)
      {
         _listStrategy.AddListItem(_stringBuilder, item);
      }

      _listStrategy.End(_stringBuilder);
   }

   public StringBuilder Clear() => _stringBuilder.Clear();

   public override string ToString() => _stringBuilder.ToString();
}