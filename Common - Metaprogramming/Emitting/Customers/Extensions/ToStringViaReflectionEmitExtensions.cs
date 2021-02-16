using System;
using System.Collections.Generic;

namespace Customers.Extensions
{
   public static class ToStringViaReflectionEmitExtensions
   {
      private static readonly Lazy<ReflectionEmitMethodGenerator> _Generator = new Lazy<ReflectionEmitMethodGenerator>();
      private static readonly Dictionary<Type, Delegate> _Methods = new Dictionary<Type, Delegate>();

      internal static string ToStringReflectionEmit<T>(this T @this)
      {
         var targetType = @this.GetType();

         if (!_Methods.ContainsKey(targetType))
         {
            _Methods.Add(targetType, _Generator.Value.Generate<T>());
         }

         var func = _Methods[targetType] as Func<T, string>;
         return func != null ? func(@this) : string.Empty;
      }
   }
}