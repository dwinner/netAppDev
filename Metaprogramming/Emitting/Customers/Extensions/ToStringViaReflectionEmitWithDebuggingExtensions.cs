using System;
using System.Collections.Generic;

namespace Customers.Extensions
{
   public static class ToStringViaReflectionEmitWithDebuggingExtensions
   {
      private static readonly Lazy<ReflectionEmitWithDebuggingMethodGenerator> _Generator =
         new Lazy<ReflectionEmitWithDebuggingMethodGenerator>();

      private static readonly Dictionary<Type, Delegate> _Methods = new Dictionary<Type, Delegate>();

      internal static string ToStringReflectionEmitWithDebugging<T>(this T @this)
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