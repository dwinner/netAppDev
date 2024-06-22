using System.Text;

namespace StaticStrategy;

public class HtmlListStrategy : IListStrategy
{
   public void Start(StringBuilder sb)
   {
      sb.AppendLine("<ul>");
   }

   public void End(StringBuilder sb)
   {
      sb.AppendLine("</ul>");
   }

   public void AddListItem(StringBuilder sb, string item)
   {
      sb.AppendLine($"  <li>{item}</li>");
   }
}