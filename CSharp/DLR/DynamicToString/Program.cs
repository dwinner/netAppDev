#nullable disable
using System.Collections;
using System.Text;

namespace DynamicToString;

internal static class Program
{
   public static void Main()
   {
      var strDump = ToStringEx("xyyzzz".GroupBy(c => c));
      Console.WriteLine(strDump);
   }

   private static string GetGroupKey<TKey, TElement>(IGrouping<TKey, TElement> group) =>
      $"Group with key={group.Key}: ";

   private static string GetGroupKey(object source) => null;

   private static string ToStringEx(object value)
   {
      if (value == null)
      {
         return "<null>";
      }

      if (value is string)
      {
         return (string)value;
      }

      if (value.GetType().IsPrimitive)
      {
         return value.ToString();
      }

      var sb = new StringBuilder();

      string groupKey = GetGroupKey((dynamic)value); // Dynamic dispatch
      if (groupKey != null)
      {
         sb.Append(groupKey);
      }

      if (value is IEnumerable)
      {
         foreach (var element in (IEnumerable)value)
         {
            sb.Append(ToStringEx(element) + " ");
         }
      }

      if (sb.Length == 0)
      {
         sb.Append(value);
      }

      return "\r\n" + sb;
   }
}