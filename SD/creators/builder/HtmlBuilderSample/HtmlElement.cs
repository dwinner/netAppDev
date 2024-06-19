using System.Text;

namespace HtmlBuilderSample;

internal class HtmlElement
{
   private const int IndentSize = 2;
   public readonly List<HtmlElement> Elements = [];
   public string Name = null!;
   private readonly string _text = null!;

   public HtmlElement()
   {
   }

   public HtmlElement(string name, string text)
   {
      Name = name;
      _text = text;
   }

   private string ToStringImpl(int indent)
   {
      var sb = new StringBuilder();
      var i = new string(' ', IndentSize * indent);
      sb.Append($"{i}<{Name}>\n");
      if (!string.IsNullOrWhiteSpace(_text))
      {
         sb.Append(new string(' ', IndentSize * (indent + 1)));
         sb.Append(_text);
         sb.Append("\n");
      }

      foreach (var e in Elements)
      {
         sb.Append(e.ToStringImpl(indent + 1));
      }

      sb.Append($"{i}</{Name}>\n");
      return sb.ToString();
   }

   public override string ToString() => ToStringImpl(0);

   public static HtmlBuilder Create(string name) => new(name);
}