using System;
using System.Reflection;

namespace DuckTyping
{
   public static class ObjectExtensions
   {
      public static object Call(this object @this, string methodName, params object[] parameters)
         =>
         @this
            .GetType()
            .GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public, null,
               Array.ConvertAll(parameters, target => target.GetType()), null)
            .Invoke(@this, parameters);
   }
}