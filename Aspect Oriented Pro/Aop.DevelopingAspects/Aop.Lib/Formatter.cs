namespace Aop.Lib
{
   using System;
   using System.Text;

   using PostSharp.Aspects;

   public static class Formatter
   {
      public static void AppendTypeName(StringBuilder stringBuilder, Type type)
      {
         stringBuilder.Append(type.FullName);
         if (type.IsGenericType)
         {
            var genericArguments = type.GetGenericArguments();
            AppendGenericArguments(stringBuilder, genericArguments);
         }
      }

      public static void AppendGenericArguments(StringBuilder stringBuilder, Type[] genericArguments)
      {
         stringBuilder.Append('<');
         for (var i = 0; i < genericArguments.Length; i++)
         {
            if (i > 0)
            {
               stringBuilder.Append(", ");
            }

            stringBuilder.Append(genericArguments[i].Name);
         }

         stringBuilder.Append('>');
      }

      public static void AppendArguments(StringBuilder stringBuilder, Arguments arguments)
      {
         stringBuilder.Append('(');
         for (var i = 0; i < arguments.Count; i++)
         {
            if (i > 0)
            {
               stringBuilder.Append(", ");
            }

            stringBuilder.Append(arguments[i]);
         }

         stringBuilder.Append(')');
      }
   }
}