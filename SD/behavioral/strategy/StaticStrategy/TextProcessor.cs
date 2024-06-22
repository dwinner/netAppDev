using System.Text;

namespace StaticStrategy;

// a.k.a. policy
public class TextProcessor<TStrategy> where TStrategy : IListStrategy, new()
{
   private readonly IListStrategy _listStrategy = new TStrategy();
   private readonly StringBuilder _sb = new();

   public void AppendList(IEnumerable<string> items)
   {
      _listStrategy.Start(_sb);
      foreach (var item in items)
      {
         _listStrategy.AddListItem(_sb, item);
      }

      _listStrategy.End(_sb);
   }

   public override string ToString() => _sb.ToString();
}