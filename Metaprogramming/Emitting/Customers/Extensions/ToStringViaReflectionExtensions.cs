using System.Linq;
using System.Reflection;

namespace Customers.Extensions
{
   public static class ToStringViaReflectionExtensions
   {
      public static string ToStringReflection<T>(this T @this)
         =>
            string.Join(Constants.Separator,
               (from property in @this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                  where property.CanRead
                  select $"{property.Name}: {property.GetValue(@this, null)}").ToList());
   }
}