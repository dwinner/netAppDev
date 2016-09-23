using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ExpressionSamples
{
   internal static class ToStringViaExpressionsExtensions
   {
      private static readonly Dictionary<Type, Delegate> _Methods = new Dictionary<Type, Delegate>();

      internal static string ToStringExpression<T>(this T @this)
      {
         var targetType = @this.GetType();
         if (!_Methods.ContainsKey(targetType))
         {
            _Methods.Add(targetType, CreateToStringViaExpression(@this));
         }

         var func = _Methods[targetType] as Func<T, string>;
         return func != null ? func(@this) : string.Empty;
      }

      private static Func<T, string> CreateToStringViaExpression<T>(T target)
      {
         var builder = typeof (StringBuilder);
         //var builderCtor = builder.GetConstructor(Type.EmptyTypes);
         var append = builder.GetMethod(nameof(StringBuilder.Append), new[] {typeof (string)});
         var toString = builder.GetMethod(nameof(StringBuilder.ToString), Type.EmptyTypes);

         var thisParameter = Expression.Parameter(typeof (T), "@this");
         // ReSharper disable once AssignNullToNotNullAttribute
         Expression body = Expression.New(builder.GetConstructor(Type.EmptyTypes));

         var properties =
            target.GetType()
               .GetProperties(BindingFlags.Instance | BindingFlags.Public)
               .Where(property => property.CanRead)
               .ToList();

         for (var i = 0; i < properties.Count; i++)
         {
            var property = properties[i];
            body = Expression.Call(body, append, Expression.Constant($"{property.Name}: "));
            var typedAppend = builder.GetMethod(nameof(StringBuilder.Append), new[] {property.PropertyType});
            body = typedAppend.GetParameters()[0].ParameterType == property.PropertyType
               ? Expression.Call(body, typedAppend, Expression.Call(thisParameter, property.GetGetMethod()))
               : Expression.Call(body, typedAppend,
                  Expression.TypeAs(Expression.Call(thisParameter, property.GetGetMethod()),
                     typedAppend.GetParameters()[0].ParameterType));
            if (i < properties.Count - 1)
            {
               body = Expression.Call(body, append, Expression.Constant(Constants.Separator));
            }
         }

         body = Expression.Call(body, toString);

         return Expression.Lambda<Func<T, string>>(body, thisParameter).Compile();
      }
   }
}