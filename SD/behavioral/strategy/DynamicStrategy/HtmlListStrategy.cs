using System.Text;

namespace DynamicStrategy;

public class HtmlListStrategy : IListStrategy
{
   public void Start(StringBuilder aStringBuilder) => aStringBuilder.AppendLine("<ul>");

   public void End(StringBuilder aStringBuilder) => aStringBuilder.AppendLine("</ul>");

   public void AddListItem(StringBuilder aStringBuilder, string item)
   {
      aStringBuilder.AppendLine($"  <li>{item}</li>");
   }
}