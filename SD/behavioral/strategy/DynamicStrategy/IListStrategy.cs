using System.Text;

namespace DynamicStrategy;

public interface IListStrategy
{
   void Start(StringBuilder aStringBuilder);

   void End(StringBuilder aStringBuilder);

   void AddListItem(StringBuilder aStringBuilder, string item);
}