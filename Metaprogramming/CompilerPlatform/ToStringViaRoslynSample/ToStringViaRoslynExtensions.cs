using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ToStringViaRoslynSample
{
   public static class ToStringViaRoslynExtensions
   {
      public sealed class Host<T>
      {
         public Host(T target)
         {
            Target = target;
         }

         public T Target { get; set; } 
      }

      public static string Generate<T>(this T @this)
      {
         var properties= @this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(info => info.CanRead).ToList();
         string code =
            $"new StringBuilder(){string.Join(".Append(\" || \")", $"{properties.Select(info => string.Format(".Append(\"{0}: \").Append(Target.{0})", info.Name))}.ToString()")}";
      }
   }
}