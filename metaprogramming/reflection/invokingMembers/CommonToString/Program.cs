using System.Collections;
using System.Text;

namespace CommonToString;

internal static class Program
{
   private static void Main()
   {
      Console.WriteLine(ToStringEx(new List<int> { 5, 6, 7 }));
      Console.WriteLine(ToStringEx("xyyzzz".GroupBy(c => c)));
   }

   public static string ToStringEx(object? value)
   {
      if (value == null)
      {
         return "<null>";
      }

      if (value.GetType().IsPrimitive)
      {
         return value.ToString() ?? string.Empty;
      }

      var sb = new StringBuilder();

      if (value is IList)
      {
         sb.Append("List of " + ((IList)value).Count + " items: ");
      }

      var closedIGrouping = value
         .GetType()
         .GetInterfaces()
         .FirstOrDefault(type => type.IsGenericType
                                 && type.GetGenericTypeDefinition() == typeof(IGrouping<,>));

      if (closedIGrouping != null) // Call the Key property on IGrouping<,>
      {
         var pi = closedIGrouping.GetProperty("Key");
         var key = pi.GetValue(value, null);
         sb.Append("Group with key=" + key + ": ");
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