using System.Linq;
using System.Reflection;

namespace ReflectionToString
{
   public static class ObjectExtensions
   {
      private const string Separator = " || ";

      public static string ToStringReflection<T>(this T @this)
         =>
         string.Join(Separator,
         (from propertyInfo in @this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
            where propertyInfo.CanRead
            select $"{propertyInfo.Name}: {propertyInfo.GetValue(@this, null)}").ToArray());
   }
}