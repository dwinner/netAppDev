using System.Text;

namespace DynamicStrategy;

public class MarkdownListStrategy : IListStrategy
{
   public void Start(StringBuilder aStringBuilder)
   {
      // markdown doesn't require a list preamble
   }

   public void End(StringBuilder aStringBuilder)
   {
   }

   public void AddListItem(StringBuilder aStringBuilder, string item)
   {
      aStringBuilder.AppendLine($" * {item}");
   }
}